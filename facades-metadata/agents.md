---
name: facades-metadata
description: C# examples for facades-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-metadata

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-metadata** category.
This folder contains standalone C# examples for facades-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (58/59 files) ← category-specific
- `using Aspose.Pdf;` (39/59 files) ← category-specific
- `using System;` (59/59 files)
- `using System.IO;` (59/59 files)
- `using System.Text;` (4/59 files)
- `using System.Xml.Linq;` (4/59 files)
- `using System.Collections.Generic;` (3/59 files)
- `using System.Linq;` (3/59 files)
- `using System.Globalization;` (1/59 files)
- `using System.IO.Compression;` (1/59 files)
- `using System.Reflection;` (1/59 files)
- `using System.Runtime.InteropServices;` (1/59 files)
- `using System.Text.Json;` (1/59 files)
- `using System.Text.RegularExpressions;` (1/59 files)

## Common Code Pattern

Most files in this category use `PdfFileInfo` from `Aspose.Pdf.Facades`:

```csharp
PdfFileInfo tool = new PdfFileInfo();
tool.BindPdf("input.pdf");
// ... PdfFileInfo operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [adjust-metadata-properties-of-a-pdf-document-embedded-within...](./adjust-metadata-properties-of-a-pdf-document-embedded-within-an-ofd-file-using-the-provided-api.cs) | `OfdLoadOptions`, `PdfFileInfo` | Adjust metadata properties of a pdf document embedded within an ofd file usin... |
| [assign-pdf-metadata-fields-title-author-subject-and-keywords...](./assign-pdf-metadata-fields-title-author-subject-and-keywords-to-an-svg-document-using-the-appropr.cs) | `SvgLoadOptions`, `PdfFileInfo` | Assign pdf metadata fields title author subject and keywords to an svg docume... |
| [assign-pdf-metadata-fields-within-a-cgm-document-programmati...](./assign-pdf-metadata-fields-within-a-cgm-document-programmatically-using-the-available-api-methods.cs) | `CgmLoadOptions`, `PdfFileInfo` | Assign pdf metadata fields within a cgm document programmatically using the a... |
| [assign-pdf-metadata-including-title-author-subject-and-keywo...](./assign-pdf-metadata-including-title-author-subject-and-keywords-to-an-mht-document-using-the-prov.cs) | `MhtLoadOptions`, `PdfFileInfo` | Assign pdf metadata including title author subject and keywords to an mht doc... |
| [assign-pdf-metadata-properties-including-title-author-subjec...](./assign-pdf-metadata-properties-including-title-author-subject-and-keywords-directly-from-an-html.cs) | `HtmlLoadOptions`, `PdfFileInfo` | Assign pdf metadata properties including title author subject and keywords di... |
| [assign-title-author-subject-and-keyword-metadata-to-an-epub-...](./assign-title-author-subject-and-keyword-metadata-to-an-epub-file-using-pdf-metadata-properties.cs) | `PdfFileInfo` | Assign title author subject and keyword metadata to an epub file using pdf me... |
| [assign-title-author-subject-and-keyword-metadata-to-an-xps-d...](./assign-title-author-subject-and-keyword-metadata-to-an-xps-document-using-the-appropriate-api-cal.cs) | `PdfFileInfo`, `XpsSaveOptions` | Assign title author subject and keyword metadata to an xps document using the... |
| [assign-title-author-subject-and-keyword-metadata-to-the-pdf-...](./assign-title-author-subject-and-keyword-metadata-to-the-pdf-output-generated-from-a-cgm-document.cs) | `PdfFileInfo` | Assign title author subject and keyword metadata to the pdf output generated ... |
| [configure-pdf-metadata-fields-including-title-author-subject...](./configure-pdf-metadata-fields-including-title-author-subject-and-keywords-directly-inside-an-xsl.cs) | `XslFoLoadOptions`, `PdfFileInfo` | Configure pdf metadata fields including title author subject and keywords dir... |
| [configure-pdf-metadata-including-title-author-subject-and-ke...](./configure-pdf-metadata-including-title-author-subject-and-keywords-when-generating-a-pdf-from-a-t.cs) | `TeXLoadOptions` | Configure pdf metadata including title author subject and keywords when gener... |
| [extract-pdf-metadata-embedded-in-a-cgm-file-and-present-it-i...](./extract-pdf-metadata-embedded-in-a-cgm-file-and-present-it-in-a-structured-format.cs) | `PdfFileInfo` | Extract pdf metadata embedded in a cgm file and present it in a structured fo... |
| [extract-pdf-metadata-title-author-subject-and-keywords-and-a...](./extract-pdf-metadata-title-author-subject-and-keywords-and-assign-these-properties-to-an-ofd-docu.cs) | `PdfFileInfo`, `OfdLoadOptions` | Extract pdf metadata title author subject and keywords and assign these prope... |
| [modify-pdf-document-metadata-using-html-controls-to-programm...](./modify-pdf-document-metadata-using-html-controls-to-programmatically-set-properties-before-rendering.cs) | `PdfFileInfo` | Modify pdf document metadata using html controls to programmatically set prop... |
| [populate-pdf-metadata-fields-title-author-subject-and-keywor...](./populate-pdf-metadata-fields-title-author-subject-and-keywords-using-values-specified-in-a-markdo.cs) | `PdfFileInfo` | Populate pdf metadata fields title author subject and keywords using values s... |
| [programmatically-assign-pdf-metadata-fields-title-author-sub...](./programmatically-assign-pdf-metadata-fields-title-author-subject-and-keywords-to-a-generated-post.cs) | `PdfFileInfo` | Programmatically assign pdf metadata fields title author subject and keywords... |
| [programmatically-retrieve-pdf-document-metadata-such-as-titl...](./programmatically-retrieve-pdf-document-metadata-such-as-title-and-author-from-within-an-xps-file.cs) | `XpsLoadOptions`, `PdfFileInfo` | Programmatically retrieve pdf document metadata such as title and author from... |
| [programmatically-update-pdf-document-metadata-properties-dur...](./programmatically-update-pdf-document-metadata-properties-during-svg-document-processing-using-the-.n.cs) | `PdfFileInfo`, `SvgSaveOptions` | Programmatically update pdf document metadata properties during svg document ... |
| [read-pdf-document-properties-and-display-them-programmatical...](./read-pdf-document-properties-and-display-them-programmatically-within-an-html-page-using-.net-librar.cs) | `PdfFileInfo`, `StringBuilder` | Read pdf document properties and display them programmatically within an html... |
| [read-pdf-document-properties-and-output-them-to-an-md-file-f...](./read-pdf-document-properties-and-output-them-to-an-md-file-for-documentation-purposes.cs) | `PdfFileInfo`, `StringBuilder` | Read pdf document properties and output them to an md file for documentation ... |
| [read-pdf-metadata-from-an-epub-file-and-expose-the-propertie...](./read-pdf-metadata-from-an-epub-file-and-expose-the-properties-via-.net-api.cs) | `EpubLoadOptions`, `PdfFileInfo` | Read pdf metadata from an epub file and expose the properties via .net api |
| [read-the-metadata-properties-of-a-pdf-document-embedded-with...](./read-the-metadata-properties-of-a-pdf-document-embedded-within-a-cgm-file-programmatically.cs) | `PdfFileInfo` | Read the metadata properties of a pdf document embedded within a cgm file pro... |
| [retrieve-a-pdf-s-title-author-subject-and-keywords-and-embed...](./retrieve-a-pdf-s-title-author-subject-and-keywords-and-embed-them-into-an-svg-document.cs) | `PdfFileInfo` | Retrieve a pdf s title author subject and keywords and embed them into an svg... |
| [retrieve-comprehensive-metadata-page-count-and-structural-de...](./retrieve-comprehensive-metadata-page-count-and-structural-details-from-a-pdf-document-using-the-.n.cs) | `PdfFileInfo` | Retrieve comprehensive metadata page count and structural details from a pdf ... |
| [retrieve-metadata-properties-from-a-pdf-document-using-the-l...](./retrieve-metadata-properties-from-a-pdf-document-using-the-library-s-api-and-expose-them-programmati.cs) | `PdfFileInfo` | Retrieve metadata properties from a pdf document using the library s api and ... |
| [retrieve-metadata-title-author-subject-keywords-from-a-pdf-f...](./retrieve-metadata-title-author-subject-keywords-from-a-pdf-file-and-embed-it-into-a-pcl-documen.cs) | `PdfFileInfo` | Retrieve metadata title author subject keywords from a pdf file and embed it ... |
| [retrieve-pdf-document-metadata-within-an-xslfo-template-to-d...](./retrieve-pdf-document-metadata-within-an-xslfo-template-to-dynamically-populate-property-values-duri.cs) | `PdfFileInfo`, `XslFoLoadOptions` | Retrieve pdf document metadata within an xslfo template to dynamically popula... |
| [retrieve-pdf-document-properties-and-write-them-to-an-xml-fi...](./retrieve-pdf-document-properties-and-write-them-to-an-xml-file-for-integration-with-other-systems.cs) | `PdfFileInfo` | Retrieve pdf document properties and write them to an xml file for integratio... |
| [retrieve-pdf-document-properties-embedded-within-an-ofd-file...](./retrieve-pdf-document-properties-embedded-within-an-ofd-file-programmatically-using-the-appropriate.cs) | `OfdLoadOptions`, `PdfFileInfo` | Retrieve pdf document properties embedded within an ofd file programmatically... |
| [retrieve-pdf-document-properties-within-a-postscript-ps-file...](./retrieve-pdf-document-properties-within-a-postscript-ps-file-using-appropriate-.net-apis-and-integ.cs) | `PsLoadOptions`, `PdfFileInfo` | Retrieve pdf document properties within a postscript ps file using appropriat... |
| [retrieve-pdf-metadata-fields-including-title-author-subject-...](./retrieve-pdf-metadata-fields-including-title-author-subject-and-keywords-from-an-epub-document-pr.cs) | `EpubLoadOptions`, `PdfFileInfo` | Retrieve pdf metadata fields including title author subject and keywords from... |
| ... | | *and 29 more files* |

## Category Statistics
- Total examples: 59

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
- Review code examples in this folder for facades-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->
