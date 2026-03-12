---
name: facades-edit-document
description: C# examples for facades-edit-document using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-edit-document

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-edit-document** category.
This folder contains standalone C# examples for facades-edit-document operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-edit-document**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (58/60 files) ← category-specific
- `using Aspose.Pdf;` (35/60 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (8/60 files)
- `using Aspose.Pdf.Text;` (4/60 files)
- `using Aspose.Pdf.Devices;` (1/60 files)
- `using Aspose.Pdf.Drawing;` (1/60 files)
- `using System;` (60/60 files)
- `using System.IO;` (57/60 files)
- `using System.Drawing;` (8/60 files)
- `using System.Collections.Generic;` (6/60 files)
- `using System.Collections;` (2/60 files)
- `using System.Drawing.Imaging;` (1/60 files)
- `using System.Linq;` (1/60 files)
- `using System.Reflection;` (1/60 files)

## Common Code Pattern

Most files in this category use `PdfContentEditor` from `Aspose.Pdf.Facades`:

```csharp
PdfContentEditor tool = new PdfContentEditor();
tool.BindPdf("input.pdf");
// ... PdfContentEditor operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [add-annotations-to-an-existing-pdf-document-programmatically...](./add-annotations-to-an-existing-pdf-document-programmatically-ensuring-they-are-saved-within-the-ori.cs) | `TextAnnotation`, `LinkAnnotation`, `GoToURIAction` | Add annotations to an existing pdf document programmatically ensuring they ar... |
| [add-annotations-to-pdf-documents-according-to-the-example-ma...](./add-annotations-to-pdf-documents-according-to-the-example-maintaining-proper-positioning-and-stylin.cs) | `TextAnnotation`, `LinkAnnotation`, `GoToURIAction` | Add annotations to pdf documents according to the example maintaining proper ... |
| [add-highlight-annotations-to-a-pdf-document-to-emphasize-sel...](./add-highlight-annotations-to-a-pdf-document-to-emphasize-selected-text-ranges-programmatically-using.cs) | `PdfAnnotationEditor`, `HighlightAnnotation` | Add highlight annotations to a pdf document to emphasize selected text ranges... |
| [add-javascript-actions-to-a-pdf-document-to-enable-interacti...](./add-javascript-actions-to-a-pdf-document-to-enable-interactive-functionality-within-the-file.cs) | `PdfContentEditor` | Add javascript actions to a pdf document to enable interactive functionality ... |
| [analyze-the-pdf-nested-bookmarks-introductory-section-ensuri...](./analyze-the-pdf-nested-bookmarks-introductory-section-ensuring-an-accurate-description-of-hierarchy.cs) | `PdfBookmarkEditor` | Analyze the pdf nested bookmarks introductory section ensuring an accurate de... |
| [attach-external-files-to-a-pdf-document-ensuring-proper-embe...](./attach-external-files-to-a-pdf-document-ensuring-proper-embedding-and-accessibility-within-the-pdf.cs) | `PdfContentEditor` | Attach external files to a pdf document ensuring proper embedding and accessi... |
| [configure-viewer-preferences-on-an-existing-pdf-file-pdf-to-...](./configure-viewer-preferences-on-an-existing-pdf-file-pdf-to-control-how-it-opens-and-displays.cs) | `PdfContentEditor` | Configure viewer preferences on an existing pdf file pdf to control how it op... |
| [create-hierarchical-bookmarks-within-a-pdf-document-to-enabl...](./create-hierarchical-bookmarks-within-a-pdf-document-to-enable-nested-navigation-structures-for-impro.cs) | `PdfBookmarkEditor` | Create hierarchical bookmarks within a pdf document to enable nested navigati... |
| [delete-all-embedded-attachments-from-a-pdf-file-while-mainta...](./delete-all-embedded-attachments-from-a-pdf-file-while-maintaining-document-integrity-and-metadata-co.cs) | `PdfContentEditor` | Delete all embedded attachments from a pdf file while maintaining document in... |
| [embed-a-specified-image-onto-a-designated-pdf-page-while-pre...](./embed-a-specified-image-onto-a-designated-pdf-page-while-preserving-original-image-dimensions.cs) | `PdfFileMend` | Embed a specified image onto a designated pdf page while preserving original ... |
| [embed-one-or-more-images-onto-a-specific-pdf-page-maintainin...](./embed-one-or-more-images-onto-a-specific-pdf-page-maintaining-original-dimensions-and-format.cs) | `PdfFileMend` | Embed one or more images onto a specific pdf page maintaining original dimens... |
| [employ-only-the-annotation-types-supported-by-the-pdf-format...](./employ-only-the-annotation-types-supported-by-the-pdf-format-when-adding-document-annotations.cs) | `PdfContentEditor` | Employ only the annotation types supported by the pdf format when adding docu... |
| [examine-the-documentation-covering-javascript-actions-within...](./examine-the-documentation-covering-javascript-actions-within-pdf-files-to-understand-supported-funct.cs) | `PdfContentEditor` | Examine the documentation covering javascript actions within pdf files to und... |
| [examine-the-introductory-pdf-attachment-guide-to-understand-...](./examine-the-introductory-pdf-attachment-guide-to-understand-handling-and-manipulation-of-document-at.cs) | `PdfExtractor`, `PdfContentEditor` | Examine the introductory pdf attachment guide to understand handling and mani... |
| [examine-the-pdf-annotation-types-overview-to-understand-supp...](./examine-the-pdf-annotation-types-overview-to-understand-supported-annotation-categories-and-their-pr.cs) | `PdfAnnotationEditor` | Examine the pdf annotation types overview to understand supported annotation ... |
| [examine-the-pdf-editing-inheritance-hierarchy-to-understand-...](./examine-the-pdf-editing-inheritance-hierarchy-to-understand-class-relationships-and-extension-points.cs) | `PdfContentEditor` | Examine the pdf editing inheritance hierarchy to understand class relationshi... |
| [examine-the-pdf-image-extraction-and-stamp-positioning-overv...](./examine-the-pdf-image-extraction-and-stamp-positioning-overview-to-understand-available-apis-and-usa.cs) | `PdfExtractor`, `PdfFileStamp` | Examine the pdf image extraction and stamp positioning overview to understand... |
| [examine-the-pdf-viewer-preferences-overview-to-understand-su...](./examine-the-pdf-viewer-preferences-overview-to-understand-supported-rendering-and-interaction-option.cs) | `PdfContentEditor` | Examine the pdf viewer preferences overview to understand supported rendering... |
| [examine-the-pdf-viewer-preferences-retrieval-overview-to-und...](./examine-the-pdf-viewer-preferences-retrieval-overview-to-understand-available-settings-and-configura.cs) | `PdfContentEditor` | Examine the pdf viewer preferences retrieval overview to understand available... |
| [extract-an-image-from-a-pdf-and-reposition-a-stamp-annotatio...](./extract-an-image-from-a-pdf-and-reposition-a-stamp-annotation-within-the-same-document.cs) | `PdfContentEditor` | Extract an image from a pdf and reposition a stamp annotation within the same... |
| [extract-embedded-images-from-a-pdf-file-preserving-original-...](./extract-embedded-images-from-a-pdf-file-preserving-original-resolution-color-depth-and-file-forma.cs) | `PdfExtractor` | Extract embedded images from a pdf file preserving original resolution color ... |
| [generate-a-hierarchical-structure-of-nested-bookmarks-within...](./generate-a-hierarchical-structure-of-nested-bookmarks-within-a-pdf-document-programmatically-using-t.cs) | `PdfBookmarkEditor` | Generate a hierarchical structure of nested bookmarks within a pdf document p... |
| [implement-a-code-sample-that-replaces-specified-text-in-a-pd...](./implement-a-code-sample-that-replaces-specified-text-in-a-pdf-using-the-library-s-facade-methods.cs) | `PdfContentEditor` | Implement a code sample that replaces specified text in a pdf using the libra... |
| [implement-image-extraction-and-stamp-placement-in-pdf-files-...](./implement-image-extraction-and-stamp-placement-in-pdf-files-according-to-the-provided-sample-code.cs) | `PdfExtractor`, `PdfFileStamp` | Implement image extraction and stamp placement in pdf files according to the ... |
| [implement-javascript-actions-in-a-pdf-document-according-to-...](./implement-javascript-actions-in-a-pdf-document-according-to-the-supplied-example-demonstrating-even.cs) | `PdfContentEditor` | Implement javascript actions in a pdf document according to the supplied exam... |
| [implement-nested-pdf-bookmarks-according-to-the-provided-exa...](./implement-nested-pdf-bookmarks-according-to-the-provided-example-preserving-hierarchical-structure.cs) | `PdfBookmarkEditor` | Implement nested pdf bookmarks according to the provided example preserving h... |
| [implement-pdf-editing-functionalities-by-following-the-suppl...](./implement-pdf-editing-functionalities-by-following-the-supplied-example-code-and-best-practice-guide.cs) | `PdfContentEditor`, `LinkAnnotation`, `GoToURIAction` | Implement pdf editing functionalities by following the supplied example code ... |
| [implement-pdf-viewer-preference-settings-by-adhering-to-the-...](./implement-pdf-viewer-preference-settings-by-adhering-to-the-provided-example-code-snippet-demonstrat.cs) | `PdfContentEditor` | Implement pdf viewer preference settings by adhering to the provided example ... |
| [implement-the-pdf-attachment-handling-example-demonstrating-...](./implement-the-pdf-attachment-handling-example-demonstrating-how-to-add-retrieve-and-manage-embedd.cs) | `PdfContentEditor`, `PdfExtractor` | Implement the pdf attachment handling example demonstrating how to add retrie... |
| [include-the-pdf-editing-namespace-to-enable-.net-pdf-manipul...](./include-the-pdf-editing-namespace-to-enable-.net-pdf-manipulation-programmatically-within-your-appli.cs) | `PdfContentEditor` | Include the pdf editing namespace to enable .net pdf manipulation programmati... |
| ... | | *and 30 more files* |

## Category Statistics
- Total examples: 60

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.BDCProperties`
- `Aspose.Pdf.Facades.BDCProperties.E`
- `Aspose.Pdf.Facades.BDCProperties.Lang`
- `Aspose.Pdf.Facades.BDCProperties.MCID`

### Rules
- Create AutoFiller with parameterless constructor: new AutoFiller().
- Call AutoFiller.Save() to persist changes to the output file.
- AutoFiller implements IDisposable — wrap in a using block for deterministic cleanup.
- Configure AutoFiller by setting properties: UnFlattenFields, OutputStream, OutputStreams, InputStream, InputFileName.
- Create PdfFileSanitization with parameterless constructor: new PdfFileSanitization().

### Warnings
- AutoFiller is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- PdfFileSanitization is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- FontColor is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- BDCProperties is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- Facade is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-edit-document patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->
