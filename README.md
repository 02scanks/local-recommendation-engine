# Browsing Insights Dashboard

A **privacy-first, self-hosted personalization engine** that tracks your browsing activity locally and turns it into meaningful insights.

## Features

### Dashboard UI

- **Activity Summary** - Real-time statistics about your browsing behavior
  - Total pages visited today
  - Most visited domain
  - Peak activity hour
  
- **Browsing Activity Feed** - Clean, chronological list of recent pages
  - Page titles with clickable URLs
  - Timestamps and domain extraction
  - Beautiful card-based layout

- **Top Topics/Interests** - Automatically extracted keywords from page titles
  - Visual frequency indicators
  - Tag-based display
  
- **Time-Based View** - Activity grouped by time periods
  - Today vs Yesterday comparison
  - Visual bar charts

### Technical Features

- Clean, minimal, modern UI built with Tailwind CSS
- Responsive design (desktop-first)
- Real-time data from API endpoints
- Auto-refresh every 30 seconds
- No authentication required
- No external analytics or tracking

---

## Quick Start

### Prerequisites

- .NET 10.0 SDK
- Modern web browser

### 1. Run the Backend

```bash
# Build the project
dotnet build

# Run the server
dotnet run
```

The API will be available at `http://localhost:5013`

### 2. Access the Dashboard

Open your browser and navigate to:

```
http://localhost:5013/
```

The dashboard will automatically load and display your browsing activity data from the API.

---

## API Endpoints

The backend provides the following REST endpoints:

### Events

- **POST** `/api/events/browsing` - Create a new browsing event
  ```json
  {
    "url": "https://example.com",
    "title": "Example Page",
    "timestamp": "2024-04-21T10:30:00Z"
  }
  ```

- **GET** `/api/events` - Get recent browsing events (last 50)
  ```json
  [
    {
      "id": "guid",
      "url": "https://example.com",
      "title": "Example Page",
      "timestamp": "2024-04-21T10:30:00Z"
    }
  ]
  ```

### Insights

- **GET** `/api/insights/top-topics` - Get top keywords/topics
  ```json
  [
    { "topic": "react", "count": 15 },
    { "topic": "javascript", "count": 12 }
  ]
  ```

- **GET** `/api/insights/summary` - Get browsing statistics
  ```json
  {
    "totalToday": 42,
    "topDomain": "github.com",
    "topDomainCount": 15,
    "peakHour": "2:00 PM"
  }
  ```

---

## Configuration

### Change API URL

Edit `wwwroot/index.html` and update the `API_BASE_URL` constant:

```javascript
const API_BASE_URL = 'http://localhost:5013/api';
```

If you're running the backend on a different port or machine, update this URL accordingly.

---

## Browser Extension Integration

The included browser extension (`recommendation engine/`) tracks your browsing activity and sends it to the backend.

### Install the Extension

#### Chrome/Edge/Brave
1. Open `chrome://extensions/`
2. Enable "Developer mode"
3. Click "Load unpacked"
4. Select the `recommendation engine` folder

#### Firefox
1. Open `about:debugging#/runtime/this-firefox`
2. Click "Load Temporary Add-on"
3. Select `manifest.json` from the `recommendation engine` folder

### Extension Features

- Automatically captures page visits
- Sends data to `http://localhost:5013/api/events/browsing`
- Filters out local files and browser internal pages
- Minimal resource usage
- Privacy-first (all data stays local)

---

## Project Structure

```
local-recomendation-engine/
├── Controllers/
│   ├── EventsController.cs      # Browsing events API
│   └── InsightsController.cs    # Analytics API
├── Models/
│   └── BrowsingEvent.cs         # Data model
├── Data/
│   └── AppDbContext.cs          # Database context
├── wwwroot/
│   └── index.html               # Dashboard UI
├── recommendation engine/
│   ├── manifest.json            # Extension config
│   └── background.js            # Extension logic
├── Program.cs                   # App configuration
└── app.db                       # SQLite database
```

---

## Design Philosophy

This dashboard is designed to be:

- **Minimal** - No feature bloat, only what matters
- **Fast** - Loads in < 1 second, insights visible immediately
- **Clean** - Lots of whitespace, readable typography
- **Personal** - Feels like your own analytics tool
- **Private** - All data stays on your machine

---

## Customization

### Colors

The dashboard uses Tailwind CSS. To change colors, edit the classes in `wwwroot/index.html`:

- Primary: `blue-500`, `blue-600`
- Secondary: `gray-100`, `gray-200`
- Accent: `purple-500`

### Auto-Refresh Interval

Change the refresh interval at the bottom of `index.html`:

```javascript
// Refresh every 60 seconds instead of 30
setInterval(loadDashboard, 60000);
```

### Number of Activities Shown

Edit the `renderActivityFeed` function:

```javascript
${events.slice(0, 20).map(e => `  // Show 20 instead of 10
```

---

## Tech Stack

**Backend:**
- .NET 10.0 (ASP.NET Core)
- Entity Framework Core
- SQLite

**Frontend:**
- Vanilla JavaScript (no frameworks)
- Tailwind CSS (via CDN)
- Modern HTML5

**Browser Extension:**
- Manifest V3
- Vanilla JavaScript

---

## Development

### Add More Insights

Edit `Controllers/InsightsController.cs` and add new endpoints:

```csharp
[HttpGet("most-active-day")]
public async Task<IActionResult> GetMostActiveDay()
{
    // Your logic here
    return Ok(result);
}
```

### Enhance UI

Edit `wwwroot/index.html` and add new sections in the main grid:

```html
<div class="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
    <h2 class="text-lg font-semibold">New Section</h2>
    <!-- Your content -->
</div>
```

---

## Troubleshooting

### Dashboard shows "No activity yet"

**Cause:** No browsing events in the database

**Solution:** 
1. Install and activate the browser extension
2. Browse some websites
3. Refresh the dashboard

### API returns 404

**Cause:** Backend not running

**Solution:**
```bash
dotnet run
```

### CORS errors in console

**Cause:** Frontend and backend on different ports

**Solution:** Update `API_BASE_URL` in `index.html` to match your backend URL

### Dashboard shows errors or empty data

**Cause:** API endpoints returning errors or no data available

**Solution:** 
1. Check the browser console for error messages
2. Verify backend is running on the correct port
3. Ensure you have browsing data in the database

---

## Privacy & Security

- **All data stays local** - No external services
- **No tracking** - No analytics, no telemetry
- **No authentication** - Designed for single-user local use
- **SQLite database** - File-based, portable, easy to delete

⚠️ **Note:** This tool is designed for personal, local use only. Do not expose it to the internet without adding proper authentication and security measures.

---

## License

This is a personal project. Feel free to modify and use as you wish.

---

## Future Ideas

- Export data as JSON/CSV
- Filter by date range
- Search functionality
- More advanced analytics (hourly patterns, weekly trends)
- Category tagging
- Bookmark integration
- Dark mode toggle

---

## Contributing

This is a personal tool, but suggestions are welcome! Feel free to fork and customize for your needs.
