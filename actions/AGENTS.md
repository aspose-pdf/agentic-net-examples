---
name: actions
description: C# examples for actions using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - actions

> **Actions** in PDF using C# / .NET -- **5** verified, compile-tested examples for **Aspose.PDF for .NET** 26.11.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **actions** category.
This folder contains standalone C# examples for actions operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **actions**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (5/5 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (4/5 files) ← category-specific
- `using System;` (5/5 files)
- `using System.IO;` (5/5 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [remove-open-action-from-pdf](./remove-open-action-from-pdf.cs) | Remove Open Action from PDF | `Document`, `OpenAction`, `Save` | Shows how to load a PDF with Aspose.Pdf, clear the document's OpenAction to prevent any automatic... |
| [set-open-action-goto-page-3](./set-open-action-goto-page-3.cs) | Set PDF Open Action to GoTo Page 3 with XYZ Destination | `Document`, `XYZExplicitDestination`, `GoToAction` | Demonstrates loading a PDF, creating an XYZExplicitDestination for page three, assigning it to a ... |
| [set-open-action-navigate-to-page-five](./set-open-action-navigate-to-page-five.cs) | Set Open Action to Navigate to Page Five | `Document`, `Page`, `XYZExplicitDestination` | Demonstrates how to load a PDF, create an XYZ explicit destination for page five, assign it to a ... |
| [set-openaction-goto-page-12](./set-openaction-goto-page-12.cs) | Set OpenAction to GoTo Page 12 | `Document`, `Page`, `GoToAction` | Loads a PDF, creates a GoToAction with an XYZExplicitDestination pointing to page 12, replaces th... |
| [set-openaction-goto-page-xyz-destination](./set-openaction-goto-page-xyz-destination.cs) | Set OpenAction to GoTo Page 10 with XYZ Destination | `Document`, `XYZExplicitDestination`, `GoToAction` | Loads a PDF, creates an explicit XYZ destination on page 10, wraps it in a GoToAction, assigns it... |

## Category Statistics
- Total examples: 5

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Annotations.Annotation`
- `Aspose.Pdf.Annotations.Border`
- `Aspose.Pdf.Annotations.DefaultAppearance`
- `Aspose.Pdf.Annotations.FreeTextAnnotation`
- `Aspose.Pdf.Annotations.GoToAction`
- `Aspose.Pdf.Annotations.GoToRemoteAction`
- `Aspose.Pdf.Annotations.GoToURIAction`
- `Aspose.Pdf.Annotations.LinkAnnotation`
- `Aspose.Pdf.Annotations.XYZExplicitDestination`
- `Aspose.Pdf.Color`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.FileSpecification`
- `Aspose.Pdf.GoToRemoteAction`
- `Aspose.Pdf.LaunchAction`
- `Aspose.Pdf.LocalHyperlink`

### Rules
- Set {doc}.OpenAction = null to remove any action that executes when the PDF is opened.
- Persist the changes by calling {doc}.Save({output_pdf}).
- Create a LinkAnnotation on {page} using a {rect}, optionally set its Border.Width to {int}, assign its Action to a new GoToURIAction({string_literal}) and add it to {page}.Annotations.
- Create a FreeTextAnnotation on {page} using a {rect}, set its Contents to {string_literal}, define its DefaultAppearance with a {font} of size {int} and {color}, assign a Border (can reuse the same Border object) and add it to {page}.Annotations.
- After modifying annotations, save the {doc} to {output_pdf}.

### Warnings
- The exact type of the OpenAction property is not captured; it is assumed to be assignable from null.
- The same Border instance is reused for multiple annotations; modifying it later will affect all annotations that share it.
- GoToURIAction expects a fully qualified URL (including scheme, e.g., "https://www.aspose.com"); providing only a domain may lead to navigation issues.
- The exact namespace for GoToRemoteAction is assumed to be Aspose.Pdf; it may reside in a sub‑namespace such as Aspose.Pdf.Actions.
- The example assumes the LinkAnnotation's Action is always a GoToURIAction; other action types (e.g., GoToRemoteAction) would require additional handling.

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for actions patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-23 | Run: `20261123_014539_8ca006`
<!-- AUTOGENERATED:END -->
