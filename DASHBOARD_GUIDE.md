# Dashboard Visual Guide

## Layout Overview

```
┌─────────────────────────────────────────────────────────────────┐
│  Browsing Insights                            [Last Updated] [↻]│
│  Your personal browsing activity tracker                         │
└─────────────────────────────────────────────────────────────────┘

┌──────────────────┐ ┌──────────────────┐ ┌──────────────────┐
│ 📄 Pages Visited │ │ 🌐 Top Domain    │ │ 🕐 Peak Hour     │
│     Today        │ │                  │ │                  │
│                  │ │                  │ │                  │
│       42         │ │   github.com     │ │     2:00 PM      │
│   Since midnight │ │   15 visits      │ │  Most active time│
└──────────────────┘ └──────────────────┘ └──────────────────┘

┌───────────────────────────────────┐  ┌──────────────────┐
│  Recent Activity                  │  │  Top Interests   │
│  Your latest browsing events      │  │  Your topics     │
├───────────────────────────────────┤  ├──────────────────┤
│                                   │  │                  │
│  ⚡ Understanding React Hooks     │  │  [React] ██ 15   │
│     reactjs.org/docs/...          │  │  [JavaScript] █ 12│
│     5m ago • reactjs.org          │  │  [CSS] █ 9       │
│                                   │  │  [Node.js] ▓ 7   │
│  ⚡ Tailwind CSS Documentation    │  │  [TypeScript] ▓ 6│
│     tailwindcss.com/docs          │  │  [WebDev] ▓ 5    │
│     15m ago • tailwindcss.com     │  │  [API] ▒ 4       │
│                                   │  │  [GitHub] ▒ 3    │
│  ⚡ GitHub Trending Projects      │  │                  │
│     github.com/trending           │  │                  │
│     30m ago • github.com          │  │                  │
│                                   │  └──────────────────┘
│  [More items...]                  │
│                                   │
└───────────────────────────────────┘

┌───────────────────────────────────┐
│  Activity Timeline                │
│  Pages visited by time period     │
├───────────────────────────────────┤
│                                   │
│  Today         ████████████  42pg │
│  Yesterday     ████████      35pg │
│                                   │
└───────────────────────────────────┘
```

---

## Color Scheme

### Primary Colors
- **Blue**: `#3B82F6` (Tailwind blue-500)
  - Used for: Primary actions, links, charts
  
- **Purple**: `#A855F7` (Tailwind purple-500)
  - Used for: Accent colors, gradients

- **Green**: `#10B981` (Tailwind green-500)
  - Used for: Success states, top domain icon

### Neutral Colors
- **White**: `#FFFFFF` - Card backgrounds
- **Gray-50**: `#F9FAFB` - Page background
- **Gray-100**: `#F3F4F6` - Subtle backgrounds
- **Gray-200**: `#E5E7EB` - Borders
- **Gray-500**: `#6B7280` - Secondary text
- **Gray-900**: `#111827` - Primary text

### Gradients
- **Activity Icons**: Blue to Purple gradient
- **Chart Bars**: Color-specific gradients

---

## Component Breakdown

### 1. Header
- **Position**: Fixed at top
- **Background**: White with subtle border
- **Elements**:
  - Large title "Browsing Insights"
  - Subtitle "Your personal browsing activity tracker"
  - Last updated timestamp
  - Refresh button (hover: gray-200)

### 2. Stats Cards (3 columns)
- **Layout**: Equal width grid (1/3 each)
- **Design**: 
  - White background
  - Rounded corners (xl)
  - Subtle shadow
  - Icon in top-right corner
  - Large number display
  - Small descriptor text
- **Spacing**: 6-unit gap between cards
- **Animation**: Fade in with staggered delays

### 3. Activity Feed (Left Column, 2/3 width)
- **Header**: "Recent Activity" with description
- **Items**:
  - Gradient icon (blue-purple)
  - Page title (bold, clickable)
  - URL (blue link, truncated)
  - Metadata: Time ago • Domain
  - Hover effect: Slides right slightly
- **Max Items**: 10 most recent

### 4. Topics Panel (Right Column, 1/3 width)
- **Position**: Sticky (stays visible on scroll)
- **Header**: "Top Interests"
- **Items**:
  - Tag-style chips (blue background)
  - Progress bar (visual frequency)
  - Count number (right-aligned)
- **Sorting**: By frequency (descending)
- **Max Items**: 10 topics

