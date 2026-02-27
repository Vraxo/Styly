# Development Pitfalls & Lessons Learned

This document tracks tricky bugs, failed approaches, and final solutions encountered during development to prevent regression.

---

## 1. The `NormalizeWhitespace` vs. Single-Line Initializer Conflict

### 🛑 The Problem

Roslyn's `NormalizeWhitespace()` is extremely aggressive. It assumes standard C# formatting conventions, which often implies expanding blocks and initializers to multiple lines.

When we force an initializer back to **SingleLine** (e.g., `new[] { 1 }`) inside a rewriter, `NormalizeWhitespace` has often already inserted indentation or newlines between the closing brace `}` and the next token (e.g., `)`, `;`, or `,`).

**Symptom:**
Input: `foreach (var x in new[] { 1 })`
Output: `foreach (var x in new[] { 1 }    )`  <-- Excess spacing artifacts.

### ❌ Failed Attempts

1. **Stripping Trivia in `InitializerRewriter`:** Failed because whitespace usually belongs to the *next* token's LeadingTrivia.
2. **Customizing `NormalizeWhitespace`:** Failed because the API lacks granularity.

### ✅ The Final Fix: `CleanupTokenSpacing` (Late-Stage Cleanup)

We implemented a global token cleanup pass at the end of the pipeline (`CodeFormatter.cs`) to aggressively strip whitespace between specific token pairs (e.g., `}` and `)` or `}` and `;`) if they appear on the same line.

---

## 2. Usings vs. Namespace Spacing (The "Drifting Newlines" Bug)

### 🛑 The Problem

When `UsingSorterRewriter` sorts directives, the nodes carry their original trivia (whitespace/newlines) to their new positions.

* If `using System;` moves from the bottom (where it had 2 trailing newlines) to the top, it creates a large gap at the start.
* If the next item `using System.Text;` moves up, it might keep its leading newline, adding to the gap.

**Symptom:**

```csharp
using System;
 // <--- Unwanted extra gap
using System.Text;
```

### ✅ The Fix

We treat the `UsingDirectiveSyntax` list as a completely reconstructed layout:

1. **Strip Leading Newlines** from all items (except the first, which inherits the file header).
2. **Force Trailing Newlines** on *every* item:
   * **Intermediate items:** Force exactly **1** newline.
   * **Last item:** Force exactly **2** newlines (to separate from the namespace).
