---
name: facades-metadata
description: C# examples for facades-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-metadata

> **Facades metadata** in PDF using C# / .NET -- **40** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-metadata** category.
This folder contains standalone C# examples for facades-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (40/40 files) ← category-specific
- `using Aspose.Pdf;` (7/40 files)
- `using Aspose.Pdf.Tagged;` (1/40 files)
- `using Aspose.Pdf.Text;` (1/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (37/40 files)
- `using System.Collections.Generic;` (4/40 files)
- `using System.Globalization;` (2/40 files)
- `using System.Text.Json;` (2/40 files)
- `using System.Threading.Tasks;` (2/40 files)
- `using System.Linq;` (1/40 files)
- `using System.Text.RegularExpressions;` (1/40 files)

## Common Code Pattern

Most files in this category use `PdfFileInfo` from `Aspose.Pdf.Facades`:

```csharp
PdfFileInfo tool = new PdfFileInfo();
tool.BindPdf("input.pdf");
// ... PdfFileInfo operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-custom-metadata-reviewedby-to-pdf](./add-custom-metadata-reviewedby-to-pdf.cs) | Add Custom Metadata 'ReviewedBy' to PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates loading a PDF with PdfFileInfo, setting a custom metadata field using SetMetaInfo, a... |
| [add-custom-metadata-to-pdf](./add-custom-metadata-to-pdf.cs) | Add Custom Metadata to PDF using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata entry to a PDF file with the PdfFileInfo facade and sav... |
| [add-custom-timestamp-metadata-to-pdf](./add-custom-timestamp-metadata-to-pdf.cs) | Add Custom Timestamp Metadata to PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to add a custom metadata field containing the current UTC timestamp to a PDF usi... |
| [add-department-metadata-to-pdfs](./add-department-metadata-to-pdfs.cs) | Add Custom 'Department' Metadata to PDFs | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Shows how to iterate over PDF files, set a custom metadata field named "Department" using Aspose.... |
| [backup-pdfs-update-metadata](./backup-pdfs-update-metadata.cs) | Backup PDFs and Update Metadata with PdfFileInfo | `PdfFileInfo`, `Title`, `Author` | Creates a backup copy of each PDF in a source folder, then modifies its metadata (title, author, ... |
| [check-custom-metadata-key](./check-custom-metadata-key.cs) | Check for Custom Metadata Key in PDF | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to verify the existence of a custom metadata entry (e.g., "Confidential") in a P... |
| [conditionally-update-pdf-author-metadata](./conditionally-update-pdf-author-metadata.cs) | Conditionally Update PDF Author Metadata | `PdfFileInfo`, `Author`, `SaveNewInfo` | Shows how to read a PDF's Author metadata with Aspose.Pdf.Facades, update it only when the field ... |
| [convert-pdf-to-pdfa-1b-with-pdffileinfo](./convert-pdf-to-pdfa-1b-with-pdffileinfo.cs) | Convert PDF to PDF/A‑1b and Save with PdfFileInfo | `Document`, `Convert`, `IsPdfaCompliant` | Creates a PDF in memory, converts it to PDF/A‑1b using Document.Convert, then uses the PdfFileInf... |
| [export-pdf-metadata-to-json](./export-pdf-metadata-to-json.cs) | Export PDF Metadata to JSON | `PdfFileInfo`, `BindPdf`, `Title` | Reads metadata from each PDF in a folder using Aspose.Pdf.Facades.PdfFileInfo and writes the coll... |
| [handle-readonly-file-errors-saving-pdf-metadata](./handle-readonly-file-errors-saving-pdf-metadata.cs) | Handle Read‑Only File Errors When Saving PDF Metadata | `PdfFileInfo`, `SaveNewInfo`, `Title` | Demonstrates how to modify PDF metadata with Aspose.Pdf.Facades.PdfFileInfo and gracefully handle... |
| [import-pdf-metadata-from-json](./import-pdf-metadata-from-json.cs) | Import PDF Metadata from JSON | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Shows how to read metadata values from a JSON file and apply them to a PDF using the PdfFileInfo ... |
| [merge-xmp-metadata-with-pdf-fileinfo](./merge-xmp-metadata-with-pdf-fileinfo.cs) | Merge XMP Metadata with PDF FileInfo | `PdfFileInfo`, `PdfXmpMetadata`, `XmpValue` | Shows how to combine standard PDF file information with XMP metadata, add a custom XMP property, ... |
| [preserve-pdf-metadata-add-version](./preserve-pdf-metadata-add-version.cs) | Preserve PDF Metadata and Add Version Field | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to keep all existing PDF metadata while adding or updating a custom "Version" fi... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata | `PdfFileInfo`, `Author` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to open a PDF, retrieve the Author metadata prope... |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata with PdfFileInfo | `PdfFileInfo`, `Creator` | Demonstrates how to open a PDF with Aspose.Pdf.Facades.PdfFileInfo, retrieve the Creator metadata... |
| [read-pdf-metadata-add-blank-page](./read-pdf-metadata-add-blank-page.cs) | Read PDF Metadata and Add a Blank Page with Aspose.Pdf | `PdfFileInfo`, `Document`, `Pages` | Demonstrates how to read basic PDF metadata using PdfFileInfo, obtain the page count, add a new b... |
| [read-pdf-metadata-safely](./read-pdf-metadata-safely.cs) | Read PDF Metadata Safely with PdfFileInfo | `PdfFileInfo`, `GetMetaInfo`, `Close` | Demonstrates opening a PDF using Aspose.Pdf.Facades.PdfFileInfo, retrieving standard and custom m... |
| [read-pdf-modification-date](./read-pdf-modification-date.cs) | Read and Format PDF Modification Date | `PdfFileInfo`, `ModDate` | Demonstrates how to use PdfFileInfo to retrieve a PDF's ModDate, parse the PDF date string, and d... |
| [read-pdf-title-metadata-log](./read-pdf-title-metadata-log.cs) | Read PDF Title Metadata and Write to Log | `PdfFileInfo`, `Title` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read the Title metadata from a PDF file... |
| [read-pdf-version-using-aspose-pdf-facade](./read-pdf-version-using-aspose-pdf-facade.cs) | Read PDF Version Using Aspose.Pdf Facade | `PdfFileInfo`, `GetPdfVersion` | Demonstrates how to initialize the PdfFileInfo facade, retrieve the PDF version string with GetPd... |
| [read-update-pdf-metadata](./read-update-pdf-metadata.cs) | Read and Update PDF Metadata with PdfFileInfo | `PdfFileInfo`, `Title`, `Author` | Demonstrates how to open a PDF, read its metadata using PdfFileInfo, modify fields such as title ... |
| [remove-custom-pdf-metadata-entry](./remove-custom-pdf-metadata-entry.cs) | Remove Custom PDF Metadata Entry Using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to clear a specific custom metadata field in a PDF by setting its value to empty... |
| [retrieve-custom-pdf-metadata-alphabetically](./retrieve-custom-pdf-metadata-alphabetically.cs) | Retrieve and Display Custom PDF Metadata Alphabetically | `PdfFileInfo`, `PdfXmpMetadata`, `BindPdf` | The example shows how to obtain all custom XMP metadata keys from a PDF, sort them alphabetically... |
| [retrieve-custom-pdf-metadata-projectcode](./retrieve-custom-pdf-metadata-projectcode.cs) | Retrieve Custom PDF Metadata (ProjectCode) | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read a custom metadata entry named "Pro... |
| [retrieve-pdf-keywords-metadata](./retrieve-pdf-keywords-metadata.cs) | Retrieve PDF Keywords Metadata | `PdfFileInfo`, `Keywords` | Shows how to load a PDF with Aspose.Pdf.Facades.PdfFileInfo and read the Keywords metadata proper... |
| [retrieve-reviewedby-metadata](./retrieve-reviewedby-metadata.cs) | Retrieve Custom PDF Metadata 'ReviewedBy' | `PdfFileInfo`, `GetMetaInfo` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to read a custom metadata property named 'Reviewe... |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set PDF Creator Metadata with PdfFileInfo | `PdfFileInfo`, `Creator`, `SaveNewInfo` | Demonstrates how to assign a custom Creator value to a PDF using the PdfFileInfo facade and persi... |
| [set-pdf-document-id-using-pdffileinfo](./set-pdf-document-id-using-pdffileinfo.cs) | Set PDF Document ID Using PdfFileInfo | `Document`, `PdfFileInfo`, `SetMetaInfo` | Demonstrates how to generate a GUID and store it as a custom metadata entry (DocumentId) in a PDF... |
| [set-pdf-keywords-metadata](./set-pdf-keywords-metadata.cs) | Set PDF Keywords Metadata with PdfFileInfo | `PdfFileInfo`, `Keywords`, `SaveNewInfo` | Demonstrates how to set the Keywords metadata field of a PDF using Aspose.Pdf.Facades.PdfFileInfo... |
| [set-pdf-language-property](./set-pdf-language-property.cs) | Set PDF Language Property Using PdfFileInfo | `PdfFileInfo`, `Document`, `ITaggedContent` | Demonstrates how to set the natural language of a PDF document to "en-US" using the PdfFileInfo f... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->
