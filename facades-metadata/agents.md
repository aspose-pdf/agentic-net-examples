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

- `using Aspose.Pdf.Facades;` (36/40 files) ← category-specific
- `using Aspose.Pdf;` (18/40 files)
- `using Aspose.Pdf.Tagged;` (1/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (38/40 files)
- `using System.Threading.Tasks;` (2/40 files)
- `using System.Collections.Generic;` (1/40 files)
- `using System.Globalization;` (1/40 files)
- `using System.Text.Json;` (1/40 files)
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
| [add-custom-metadata-field](./add-custom-metadata-field.cs) | Add Custom Metadata Field to PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to add a custom metadata property "LastUpdated" with the current UTC timestamp t... |
| [add-custom-metadata-version](./add-custom-metadata-version.cs) | Add Custom Metadata Field While Preserving Existing Metadata | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to keep existing PDF custom metadata and add a new "Version" field using PdfFile... |
| [add-custom-metadata](./add-custom-metadata.cs) | Add Custom Metadata to PDF using SetMetaInfo | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to add a custom metadata entry named "ProjectCode" to a PDF file using PdfFileIn... |
| [add-custom-metadata__v2](./add-custom-metadata__v2.cs) | Add Custom Metadata Field to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates adding a custom metadata property "ReviewedBy" to a PDF using PdfFileInfo and persis... |
| [add-custom-metadata__v3](./add-custom-metadata__v3.cs) | Add Custom Metadata Field to Multiple PDFs | `Document`, `SetMetaInfo`, `SaveNewInfo` | Loops through PDF files, adds a custom "Department" metadata field using PdfFileInfo.SetMetaInfo,... |
| [backup-modify-metadata](./backup-modify-metadata.cs) | Backup PDF and Modify Metadata with PdfFileInfo | `PdfFileInfo`, `SaveNewInfo` | Creates a backup of a PDF file, then updates its metadata (title, author, subject) using Aspose.P... |
| [check-custom-metadata](./check-custom-metadata.cs) | Check Custom Metadata Key in PDF | `PdfXmpMetadata`, `Contains`, `this[string]` | Demonstrates how to verify the presence of a custom metadata entry before reading its value. |
| [dispose-pdffileinfo-pdfdocument](./dispose-pdffileinfo-pdfdocument.cs) | Dispose PdfFileInfo and PdfDocument with Using Blocks | `Document`, `PdfFileInfo`, `SaveNewInfo` | Demonstrates loading a PDF, reading and updating its metadata using PdfFileInfo, and properly dis... |
| [export-pdf-metadata](./export-pdf-metadata.cs) | Export PDF Metadata to JSON Files | `Document`, `ExportToJson` | Iterates through all PDF files in a folder, extracts form field metadata as JSON, and saves each ... |
| [handle-readonly-file-attribute](./handle-readonly-file-attribute.cs) | Handle read‑only file attribute when updating PDF info | `PdfFileInfo`, `SaveNewInfo` | Demonstrates updating PDF metadata with PdfFileInfo and handling IOException caused by a read‑onl... |
| [import-pdf-metadata-json](./import-pdf-metadata-json.cs) | Import PDF Metadata from JSON using PdfFileInfo | `PdfFileInfo`, `SaveNewInfo`, `Title` | Reads metadata values from a JSON file and writes them into a PDF file using Aspose.Pdf.Facades.P... |
| [log-pdf-metadata-to-csv](./log-pdf-metadata-to-csv.cs) | Log PDF Metadata Changes to CSV | `Document`, `DocumentInfo`, `Save` | Loads a PDF, records original metadata, updates metadata fields, saves the PDF, and writes a CSV ... |
| [merge-xmp-pdffileinfo](./merge-xmp-pdffileinfo.cs) | Merge XMP Metadata with PdfFileInfo Entries | `PdfFileInfo`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to combine standard PDF file information with custom XMP metadata and save the r... |
| [parallel-metadata-update](./parallel-metadata-update.cs) | Parallel Metadata Update for Multiple PDFs | `Document`, `DocumentInfo`, `ForEach` | Demonstrates how to modify PDF metadata (title, author, subject, keywords) concurrently across ma... |
| [pdf-fileinfo-metadata](./pdf-fileinfo-metadata.cs) | Read and Update PDF Metadata using PdfFileInfo | `PdfFileInfo`, `SaveNewInfo`, `Title` | Demonstrates opening a PDF, reading its metadata with PdfFileInfo, modifying fields, and saving t... |
| [read-custom-metadata](./read-custom-metadata.cs) | Read Custom PDF Metadata Safely | `PdfFileInfo`, `BindPdf`, `GetMetaInfo` | Demonstrates how to read a custom metadata property from a PDF using PdfFileInfo and handle null ... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata using PdfFileInfo | `PdfFileInfo`, `Author` | Demonstrates how to read the Author metadata from a PDF file using Aspose.Pdf.Facades.PdfFileInfo. |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata using PdfFileInfo | `PdfFileInfo`, `Creator` | Demonstrates how to load a PDF with PdfFileInfo and retrieve the Creator metadata property. |
| [read-pdf-moddate](./read-pdf-moddate.cs) | Read PDF Modification Date using PdfFileInfo | `PdfFileInfo`, `ModDate`, `IsPdfFile` | Demonstrates how to retrieve the ModDate metadata from a PDF file using Aspose.Pdf.Facades.PdfFil... |
| [read-pdf-title-metadata](./read-pdf-title-metadata.cs) | Read PDF Title Metadata and Log It | `PdfFileInfo`, `Title` | Demonstrates how to read the Title property from a PDF using PdfFileInfo and write it to a log file. |
| [read-pdf-version](./read-pdf-version.cs) | Read PDF Version Number using PdfFileInfo | `PdfFileInfo`, `GetPdfVersion`, `Document` | Demonstrates how to retrieve the PDF version from a file using Aspose.Pdf's PdfFileInfo (and Docu... |
| [remove-custom-metadata](./remove-custom-metadata.cs) | Remove Custom Metadata Entry from PDF | `PdfFileInfo`, `SetMetaInfo`, `Save` | Demonstrates how to clear a specific custom metadata entry in a PDF by setting its value to empty... |
| [retrieve-custom-metadata](./retrieve-custom-metadata.cs) | Retrieve and Display Custom PDF Metadata Alphabetically | `Document`, `Keys`, `IsPredefinedKey` | Loads a PDF, extracts all custom metadata keys, sorts them alphabetically, and prints each key wi... |
| [retrieve-pdf-keywords](./retrieve-pdf-keywords.cs) | Retrieve PDF Keywords Metadata using PdfFileInfo | `PdfFileInfo`, `Keywords` | Demonstrates how to read the Keywords metadata from a PDF file using Aspose.Pdf.Facades.PdfFileInfo. |
| [retrieve-projectcode-metadata](./retrieve-projectcode-metadata.cs) | Retrieve Custom PDF Metadata "ProjectCode" | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to read a custom metadata property named ProjectCode from a PDF using Aspose.Pdf. |
| [retrieve-reviewedby-metadata](./retrieve-reviewedby-metadata.cs) | Retrieve Custom PDF Metadata "ReviewedBy" | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to read a custom metadata property named "ReviewedBy" from a PDF using Aspose.Pdf. |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set PDF Creator Metadata using PdfFileInfo | `PdfFileInfo`, `SaveNewInfo` | Demonstrates how to assign a custom Creator value to a PDF file using Aspose.Pdf.Facades.PdfFileI... |
| [set-pdf-document-id](./set-pdf-document-id.cs) | Set PDF Document ID using Guid and PdfFileInfo | `Document`, `SetMetaInfo`, `SaveNewInfo` | Creates a PDF, generates a GUID, and stores it as a custom DocumentID metadata using PdfFileInfo. |
| [set-pdf-keywords](./set-pdf-keywords.cs) | Set PDF Keywords Metadata Using PdfFileInfo | `PdfFileInfo`, `Keywords`, `SaveNewInfo` | Demonstrates how to set the Keywords metadata field of a PDF using PdfFileInfo and verify the cha... |
| [set-pdf-language-pdffileinfo](./set-pdf-language-pdffileinfo.cs) | Set PDF Language Property via PdfFileInfo | `PdfFileInfo`, `SaveNewInfo`, `Document` | Demonstrates how to set the PDF document language to en-US using PdfFileInfo and save the changes. |
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
Updated: 2026-04-07 | Run: `20260407_213136_a66d65`
<!-- AUTOGENERATED:END -->