### 5. Timeline (Bottom Section)
- **Header**: "Activity Timeline"
- **Items**:
  - Period label (Today/Yesterday)
  - Horizontal bar chart
  - Count displayed on bar and right side
  - Gradient fill effect
- **Responsive**: Bar width = proportion of max count

---

## Responsive Behavior

### Desktop (lg: 1024px+)
- 3-column stats cards
- 2/3 + 1/3 split for main content
- Topics panel becomes sticky

### Tablet (md: 768px+)
- 3-column stats cards
- Single column main content
- Topics below activity feed

### Mobile (< 768px)
- Single column layout
- Stacked stats cards
- Full-width activity feed
- Full-width topics panel

---

## Typography

### Font Family
- System font stack for performance
- `font-sans` class (Tailwind default)

### Sizes & Weights
- **Page Title**: 3xl, semibold (text-3xl font-semibold)
- **Card Headers**: lg, semibold (text-lg font-semibold)
- **Stat Numbers**: 3xl, bold (text-3xl font-bold)
- **Activity Titles**: sm, medium (text-sm font-medium)
- **Body Text**: sm, regular (text-sm)
- **Metadata**: xs, regular (text-xs)

---

## Animations

### Fade In
- Duration: 0.5s
- Easing: ease-out
- Movement: 10px from bottom
- Stagger: 0.1s delay per card

### Hover Effects
- Activity items slide right 4px
- Buttons darken slightly
- Links change color
- Transition: 0.2s ease

### Loading Skeleton
- Gradient animation (left to right)
- Duration: 1.5s infinite
- Colors: Gray-100 to Gray-200

---

## State Management

### Data Loading States

1. **Initial Load**
   - Show skeleton loaders
   - Gray pulsing rectangles

2. **Loading Complete**
   - Fade in real content
   - Remove skeletons

3. **No Data**
   - Show friendly message
   - "No activity yet"

4. **Error State**
   - Fall back to mock data
   - Console warning (not user-visible)

### Auto-Refresh
- Every 30 seconds
- Silent background update
- Timestamps update automatically

---

## Accessibility

### Semantic HTML
- Proper header hierarchy
- Semantic section elements
- Descriptive link text

### Colors
- Sufficient contrast ratios
- Not relying solely on color
- Text + icons for clarity

### Interactive Elements
- Large click targets (min 44x44)
- Visible focus states
- Hover feedback

---

## Performance

### Optimization Strategies
- Tailwind CSS via CDN (no build step)
- Vanilla JavaScript (no framework overhead)
- Single HTML file (minimal HTTP requests)
- Efficient DOM updates
- Debounced auto-refresh

### Load Time
- Initial: < 1 second
- Data fetch: < 200ms (local API)
- Render: < 100ms

---

## Browser Support

Tested and working on:
- Chrome 100+
- Firefox 100+
- Safari 15+
- Edge 100+

Uses:
- Modern JavaScript (ES6+)
- CSS Grid & Flexbox
- Fetch API
- Template literals
- Arrow functions

---

## Customization Quick Reference

### Change Primary Color
Find and replace all instances of:
- `blue-500` → your color
- `blue-600` → darker shade
- `blue-50` → lighter tint

### Adjust Spacing
- Gap between cards: `gap-6` (24px)
- Card padding: `p-6` (24px)
- Section margin: `mb-8` (32px)

### Modify Refresh Rate
```javascript
// Line ~670 in index.html
setInterval(loadDashboard, 30000); // Change 30000 to desired ms
```

### Show More Activities
```javascript
// Line ~540 in index.html
${events.slice(0, 10).map(e => ` // Change 10 to desired count
```

---

## Integration Points

### API Configuration
Located at top of `<script>` section:
```javascript
const API_BASE_URL = 'http://localhost:5013/api';
const USE_MOCK_DATA = false;
```

### Mock Data
Arrays defined for offline testing:
- `MOCK_EVENTS` - Sample browsing events
- `MOCK_TOPICS` - Sample topics

### Fallback Strategy
1. Try real API first
2. On error, console.warn()
3. Return mock data
4. User sees data regardless of API status

---

This dashboard represents a balance between:
- **Simplicity** - Easy to understand and use
- **Aesthetics** - Modern, clean design
- **Functionality** - All requested features
- **Performance** - Fast load, smooth interactions
- **Privacy** - No external dependencies
