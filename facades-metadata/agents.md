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

- `using Aspose.Pdf.Facades;` (38/40 files) ← category-specific
- `using Aspose.Pdf;` (12/40 files)
- `using Aspose.Pdf.Tagged;` (1/40 files)
- `using System;` (40/40 files)
- `using System.IO;` (37/40 files)
- `using System.Collections.Generic;` (3/40 files)
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
| [add-custom-metadata-field-to-pdf](./add-custom-metadata-field-to-pdf.cs) | Add Custom Metadata Field to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata field "ReviewedBy" to a PDF using PdfFileInfo and persi... |
| [add-custom-metadata-to-pdf](./add-custom-metadata-to-pdf.cs) | Add Custom Metadata to PDF using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata entry to a PDF file with Aspose.Pdf.Facades.PdfFileInfo... |
| [add-custom-timestamp-metadata-to-pdf](./add-custom-timestamp-metadata-to-pdf.cs) | Add Custom Timestamp Metadata to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata field "LastUpdated" with the current UTC timestamp to a... |
| [add-department-metadata-to-pdfs](./add-department-metadata-to-pdfs.cs) | Add Department Metadata to PDFs | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Shows how to loop through PDF files, set a custom "Department" metadata field using PdfFileInfo, ... |
| [add-or-update-custom-metadata-field](./add-or-update-custom-metadata-field.cs) | Add or Update Custom Metadata Field in PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Shows how to preserve existing PDF metadata and add or update a custom property using the PdfFile... |
| [apply-metadata-parallel-to-pdfs](./apply-metadata-parallel-to-pdfs.cs) | Apply Metadata to Multiple PDFs in Parallel | `PdfFileInfo`, `SaveNewInfo` | Demonstrates how to set the Title, Author, and Keywords of several PDF files concurrently using t... |
| [backup-pdf-update-metadata](./backup-pdf-update-metadata.cs) | Backup PDF and Update Metadata with PdfFileInfo | `Document`, `PdfFileInfo`, `SaveNewInfo` | Demonstrates creating a backup of a PDF file and then modifying its metadata (title, author, subj... |
| [export-pdf-metadata-to-json](./export-pdf-metadata-to-json.cs) | Export PDF Metadata to JSON | `PdfFileInfo` | Shows how to read metadata from each PDF in a folder using Aspose.Pdf.Facades.PdfFileInfo and ser... |
| [handle-readonly-file-errors-saving-pdf-metadata](./handle-readonly-file-errors-saving-pdf-metadata.cs) | Handle Read‑Only File Errors When Saving PDF Metadata | `PdfFileInfo`, `SaveNewInfo` | Demonstrates modifying PDF metadata with PdfFileInfo and robustly handling IOException or Unautho... |
| [import-json-metadata-to-pdf](./import-json-metadata-to-pdf.cs) | Import JSON Metadata and Apply to PDFs using PdfFileInfo | `PdfFileInfo`, `SaveNewInfo`, `SetMetaInfo` | The example reads a JSON file containing standard and custom metadata, maps the values to a PdfFi... |
| [merge-xmp-metadata-with-pdf-file-info](./merge-xmp-metadata-with-pdf-file-info.cs) | Merge XMP Metadata with PDF File Info | `PdfFileInfo`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to combine standard PDF document information (title, author) with an XMP metadat... |
| [read-confidential-metadata-from-pdf](./read-confidential-metadata-from-pdf.cs) | Read Custom 'Confidential' Metadata from PDF | `Document`, `DocumentInfo` | Shows how to open a PDF with Aspose.Pdf, verify that a custom metadata key exists, and safely rea... |
| [read-custom-pdf-metadata-null-handling](./read-custom-pdf-metadata-null-handling.cs) | Read Custom PDF Metadata with Null Handling | `PdfFileInfo`, `GetMetaInfo` | Shows how to retrieve custom metadata from a PDF using Aspose.Pdf.Facades.PdfFileInfo and gracefu... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata with Aspose.Pdf | `Document`, `DocumentInfo` | Demonstrates loading a PDF using Aspose.Pdf, retrieving the Author field from the document's info... |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata Using PdfFileInfo | `Document`, `PdfFileInfo` | Shows how to retrieve the Creator metadata from a PDF file using Aspose.Pdf.Facades.PdfFileInfo w... |
| [read-pdf-modification-date](./read-pdf-modification-date.cs) | Read and Format PDF Modification Date | `PdfFileInfo` | Shows how to retrieve the ModDate metadata from a PDF using Aspose.Pdf.Facades.PdfFileInfo, conve... |
| [read-pdf-title-metadata-log](./read-pdf-title-metadata-log.cs) | Read PDF Title Metadata and Write to Log | `PdfFileInfo`, `Title` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read the Title metadata from a PDF file... |
| [read-pdf-version-using-pdffileinfo](./read-pdf-version-using-pdffileinfo.cs) | Read PDF Version Using PdfFileInfo | `PdfFileInfo`, `GetPdfVersion` | Demonstrates how to retrieve the PDF version number from a file using Aspose.Pdf.Facades.PdfFileI... |
| [read-update-pdf-metadata](./read-update-pdf-metadata.cs) | Read and Update PDF Metadata using PdfFileInfo | `PdfFileInfo`, `BindPdf`, `GetPdfVersion` | Shows how to open a PDF, read its metadata, modify title, author, subject and keywords, and save ... |
| [remove-custom-metadata-field](./remove-custom-metadata-field.cs) | Remove Custom Metadata Field from PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Loads a PDF, clears the value of a custom metadata entry named "ObsoleteField" using SetMetaInfo,... |
| [retrieve-custom-pdf-metadata-alphabetically](./retrieve-custom-pdf-metadata-alphabetically.cs) | Retrieve and Display Custom PDF Metadata Alphabetically | `Document`, `PdfFileInfo`, `IsPredefinedKey` | The example loads a PDF, extracts all custom metadata keys, sorts them alphabetically, and prints... |
| [retrieve-custom-pdf-metadata-projectcode](./retrieve-custom-pdf-metadata-projectcode.cs) | Retrieve Custom PDF Metadata (ProjectCode) | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to read a custom metadata entry named "ProjectCode" from a PDF file using the As... |
| [retrieve-pdf-keywords-metadata](./retrieve-pdf-keywords-metadata.cs) | Retrieve PDF Keywords Metadata | `PdfFileInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read the Keywords metadata from a PDF f... |
| [retrieve-reviewedby-metadata](./retrieve-reviewedby-metadata.cs) | Retrieve Custom Metadata 'ReviewedBy' from PDF | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read a custom metadata property named "... |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set PDF Creator Metadata Using PdfFileInfo | `PdfFileInfo`, `SaveNewInfo` | Shows how to assign a custom Creator value to a PDF via Aspose.Pdf.Facades.PdfFileInfo and persis... |
| [set-pdf-document-id-using-pdffileinfo](./set-pdf-document-id-using-pdffileinfo.cs) | Set PDF Document ID Using PdfFileInfo | `Document`, `PdfFileInfo`, `SetMetaInfo` | Demonstrates how to generate a GUID and store it as custom metadata (DocumentID) in a PDF using t... |
| [set-pdf-keywords-metadata](./set-pdf-keywords-metadata.cs) | Set PDF Keywords Metadata and Verify with PdfFileInfo | `PdfFileInfo`, `SaveNewInfo` | Demonstrates how to set the Keywords metadata field of a PDF using Aspose.Pdf.Facades.PdfFileInfo... |
| [set-pdf-language-using-pdffileinfo](./set-pdf-language-using-pdffileinfo.cs) | Set PDF Language Property Using PdfFileInfo | `PdfFileInfo`, `Document`, `ITaggedContent` | Demonstrates how to set the document language to "en-US" via the PdfFileInfo facade and save the ... |
| [set-pdfa-compliance-using-pdffileinfo](./set-pdfa-compliance-using-pdffileinfo.cs) | Set PDF/A Compliance Using PdfFileInfo | `Document`, `PdfFileInfo`, `PdfFormat` | Demonstrates converting a PDF to PDF/A‑1b and enforcing strict validation with PdfFileInfo to pro... |
| [thread-safe-pdf-metadata-update](./thread-safe-pdf-metadata-update.cs) | Thread‑Safe PDF Metadata Update with PdfFileInfo | `PdfFileInfo`, `BindPdf`, `SaveNewInfo` | Shows how to modify PDF metadata concurrently using Aspose.Pdf.Facades.PdfFileInfo, giving each p... |
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
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->
