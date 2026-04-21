const API_ENDPOINT = 'http://localhost:5013/api/events/browsing';
const DEBOUNCE_MS = 10000; // 10 seconds
const DEBOUNCE_KEY = 'recent_urls';

// Filtered out URL patterns
const EXCLUDED_PATTERNS = [
  'chrome://',
  'about:',
  'chrome-extension://',
  'edge://',
  'file://',
  'moz-extension://',
  'safari-extension://',
  'view-source:',
  'data:',
  'blob:'
];

/**
 * Check if URL should be tracked
 */
function isTrackableUrl(url) {
  if (!url) return false;
  
  // Exclude non-web URLs
  if (EXCLUDED_PATTERNS.some(pattern => url.startsWith(pattern))) {
    return false;
  }
  
  // Only track http and https URLs
  if (!url.startsWith('http://') && !url.startsWith('https://')) {
    return false;
  }
  
  return true;
}

/**
 * Send browsing event to local API
 */
async function sendBrowsingEvent(url, title) {
  try {
    const payload = {
      url,
      title: title || '',
      timestamp: new Date().toISOString()
    };

    const response = await fetch(API_ENDPOINT, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    });

    // Silently fail if API is unavailable - no retry spam
    if (!response.ok) {
      console.debug(`API response: ${response.status}`);
    }
  } catch (error) {
    // API unavailable or network error - fail silently
    console.debug('Local API unreachable');
  }
}

/**
 * Check if URL was recently visited (within debounce window)
 */
async function isRecentlyTracked(url) {
  const data = await chrome.storage.local.get(DEBOUNCE_KEY);
  const recentUrls = data[DEBOUNCE_KEY] || {};

  if (recentUrls[url]) {
    const timeSinceLastVisit = Date.now() - recentUrls[url];
    return timeSinceLastVisit < DEBOUNCE_MS;
  }

  return false;
}

/**
 * Mark URL as recently visited
 */
async function markAsTracked(url) {
  const data = await chrome.storage.local.get(DEBOUNCE_KEY);
  const recentUrls = data[DEBOUNCE_KEY] || {};

  recentUrls[url] = Date.now();

  // Clean up old entries to prevent storage bloat
  const cutoffTime = Date.now() - DEBOUNCE_MS;
  Object.keys(recentUrls).forEach(key => {
    if (recentUrls[key] < cutoffTime) {
      delete recentUrls[key];
    }
  });

  await chrome.storage.local.set({ [DEBOUNCE_KEY]: recentUrls });
}

/**
 * Handle completed navigation
 */
async function handleNavigation(details) {
  // Only track main frame navigation (not iframes)
  if (details.frameId !== 0) return;

  // Get the tab to retrieve title
  const tab = await chrome.tabs.get(details.tabId);

  if (!isTrackableUrl(tab.url)) return;
  if (await isRecentlyTracked(tab.url)) return;

  // Mark as tracked before sending (to prevent race conditions)
  await markAsTracked(tab.url);

  // Send to API
  await sendBrowsingEvent(tab.url, tab.title);
}

// Listen for navigation completion
chrome.webNavigation.onCompleted.addListener(handleNavigation);
