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
- `using Aspose.Pdf;` (6/40 files)
- `using Aspose.Pdf.Tagged;` (1/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (37/40 files)
- `using System.Collections.Generic;` (4/40 files)
- `using System.Text.Json;` (2/40 files)
- `using System.Threading.Tasks;` (2/40 files)
- `using System.Globalization;` (1/40 files)
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
| [add-custom-metadata-to-pdf](./add-custom-metadata-to-pdf.cs) | Add Custom Metadata to PDF using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata entry (ProjectCode) to an existing PDF file and save th... |
| [add-custom-metadata-to-pdf__v2](./add-custom-metadata-to-pdf__v2.cs) | Add Custom Metadata to PDF Using PdfFileInfo Facade | `Document`, `PdfFileInfo`, `SetMetaInfo` | Demonstrates how to add a custom metadata field to a PDF document with the PdfFileInfo facade and... |
| [add-custom-metadata](./add-custom-metadata.cs) | Add Custom Metadata to Multiple PDFs | `Document`, `PdfFileInfo`, `SetMetaInfo` | Creates sample PDFs, loops through them, adds a custom 'Department' metadata field using PdfFileI... |
| [add-custom-timestamp-metadata-to-pdf](./add-custom-timestamp-metadata-to-pdf.cs) | Add Custom Timestamp Metadata to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Shows how to set a custom metadata field containing the current UTC timestamp using PdfFileInfo a... |
| [apply-metadata-parallel-pdf](./apply-metadata-parallel-pdf.cs) | Apply Metadata Changes to Multiple PDFs in Parallel | `PdfFileInfo`, `SaveNewInfo` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to set title, author, subject, and keywords on se... |
| [audit-pdf-metadata-to-csv](./audit-pdf-metadata-to-csv.cs) | Audit PDF Metadata Changes and Log to CSV | `PdfFileInfo`, `SaveNewInfo`, `Title` | The example reads a PDF's original metadata with PdfFileInfo, updates several fields, saves a new... |
| [backup-pdfs-update-metadata](./backup-pdfs-update-metadata.cs) | Backup PDFs and Update Metadata with PdfFileInfo | `PdfFileInfo`, `Title`, `Author` | Creates a backup copy of each PDF in a specified folder and then modifies its metadata (title, au... |
| [check-custom-pdf-metadata-key](./check-custom-pdf-metadata-key.cs) | Read Custom PDF Metadata Safely | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to verify the existence of a custom metadata entry ("Confidential") in a PDF fil... |
| [conditionally-update-pdf-author-metadata](./conditionally-update-pdf-author-metadata.cs) | Conditionally Update PDF Author Metadata | `PdfFileInfo`, `Author`, `SaveNewInfo` | Shows how to read a PDF's Author field with Aspose.Pdf.Facades.PdfFileInfo, set it only when the ... |
| [dispose-pdffileinfo-and-document-add-text-stamp](./dispose-pdffileinfo-and-document-add-text-stamp.cs) | Dispose PdfFileInfo and Document with Using Blocks and Add T... | `PdfFileInfo`, `Document`, `TextStamp` | Demonstrates using `using` blocks to properly dispose `PdfFileInfo` and `Document` objects, retri... |
| [export-pdf-metadata-to-json](./export-pdf-metadata-to-json.cs) | Export PDF Metadata to JSON | `PdfFileInfo`, `BindPdf`, `Title` | Shows how to read metadata from each PDF in a folder using Aspose.Pdf.Facades.PdfFileInfo and ser... |
| [handle-readonly-when-saving-pdf-metadata](./handle-readonly-when-saving-pdf-metadata.cs) | Handle Read‑Only File Attribute When Saving PDF Metadata | `PdfFileInfo`, `Title`, `SaveNewInfo` | Shows how to modify PDF metadata with PdfFileInfo and gracefully recover from an IOException caus... |
| [import-json-metadata-into-pdfs](./import-json-metadata-into-pdfs.cs) | Import JSON Metadata into PDFs using PdfFileInfo | `PdfFileInfo`, `Author`, `Title` | Shows how to read a JSON file of metadata, apply both standard and custom properties to PDF files... |
| [list-custom-pdf-metadata-alphabetically](./list-custom-pdf-metadata-alphabetically.cs) | List Custom PDF Metadata Alphabetically | `PdfFileInfo`, `GetMetaInfo`, `Header` | Demonstrates how to retrieve all custom metadata keys from a PDF using the PdfFileInfo facade, so... |
| [merge-xmp-metadata-with-pdf-fileinfo](./merge-xmp-metadata-with-pdf-fileinfo.cs) | Merge XMP Metadata with PDF FileInfo | `Document`, `PdfFileInfo`, `SaveNewInfoWithXmp` | Shows how to update standard PDF file information (title, author, etc.) and merge those changes w... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata with PdfFileInfo | `PdfFileInfo`, `Author` | Demonstrates how to open a PDF file with Aspose.Pdf.Facades.PdfFileInfo, retrieve the Author meta... |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata Using PdfFileInfo | `PdfFileInfo`, `Creator` | Demonstrates how to open a PDF with Aspose.Pdf.Facades.PdfFileInfo and retrieve the Creator metad... |
| [read-pdf-metadata-null-handling](./read-pdf-metadata-null-handling.cs) | Read PDF Metadata with Null Handling using Aspose.Pdf | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to read standard and custom PDF metadata using Aspose.Pdf's PdfFileInfo facade a... |
| [read-pdf-modification-date](./read-pdf-modification-date.cs) | Read and Format PDF Modification Date using PdfFileInfo | `PdfFileInfo`, `ModDate` | Demonstrates how to retrieve the ModDate metadata from a PDF with Aspose.Pdf.Facades.PdfFileInfo,... |
| [read-pdf-title-metadata-and-log](./read-pdf-title-metadata-and-log.cs) | Read PDF Title Metadata and Log It | `PdfFileInfo`, `Title` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read the Title metadata from a PDF file... |
| [read-pdf-version-using-aspose-pdf](./read-pdf-version-using-aspose-pdf.cs) | Read PDF Version Using Aspose.Pdf | `PdfFileInfo`, `GetPdfVersion` | Shows how to retrieve the PDF version string from a file by using Aspose.Pdf.Facades.PdfFileInfo. |
| [read-update-pdf-metadata](./read-update-pdf-metadata.cs) | Read and Update PDF Metadata with PdfFileInfo | `PdfFileInfo`, `BindPdf`, `Title` | Demonstrates how to bind a PDF file, read its metadata (title, author, page count, version), modi... |
| [remove-obsolete-metadata-field](./remove-obsolete-metadata-field.cs) | Remove Obsolete Metadata Field from PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to load a PDF with PdfFileInfo, clear a custom metadata entry by setting it to a... |
| [retrieve-custom-pdf-metadata-projectcode](./retrieve-custom-pdf-metadata-projectcode.cs) | Retrieve Custom PDF Metadata (ProjectCode) | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read a custom metadata entry named "Pro... |
| [retrieve-pdf-keywords-metadata](./retrieve-pdf-keywords-metadata.cs) | Retrieve PDF Keywords Metadata using PdfFileInfo | `PdfFileInfo`, `Keywords`, `Dispose` | Demonstrates how to open a PDF with Aspose.Pdf.Facades.PdfFileInfo, read the Keywords metadata pr... |
| [retrieve-reviewedby-metadata](./retrieve-reviewedby-metadata.cs) | Retrieve Custom PDF Metadata 'ReviewedBy' Using PdfFileInfo | `PdfFileInfo`, `GetMetaInfo` | The example checks for a PDF file, opens it with PdfFileInfo, and reads the custom metadata prope... |
| [set-custom-document-id-pdf](./set-custom-document-id-pdf.cs) | Set Custom Document ID in PDF using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to generate a GUID and store it as a custom metadata field in a PDF file using t... |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set PDF Creator Metadata Using PdfFileInfo | `PdfFileInfo`, `Creator`, `SaveNewInfo` | Demonstrates how to assign a custom Creator value to a PDF's metadata using Aspose.Pdf.Facades.Pd... |
| [set-pdf-keywords-metadata](./set-pdf-keywords-metadata.cs) | Set PDF Keywords Metadata Using PdfFileInfo | `PdfFileInfo`, `Keywords`, `SaveNewInfo` | Demonstrates how to assign keyword metadata to a PDF file with Aspose.Pdf.Facades.PdfFileInfo, sa... |
| [set-pdf-language-property](./set-pdf-language-property.cs) | Set PDF Language Property via PdfFileInfo | `PdfFileInfo`, `Document`, `ITaggedContent` | Shows how to load a PDF with PdfFileInfo, set the document language to "en-US" using the TaggedCo... |
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
Updated: 2026-06-18 | Run: `20260618_025753_02f7ba`
<!-- AUTOGENERATED:END -->
