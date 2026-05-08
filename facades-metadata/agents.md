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

- `using Aspose.Pdf.Facades;` (40/40 files) ← category-specific
- `using Aspose.Pdf;` (5/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (36/40 files)
- `using System.Collections.Generic;` (3/40 files)
- `using System.Text.Json;` (2/40 files)
- `using System.Threading.Tasks;` (2/40 files)
- `using System.Globalization;` (1/40 files)
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
| [add-custom-metadata-to-pdf](./add-custom-metadata-to-pdf.cs) | Add Custom Metadata Field to PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Shows how to set a custom metadata entry (ReviewedBy) on an existing PDF using Aspose.Pdf.Facades... |
| [add-department-metadata-to-pdfs](./add-department-metadata-to-pdfs.cs) | Add Custom Department Metadata to PDFs | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to loop through PDF files, set a custom metadata field called "Department" using... |
| [add-lastupdated-timestamp-metadata](./add-lastupdated-timestamp-metadata.cs) | Add LastUpdated Timestamp Metadata to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata field containing the current UTC timestamp to a PDF usi... |
| [add-or-update-custom-pdf-metadata](./add-or-update-custom-pdf-metadata.cs) | Add or Update Custom PDF Metadata While Preserving Existing ... | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to add or update a custom metadata field (... |
| [add-projectcode-metadata-to-pdf](./add-projectcode-metadata-to-pdf.cs) | Add Custom ProjectCode Metadata to PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Shows how to load a PDF, add a custom metadata entry named "ProjectCode" using SetMetaInfo, and s... |
| [audit-pdf-metadata-to-csv](./audit-pdf-metadata-to-csv.cs) | Audit and Update PDF Metadata with CSV Log | `PdfFileInfo`, `Title`, `Author` | Demonstrates how to read, modify, and save PDF metadata using Aspose.Pdf.Facades, then record the... |
| [backup-pdfs-update-metadata](./backup-pdfs-update-metadata.cs) | Backup PDFs and Update Metadata with PdfFileInfo | `PdfFileInfo`, `BindPdf`, `SaveNewInfo` | Creates a backup copy of each PDF in a source folder, then uses Aspose.Pdf.Facades.PdfFileInfo to... |
| [clear-custom-pdf-metadata-field](./clear-custom-pdf-metadata-field.cs) | Clear Custom PDF Metadata Field | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to remove a specific custom metadata entry from a PDF by setting its value to an... |
| [conditionally-update-pdf-author-metadata](./conditionally-update-pdf-author-metadata.cs) | Conditionally Update PDF Author Metadata | `PdfFileInfo`, `Author`, `SaveNewInfo` | Shows how to read a PDF's Author metadata with Aspose.Pdf.Facades, set it only when the field is ... |
| [convert-pdf-to-pdfa-1b-with-pdffileinfo](./convert-pdf-to-pdfa-1b-with-pdffileinfo.cs) | Convert PDF to PDF/A‑1b and enforce compliance via PdfFileIn... | `Document`, `Convert`, `IsPdfaCompliant` | The example loads a PDF, converts it to PDF/A‑1b using Document.Convert, checks the compliance fl... |
| [dispose-pdf-objects-using-blocks](./dispose-pdf-objects-using-blocks.cs) | Dispose PDF Objects Using Using Blocks | `PdfFileInfo`, `Document`, `NumberOfPages` | Shows how to properly dispose Aspose.Pdf PdfFileInfo and Document objects with using statements, ... |
| [export-pdf-metadata-to-json](./export-pdf-metadata-to-json.cs) | Export PDF Metadata to JSON | `PdfFileInfo`, `BindPdf` | Shows how to read metadata from PDF files in a directory using Aspose.Pdf.Facades.PdfFileInfo and... |
| [handle-readonly-file-errors-saving-pdf-metadata](./handle-readonly-file-errors-saving-pdf-metadata.cs) | Handle Read‑Only File Errors When Saving PDF Metadata | `PdfFileInfo`, `Title`, `Author` | Demonstrates updating PDF metadata with Aspose.Pdf.Facades.PdfFileInfo and handling IOException c... |
| [list-pdf-custom-metadata-alphabetically](./list-pdf-custom-metadata-alphabetically.cs) | List PDF Custom Metadata Alphabetically | `PdfFileInfo`, `BindPdf`, `GetMetaInfo` | The example reads a PDF file, extracts all custom XMP metadata keys, sorts them alphabetically, a... |
| [merge-xmp-metadata-with-pdf-file-info](./merge-xmp-metadata-with-pdf-file-info.cs) | Merge XMP Metadata with PDF File Info | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add custom XMP properties to a PDF, then combine them with standard document ... |
| [read-custom-pdf-metadata-safely](./read-custom-pdf-metadata-safely.cs) | Read Custom PDF Metadata Safely | `PdfFileInfo`, `GetMetaInfo` | Demonstrates using Aspose.Pdf.Facades.PdfFileInfo to check for a custom metadata key "Confidentia... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata | `PdfFileInfo`, `Author` | Shows how to retrieve the Author metadata from a PDF file using Aspose.Pdf.Facades.PdfFileInfo an... |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata using PdfFileInfo | `PdfFileInfo`, `Creator` | Shows how to open a PDF with Aspose.Pdf.Facades.PdfFileInfo and retrieve the Creator metadata pro... |
| [read-pdf-metadata-handle-empty-values](./read-pdf-metadata-handle-empty-values.cs) | Read PDF Metadata and Handle Empty Values | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to retrieve standard and custom PDF metadata using Aspose.Pdf.Facades.PdfFileInf... |
| [read-pdf-modification-date](./read-pdf-modification-date.cs) | Read PDF Modification Date with PdfFileInfo | `PdfFileInfo`, `IsPdfFile`, `ModDate` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to verify a PDF, extract its ModDate string, conv... |
| [read-pdf-title-metadata-to-log](./read-pdf-title-metadata-to-log.cs) | Read PDF Title Metadata and Write to Log | `PdfFileInfo`, `Title` | Demonstrates how to use the PdfFileInfo facade to read the Title metadata from a PDF document and... |
| [read-pdf-version-using-aspose-pdf-facade](./read-pdf-version-using-aspose-pdf-facade.cs) | Read PDF Version Using Aspose.Pdf Facade | `PdfFileInfo`, `GetPdfVersion` | Demonstrates how to obtain the PDF version string from a PDF file using the Aspose.Pdf.Facades.Pd... |
| [read-update-pdf-metadata](./read-update-pdf-metadata.cs) | Read and Update PDF Metadata with PdfFileInfo | `PdfFileInfo`, `BindPdf`, `Title` | Demonstrates how to bind a PDF file, read its metadata such as title, author, page count and vers... |
| [retrieve-custom-pdf-metadata-projectcode](./retrieve-custom-pdf-metadata-projectcode.cs) | Retrieve Custom PDF Metadata (ProjectCode) | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read a custom metadata entry named "Pro... |
| [retrieve-pdf-keywords-metadata](./retrieve-pdf-keywords-metadata.cs) | Retrieve PDF Keywords Metadata with PdfFileInfo | `PdfFileInfo`, `IsPdfFile`, `Keywords` | Demonstrates how to open a PDF file using Aspose.Pdf.Facades.PdfFileInfo, verify it is a PDF, and... |
| [retrieve-reviewedby-metadata](./retrieve-reviewedby-metadata.cs) | Retrieve Custom PDF Metadata (ReviewedBy) Using PdfFileInfo | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to read a custom metadata field named "ReviewedBy" from a PDF file using Aspose.... |
| [set-custom-document-id-metadata](./set-custom-document-id-metadata.cs) | Set Custom Document ID Metadata in PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to generate a GUID and store it as a custom metadata entry (DocumentID) in a PDF... |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set PDF Creator Metadata Using PdfFileInfo | `PdfFileInfo`, `Creator`, `SaveNewInfo` | Demonstrates how to assign a custom Creator value to a PDF file using Aspose.Pdf's PdfFileInfo fa... |
| [set-pdf-keywords-metadata](./set-pdf-keywords-metadata.cs) | Set PDF Keywords Metadata with PdfFileInfo | `PdfFileInfo`, `Keywords`, `SaveNewInfo` | Demonstrates how to assign keyword metadata to a PDF using Aspose.Pdf.Facades.PdfFileInfo, save t... |
| [set-pdf-language-property](./set-pdf-language-property.cs) | Set PDF Language Property Using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to set the language identifier of a PDF to "en-US" using the PdfFileInfo facade ... |
| ... | | | *and 10 more files* |

## Category Statistics
- Total examples: 40

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_122810_38c04a`
<!-- AUTOGENERATED:END -->
