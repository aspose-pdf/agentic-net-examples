---
name: facades-metadata
description: C# examples for facades-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-metadata

> **Facades metadata** in PDF using C# / .NET -- **40** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-metadata** category.
This folder contains standalone C# examples for facades-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (39/40 files) ← category-specific
- `using Aspose.Pdf;` (5/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (35/40 files)
- `using System.Collections.Generic;` (4/40 files)
- `using System.Text.Json;` (2/40 files)
- `using System.Threading.Tasks;` (2/40 files)
- `using System.Collections.Concurrent;` (1/40 files)
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
| [add-custom-metadata-field-to-pdf](./add-custom-metadata-field-to-pdf.cs) | Add Custom Metadata Field to PDF Using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to preserve existing custom metadata in a PDF and add a new "Version" field usin... |
| [add-custom-metadata-reviewedby-to-pdf](./add-custom-metadata-reviewedby-to-pdf.cs) | Add Custom Metadata 'ReviewedBy' to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to set a custom metadata field in a PDF using Aspose.Pdf.Facades.PdfFileInfo and... |
| [add-custom-metadata-to-pdf](./add-custom-metadata-to-pdf.cs) | Add Custom Metadata to PDF using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata entry (ProjectCode) to an existing PDF and save the upd... |
| [add-department-metadata-to-pdfs](./add-department-metadata-to-pdfs.cs) | Add Custom Department Metadata to PDFs | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to loop through PDF files, set a custom "Department" metadata field using PdfFil... |
| [add-lastupdated-metadata-to-pdf](./add-lastupdated-metadata-to-pdf.cs) | Add LastUpdated Metadata to PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to add a custom metadata field named "LastUpdated" with the current UTC timestam... |
| [apply-metadata-changes-to-multiple-pdfs-in-paralle...](./apply-metadata-changes-to-multiple-pdfs-in-parallel.cs) | Apply Metadata Changes to Multiple PDFs in Parallel | `PdfFileInfo`, `Title`, `Author` | Demonstrates how to update the Title, Author, and Subject metadata of several PDF files concurren... |
| [audit-pdf-metadata-to-csv](./audit-pdf-metadata-to-csv.cs) | Audit PDF Metadata Changes and Log to CSV | `PdfFileInfo`, `Title`, `Author` | Demonstrates how to read existing PDF metadata with Aspose.Pdf.Facades, modify selected fields, s... |
| [backup-and-update-pdf-metadata](./backup-and-update-pdf-metadata.cs) | Backup PDFs and Update Metadata with PdfFileInfo | `PdfFileInfo`, `Title`, `Author` | The example creates a backup of each PDF in a folder, then uses Aspose.Pdf.Facades.PdfFileInfo to... |
| [convert-pdf-to-pdfa-1b-using-pdffileinfo](./convert-pdf-to-pdfa-1b-using-pdffileinfo.cs) | Convert PDF to PDF/A-1B Using PdfFileInfo | `PdfFileInfo`, `Document`, `Convert` | Demonstrates how to load a PDF with PdfFileInfo, convert it to PDF/A‑1B compliance, and save the ... |
| [export-pdf-metadata-to-json](./export-pdf-metadata-to-json.cs) | Export PDF Metadata to JSON | `PdfFileInfo`, `BindPdf`, `Title` | The program scans a folder for PDF files, extracts their metadata using Aspose.Pdf.Facades.PdfFil... |
| [handle-readonly-file-errors-when-saving-pdf-metada...](./handle-readonly-file-errors-when-saving-pdf-metadata.cs) | Handle Read‑Only File Errors When Saving PDF Metadata | `PdfFileInfo`, `Title`, `SaveNewInfo` | Demonstrates how to modify PDF metadata with Aspose.Pdf.Facades and reliably save it even when th... |
| [import-json-metadata-to-pdf](./import-json-metadata-to-pdf.cs) | Import JSON Metadata and Apply to PDF Files | `PdfFileInfo`, `Title`, `Author` | The example reads a JSON file containing metadata definitions and applies the standard and custom... |
| [list-custom-pdf-metadata-keys](./list-custom-pdf-metadata-keys.cs) | List Custom PDF Metadata Keys Alphabetically | `PdfFileInfo`, `GetMetaInfo`, `Document` | Shows how to extract all custom metadata keys from a PDF, sort them alphabetically, and output ea... |
| [load-pdf-retrieve-file-info-and-save](./load-pdf-retrieve-file-info-and-save.cs) | Load PDF, Retrieve File Info, and Save with Proper Disposal | `Document`, `PdfFileInfo`, `Save` | Demonstrates loading a PDF using Aspose.Pdf.Document inside a using block, accessing file‑level i... |
| [merge-xmp-metadata-with-pdf-fileinfo](./merge-xmp-metadata-with-pdf-fileinfo.cs) | Merge XMP Metadata with PDF FileInfo | `Document`, `Info`, `Metadata` | Demonstrates how to combine standard PDF FileInfo metadata with custom XMP metadata in a PDF usin... |
| [read-custom-pdf-metadata-existence-check](./read-custom-pdf-metadata-existence-check.cs) | Read Custom PDF Metadata with Existence Check | `PdfFileInfo`, `GetMetaInfo` | Shows how to verify a PDF file exists and safely read a custom metadata key "Confidential" using ... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata Using PdfFileInfo | `PdfFileInfo`, `Author` | Demonstrates how to open a PDF with Aspose.Pdf.Facades.PdfFileInfo and retrieve the Author metada... |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata | `PdfFileInfo`, `Creator` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to retrieve the Creator metadata property from an... |
| [read-pdf-metadata-with-null-handling](./read-pdf-metadata-with-null-handling.cs) | Read PDF Metadata with Null Handling | `PdfFileInfo`, `GetMetaInfo`, `Title` | Shows how to read custom and standard PDF metadata using PdfFileInfo and gracefully handle null o... |
| [read-pdf-modification-date](./read-pdf-modification-date.cs) | Read PDF Modification Date with PdfFileInfo | `PdfFileInfo`, `ModDate`, `Dispose` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to obtain the ModDate metadata from a PDF and out... |
| [read-pdf-title-metadata-log](./read-pdf-title-metadata-log.cs) | Read PDF Title Metadata and Write to Log | `PdfFileInfo`, `Title` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read the Title metadata from a PDF file... |
| [read-pdf-version](./read-pdf-version.cs) | Read PDF Version Using Aspose.Pdf Facade | `PdfFileInfo`, `BindPdf`, `GetPdfVersion` | Shows how to bind a PDF file with PdfFileInfo and retrieve its version string for later use. |
| [read-update-pdf-metadata](./read-update-pdf-metadata.cs) | Read and Update PDF Metadata | `PdfFileInfo`, `BindPdf`, `Title` | Demonstrates how to open a PDF, read its existing metadata, modify fields such as title and autho... |
| [remove-custom-pdf-metadata-entry](./remove-custom-pdf-metadata-entry.cs) | Remove Custom PDF Metadata Entry | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to delete a specific custom metadata field from a PDF by setting its value to an... |
| [retrieve-custom-pdf-metadata-reviewedby](./retrieve-custom-pdf-metadata-reviewedby.cs) | Retrieve Custom PDF Metadata (ReviewedBy) | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read a custom metadata property from a ... |
| [retrieve-custom-pdf-metadata](./retrieve-custom-pdf-metadata.cs) | Retrieve Custom PDF Metadata (ProjectCode) | `PdfFileInfo`, `GetMetaInfo` | Shows how to read a custom metadata entry named "ProjectCode" from a PDF file using Aspose.Pdf.Fa... |
| [retrieve-pdf-keywords-metadata](./retrieve-pdf-keywords-metadata.cs) | Retrieve PDF Keywords Metadata using PdfFileInfo | `PdfFileInfo`, `Keywords` | Demonstrates how to open a PDF with Aspose.Pdf.Facades.PdfFileInfo and read the Keywords metadata... |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set PDF Creator Metadata Using PdfFileInfo | `PdfFileInfo`, `Creator`, `SaveNewInfo` | Demonstrates how to assign a custom Creator value to a PDF file by using Aspose.Pdf.Facades.PdfFi... |
| [set-pdf-document-id-using-guid](./set-pdf-document-id-using-guid.cs) | Set PDF Document ID Using GUID Metadata | `Document`, `PdfFileInfo`, `BindPdf` | Generates a new GUID and stores it as a custom metadata entry (DocumentID) in a PDF using the Pdf... |
| [set-pdf-keywords-metadata](./set-pdf-keywords-metadata.cs) | Set PDF Keywords Metadata Using PdfFileInfo | `PdfFileInfo`, `Keywords`, `SaveNewInfo` | Demonstrates how to assign a Keywords metadata value to a PDF file with Aspose.Pdf.Facades.PdfFil... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->
