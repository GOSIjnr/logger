# Logger – Product & Architecture Overview

## 1. Product Vision

**Logger** is a SaaS incident tracking tool designed to replace spreadsheets with a more reliable, structured, and scalable system — without losing the familiarity and flexibility that spreadsheet users value.

The goal is to:
- Capture incidents (logs) in a serious, operational tool
- Feel immediately familiar to spreadsheet users
- Avoid the chaos of free-form spreadsheets
- Enable analytics, accountability, and growth over time

Logger is **org-centric**, not user-centric.

---

## 2. Target Users

### Primary Market

- Teams currently using Excel / Google Sheets to track:
    - Incidents
    - Outages
    - Operational issues
    - Customer-impacting events

### Secondary Market

- Teams already using tools like Jira/Notion but looking for:
    - Less overhead
    - More focus
    - Cleaner operational UX

---

## 3. Core Mental Model

> **An organization contains multiple spreadsheet-like log sheets.**

- Each **Sheet** behaves like a spreadsheet page
- Sheets are isolated from each other
- Incidents (rows) belong to exactly one sheet

This mirrors how spreadsheet users already think:

- One file → many tabs
- Each tab → one purpose

---

## 4. Sheets (Key Concept)

### What a Sheet Is

A **Sheet** is:

- A spreadsheet-style table
- With fixed required columns
- And optional custom columns
- Holding its own incidents

Examples:

- Security Incidents
- Customer Outages
- Internal Ops Logs

---

## 5. Required (System) Columns

Every incident in every sheet has these columns:

- Incident ID
- Status
- Severity
- Created At
- Resolved At
- Assignee (optional MVP)

These are:

- Immutable
- Queryable
- Used for analytics

---

## 6. Custom Columns (Controlled Flexibility)

### Philosophy

> Spreadsheet familiarity **without spreadsheet chaos**.

Custom columns are:

- **Sheet-level**, not org-level
- Typed and structured
- Defined once, applied to all rows in that sheet

### Supported Types (MVP)

- Short text
- Number
- Boolean
- Date
- Dropdown (enum)

### Not Allowed

- Free-form per-row schema
- Formulas
- Nested objects
- Unlimited custom fields

---

## 7. UI / UX Model

### Table-First Design

- The sheet table is the primary interface
- Dense rows, fast scanning
- Optimized for keyboard + mouse

### Sidebar Editing (Primary Edit Surface)

- Clicking a row opens a slide-in sidebar
- Sidebar is used for:
    - Creating new incidents
    - Editing full incident details
    - Editing custom fields

Table stays visible for context.

---

## 8. Inline Editing Rules

Allowed inline (safe fields only):

- Status
- Severity
- Boolean fields
- Dropdown fields

Not inline:

- Long text
- Notes
- Timeline / history

---

## 9. Empty Cells & Consistency

- All columns exist for all rows in a sheet
- Empty values show as:
    - `—` or `Not set`
- No row-level schema differences

This preserves spreadsheet expectations.

---

## 10. Sheet Switching UX

- Sheet selector (dropdown or tab-style)
- Instant switching
- No page reloads

Feels like switching spreadsheet tabs.

---

## 11. Views vs Sheets (Intentional Choice)

### MVP Choice

- **Sheets**, not views
- Each sheet owns its data and schema

### Future (v2+)

- Views can be added later:
    - Filters
    - Column visibility
    - Analytics

Sheets are the foundation.

---

## 12. Data Architecture (High-Level)

### Core Tables

- Organizations
- Sheets
- Incidents

### Custom Field Tables

- SheetFieldDefinitions
- SheetFieldValues (typed columns)

Custom data is **structured metadata**, not free JSON.

---

## 13. Analytics Philosophy

Because data is typed and structured:
- Sorting works
- Filtering works
- Aggregations work
- Charts are trustworthy

Analytics is a differentiator.

---

## 14. Monetization Philosophy

- Monetize at **org level**, not user level
- Respectable limits, not paywalls

Possible levers:

- Number of sheets
- Incidents per sheet
- Custom columns per sheet
- Analytics depth

Free plan remains usable.

---

## 15. Design Language

**Operational Minimalism**

- Terminal-inspired
- Monochrome base
- Low-saturation semantic colors
- Blocky buttons
- Minimal animation
- Calm, neutral copy

Serious, tool-like, trustworthy.

---

## 16. MVP Summary

MVP includes:

- Organizations
- Multiple sheets per org
- Required columns everywhere
- Custom columns per sheet (limited)
- Table-first UI
- Sidebar editing
- Pagination + server-side queries

No overengineering.

---

## 17. Long-Term Direction

- Views on top of sheets
- Cross-sheet analytics
- Audit trails
- Incident types
- Enterprise controls

Foundation remains the same.

---

## 18. One-Sentence Anchor

> **Logger replaces spreadsheets by keeping what works and fixing what breaks.**