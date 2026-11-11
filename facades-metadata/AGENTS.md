---
name: facades-metadata
description: C# examples for facades-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-metadata

> **Facades metadata** in PDF using C# / .NET -- **39** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-metadata** category.
This folder contains standalone C# examples for facades-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (37/39 files) ← category-specific
- `using Aspose.Pdf;` (11/39 files)
- `using Aspose.Pdf.LogicalStructure;` (1/39 files)
- `using Aspose.Pdf.Tagged;` (1/39 files)
- `using Aspose.Pdf.Text;` (1/39 files)
- `using System;` (39/39 files)
- `using System.IO;` (39/39 files)
- `using System.Text.Json;` (2/39 files)
- `using System.Threading.Tasks;` (2/39 files)
- `using System.Collections.Generic;` (1/39 files)
- `using System.Linq;` (1/39 files)
- `using System.Text;` (1/39 files)
- `using System.Text.Json.Serialization;` (1/39 files)
- `using System.Text.RegularExpressions;` (1/39 files)

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
| [add-custom-metadata-field-to-pdf](./add-custom-metadata-field-to-pdf.cs) | Update PDF Metadata with a Version Field | `PdfFileInfo`, `SetMetaInfo`, `Save` | Loads an existing PDF, adds or updates a custom metadata entry named "Version" while preserving a... |
| [add-custom-metadata-reviewedby-to-pdf](./add-custom-metadata-reviewedby-to-pdf.cs) | Add Custom 'ReviewedBy' Metadata to PDF | `PdfFileInfo`, `SetMetaInfo`, `Save` | Shows how to add a custom metadata field named "ReviewedBy" to a PDF using Aspose.Pdf.Facades Pdf... |
| [add-custom-metadata-to-pdf](./add-custom-metadata-to-pdf.cs) | Add Custom Metadata to PDF using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `Save` | Demonstrates how to add a custom metadata entry to a PDF file with Aspose.Pdf.Facades PdfFileInfo... |
| [add-department-metadata-to-pdfs](./add-department-metadata-to-pdfs.cs) | Add Custom Department Metadata to PDFs | `PdfFileInfo`, `SetMetaInfo`, `Save` | Demonstrates looping through PDF files, adding a custom "Department" metadata field with Aspose.P... |
| [add-lastupdated-metadata-to-pdf](./add-lastupdated-metadata-to-pdf.cs) | Add Custom 'LastUpdated' Metadata to PDF | `PdfFileInfo`, `SetMetaInfo`, `Save` | Shows how to set a custom metadata field with the current UTC timestamp using Aspose.Pdf.Facades ... |
| [apply-metadata-changes-to-multiple-pdfs-in-paralle...](./apply-metadata-changes-to-multiple-pdfs-in-parallel.cs) | Parallel Batch Update of PDF Metadata | `PdfFileInfo`, `BindPdf`, `Title` | Demonstrates how to apply metadata changes to multiple PDF files concurrently using the Task Para... |
| [audit-pdf-metadata-to-csv](./audit-pdf-metadata-to-csv.cs) | Capture, Update, and Audit PDF Metadata with CSV | `PdfFileInfo`, `BindPdf`, `Document` | The example reads a PDF's original metadata using PdfFileInfo, updates the metadata via Document.... |
| [backup-and-update-pdf-metadata](./backup-and-update-pdf-metadata.cs) | Backup PDFs and Update Metadata with PdfFileInfo | `PdfFileInfo`, `Title`, `Author` | Creates a backup copy of each PDF in a folder, then uses Aspose.Pdf.Facades.PdfFileInfo to modify... |
| [convert-pdf-to-pdfa-1b-using-pdffileinfo](./convert-pdf-to-pdfa-1b-using-pdffileinfo.cs) | Convert PDF to PDF/A‑1b Compliance | `Document`, `Convert`, `PdfFormat` | Shows how to load a PDF with Aspose.Pdf and convert it to PDF/A‑1b archival format using the Docu... |
| [export-pdf-metadata-to-json](./export-pdf-metadata-to-json.cs) | Export PDF Metadata to JSON | `PdfFileInfo`, `Title`, `Author` | Iterates over PDF files in a folder, extracts their document information with Aspose.Pdf.Facades.... |
| [handle-readonly-file-errors-when-saving-pdf-metada...](./handle-readonly-file-errors-when-saving-pdf-metadata.cs) | Update PDF Metadata with Read‑Only File Handling | `PdfFileInfo`, `SaveNewInfo` | Demonstrates how to modify PDF document information using Aspose.Pdf.Facades and gracefully handl... |
| [import-json-metadata-to-pdf](./import-json-metadata-to-pdf.cs) | Import JSON Metadata and Apply to PDFs | `PdfFileInfo`, `BindPdf`, `Save` | Reads a JSON file containing PDF metadata entries and applies the specified title, author, subjec... |
| [list-custom-pdf-metadata-keys](./list-custom-pdf-metadata-keys.cs) | Retrieve Custom PDF Metadata Keys Alphabetically | `Document`, `DocumentInfo`, `Keys` | The example loads a PDF, filters out standard metadata entries, and lists all custom metadata key... |
| [load-pdf-retrieve-file-info-and-save](./load-pdf-retrieve-file-info-and-save.cs) | Read PDF Metadata and Add a Page using Aspose.Pdf | `PdfFileInfo`, `Document`, `Add` | Shows how to retrieve PDF file information with PdfFileInfo and modify the document by adding a b... |
| [merge-xmp-metadata-with-pdf-fileinfo](./merge-xmp-metadata-with-pdf-fileinfo.cs) | Merge XMP Metadata Packet into PDF Document | `Document`, `Metadata`, `RegisterNamespaceUri` | Shows how to load an XMP packet, extract simple properties, register a custom namespace, and merg... |
| [read-custom-pdf-metadata-existence-check](./read-custom-pdf-metadata-existence-check.cs) | Read Custom PDF Metadata Safely | `Document`, `DocumentInfo`, `ContainsKey` | Shows how to load a PDF with Aspose.Pdf, verify the presence of a custom metadata key, and read i... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata with PdfFileInfo | `PdfFileInfo`, `Author`, `Dispose` | Demonstrates how to open a PDF file using Aspose.Pdf.Facades.PdfFileInfo and retrieve the Author ... |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata with PdfFileInfo | `PdfFileInfo`, `Creator`, `Dispose` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to access the Creator metadata field of a PDF doc... |
| [read-pdf-metadata-with-null-handling](./read-pdf-metadata-with-null-handling.cs) | Read PDF Metadata with Null Handling | `PdfFileInfo`, `GetMetaInfo` | Shows how to read standard PDF metadata using Aspose.Pdf.Facades.PdfFileInfo and gracefully handl... |
| [read-pdf-modification-date](./read-pdf-modification-date.cs) | Read PDF Modification Date with PdfFileInfo | `PdfFileInfo`, `ModDate` | Shows how to obtain the ModDate metadata from a PDF using Aspose.Pdf.Facades.PdfFileInfo, parse t... |
| [read-pdf-title-metadata-log](./read-pdf-title-metadata-log.cs) | Read PDF Title Metadata and Write to Log | `PdfFileInfo`, `Title`, `Dispose` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read the Title metadata from a PDF file... |
| [read-pdf-version](./read-pdf-version.cs) | Read PDF Version Number Using Aspose.Pdf | `Document`, `Version`, `PdfFileInfo` | Shows how to load a PDF file and retrieve its version string with Aspose.Pdf's Document class (op... |
| [read-update-pdf-metadata](./read-update-pdf-metadata.cs) | Update PDF Metadata with PdfFileInfo | `PdfFileInfo`, `Title`, `Author` | Demonstrates how to open a PDF, read its existing metadata, modify Title, Author, and Subject, an... |
| [remove-custom-pdf-metadata-entry](./remove-custom-pdf-metadata-entry.cs) | Clear Obsolete Custom Metadata Field from PDF | `PdfFileInfo`, `BindPdf`, `SetMetaInfo` | Demonstrates how to bind a PDF with PdfFileInfo, remove a specific custom metadata entry by setti... |
| [retrieve-custom-pdf-metadata-reviewedby](./retrieve-custom-pdf-metadata-reviewedby.cs) | Retrieve Custom PDF Metadata (ReviewedBy) with Aspose.Pdf | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to read a custom metadata entry named "Rev... |
| [retrieve-custom-pdf-metadata](./retrieve-custom-pdf-metadata.cs) | Retrieve Custom PDF Metadata (ProjectCode) using Aspose.Pdf | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to read a custom metadata entry named "ProjectCode" from a PDF file using Aspose... |
| [retrieve-pdf-keywords-metadata](./retrieve-pdf-keywords-metadata.cs) | Read PDF Keywords Metadata with PdfFileInfo | `PdfFileInfo`, `Keywords` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to retrieve the Keywords metadata field fr... |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set Custom Creator Metadata in PDF | `PdfFileInfo`, `ctor`, `Creator` | Demonstrates how to assign a custom Creator value to a PDF using Aspose.Pdf.Facades.PdfFileInfo a... |
| [set-pdf-keywords-metadata](./set-pdf-keywords-metadata.cs) | Set PDF Keywords Metadata Using PdfFileInfo | `PdfFileInfo`, `BindPdf`, `Save` | Demonstrates how to assign the Keywords metadata field of a PDF document using Aspose.Pdf.Facades... |
| [set-pdf-language-property](./set-pdf-language-property.cs) | Set PDF Language Property Using Tagged Content | `PdfFileInfo`, `Document`, `ITaggedContent` | Demonstrates how to read basic PDF metadata with PdfFileInfo and set the document language to "en... |
| ... | | | *and 9 more files* |

## Category Statistics
- Total examples: 39

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-10 | Run: `20261110_230722_0632b6`
<!-- AUTOGENERATED:END -->
