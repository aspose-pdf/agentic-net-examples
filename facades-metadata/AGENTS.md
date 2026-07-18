---
name: facades-metadata
description: C# examples for facades-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-metadata

> **Facades metadata** in PDF using C# / .NET -- **40** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using System.IO;` (38/40 files)
- `using System.Collections.Generic;` (5/40 files)
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
| [add-custom-metadata-field-to-pdfs](./add-custom-metadata-field-to-pdfs.cs) | Add Custom Metadata Field to PDFs Using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Shows how to iterate over PDF files, set a custom metadata entry named "Department" with PdfFileI... |
| [add-custom-metadata-to-pdf](./add-custom-metadata-to-pdf.cs) | Add Custom Metadata to PDF using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata entry to a PDF file with the PdfFileInfo facade and sav... |
| [add-lastupdated-timestamp-metadata](./add-lastupdated-timestamp-metadata.cs) | Add LastUpdated Timestamp Metadata to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to add a custom metadata field containing the current UTC timestamp to a PDF usi... |
| [add-or-update-custom-pdf-metadata](./add-or-update-custom-pdf-metadata.cs) | Add or Update Custom PDF Metadata While Preserving Existing ... | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to add or update a custom metadata field (... |
| [add-reviewedby-metadata-to-pdf](./add-reviewedby-metadata-to-pdf.cs) | Add Custom Metadata Field "ReviewedBy" to PDF | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to open a PDF with PdfFileInfo, set a custom metadata entry, and save the update... |
| [apply-metadata-updates-to-multiple-pdfs-in-paralle...](./apply-metadata-updates-to-multiple-pdfs-in-parallel.cs) | Apply Metadata Updates to Multiple PDFs in Parallel | `PdfFileInfo`, `Author`, `Title` | Demonstrates how to update author, title, and subject metadata for a batch of PDF files concurren... |
| [backup-pdfs-update-metadata](./backup-pdfs-update-metadata.cs) | Backup PDFs and Update Metadata with PdfFileInfo | `PdfFileInfo`, `BindPdf`, `SaveNewInfo` | Creates a backup copy of each PDF in a source folder and then uses the Aspose.Pdf.Facades.PdfFile... |
| [clear-custom-pdf-metadata-field](./clear-custom-pdf-metadata-field.cs) | Clear Custom PDF Metadata Field | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | Demonstrates how to remove a specific custom metadata entry from a PDF by setting its value to an... |
| [dispose-pdffileinfo-and-document-using-blocks](./dispose-pdffileinfo-and-document-using-blocks.cs) | Dispose PdfFileInfo and Document Using Blocks | `PdfFileInfo`, `Document`, `Add` | Demonstrates creating a PDF, then using PdfFileInfo and Document inside using statements to autom... |
| [export-pdf-metadata-to-json](./export-pdf-metadata-to-json.cs) | Export PDF Metadata to JSON | `PdfFileInfo`, `BindPdf`, `Title` | Shows how to read metadata from each PDF file in a folder using Aspose.Pdf.Facades.PdfFileInfo an... |
| [import-json-metadata-to-pdf](./import-json-metadata-to-pdf.cs) | Import JSON Metadata and Apply to PDFs using PdfFileInfo | `PdfFileInfo`, `SetMetaInfo`, `SaveNewInfo` | The example reads a JSON file containing metadata records, deserializes them, and uses Aspose.Pdf... |
| [merge-xmp-metadata-with-pdffileinfo](./merge-xmp-metadata-with-pdffileinfo.cs) | Merge XMP Metadata with PdfFileInfo | `PdfFileInfo`, `Document`, `Metadata` | Demonstrates how to combine standard PdfFileInfo properties with an XMP metadata packet and save ... |
| [read-custom-pdf-metadata-key](./read-custom-pdf-metadata-key.cs) | Read Custom PDF Metadata Key | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use the PdfFileInfo facade to check for a custom metadata key ("Confidential"... |
| [read-custom-pdf-metadata](./read-custom-pdf-metadata.cs) | Read Custom PDF Metadata with Null Handling | `PdfFileInfo`, `GetMetaInfo` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to read custom metadata keys from a PDF and grace... |
| [read-pdf-author-metadata](./read-pdf-author-metadata.cs) | Read PDF Author Metadata | `PdfFileInfo`, `Author` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to read the Author metadata property from a PDF f... |
| [read-pdf-creator-metadata](./read-pdf-creator-metadata.cs) | Read PDF Creator Metadata with PdfFileInfo | `PdfFileInfo`, `Creator` | Shows how to use Aspose.Pdf.Facades.PdfFileInfo to open a PDF file and retrieve its Creator metad... |
| [read-pdf-metadata-using-pdffileinfo](./read-pdf-metadata-using-pdffileinfo.cs) | Read PDF Metadata Using PdfFileInfo | `PdfFileInfo`, `GetPdfVersion`, `Title` | Demonstrates how to open a PDF file with Aspose.Pdf.Facades.PdfFileInfo and retrieve common metad... |
| [read-pdf-modification-date](./read-pdf-modification-date.cs) | Read PDF Modification Date Using PdfFileInfo | `PdfFileInfo`, `ModDate` | Shows how to extract the ModDate metadata from a PDF file with Aspose.Pdf.Facades.PdfFileInfo and... |
| [read-pdf-title-metadata](./read-pdf-title-metadata.cs) | Read PDF Title Metadata and Write to Log | `PdfFileInfo`, `Title`, `Dispose` | Demonstrates how to load a PDF with Aspose.Pdf.Facades.PdfFileInfo, retrieve the Title metadata, ... |
| [read-pdf-version](./read-pdf-version.cs) | Read PDF Version Using Aspose.Pdf Facade | `PdfFileInfo`, `GetPdfVersion` | Demonstrates how to use Aspose.Pdf.Facades.PdfFileInfo to retrieve the PDF version string from a ... |
| [retrieve-custom-pdf-metadata-alphabetically](./retrieve-custom-pdf-metadata-alphabetically.cs) | Retrieve and Display All Custom PDF Metadata Alphabetically | `PdfFileInfo`, `PdfXmpMetadata`, `GetMetaInfo` | The example shows how to obtain all custom metadata keys from a PDF using Aspose.Pdf.Facades, sor... |
| [retrieve-pdf-keywords-metadata](./retrieve-pdf-keywords-metadata.cs) | Retrieve PDF Keywords Metadata with PdfFileInfo | `PdfFileInfo`, `Keywords`, `Dispose` | Demonstrates how to open a PDF file using Aspose.Pdf.Facades.PdfFileInfo and read the Keywords me... |
| [retrieve-projectcode-metadata](./retrieve-projectcode-metadata.cs) | Retrieve Custom Metadata "ProjectCode" from PDF | `PdfFileInfo`, `GetMetaInfo` | Demonstrates how to use the PdfFileInfo facade to read a custom metadata entry named "ProjectCode... |
| [retrieve-reviewedby-metadata](./retrieve-reviewedby-metadata.cs) | Retrieve Custom "ReviewedBy" Metadata from PDF | `PdfFileInfo`, `GetMetaInfo` | Shows how to read a custom metadata property called "ReviewedBy" from a PDF file using Aspose.Pdf... |
| [set-pdf-creator-metadata](./set-pdf-creator-metadata.cs) | Set PDF Creator Metadata with PdfFileInfo | `PdfFileInfo`, `Creator`, `SaveNewInfo` | Demonstrates how to assign a custom Creator value to a PDF using Aspose.Pdf.Facades.PdfFileInfo a... |
| [set-pdf-document-id-with-guid](./set-pdf-document-id-with-guid.cs) | Set PDF Document ID with GUID | `Document`, `PdfFileInfo`, `Header` | Demonstrates how to generate a GUID and store it as a custom Document ID in a PDF's metadata usin... |
| [set-pdf-keywords-metadata](./set-pdf-keywords-metadata.cs) | Set PDF Keywords Metadata with PdfFileInfo | `PdfFileInfo`, `Keywords`, `SaveNewInfo` | Demonstrates how to assign the Keywords metadata field of a PDF using Aspose.Pdf.Facades.PdfFileI... |
| [set-pdf-language-property](./set-pdf-language-property.cs) | Set PDF Language Property Using PdfFileInfo | `PdfFileInfo`, `Header`, `SaveNewInfo` | Shows how to set the document language (Lang entry) of a PDF via Aspose.Pdf.Facades.PdfFileInfo a... |
| [set-pdfa-compliance-flag-with-pdffileinfo](./set-pdfa-compliance-flag-with-pdffileinfo.cs) | Convert PDF to PDF/A and Save with PdfFileInfo | `Document`, `PdfFormatConversionOptions`, `PdfFormat` | The example loads a PDF, converts it to PDF/A if it is not already compliant, and then uses the P... |
| [thread-safe-pdf-metadata-update](./thread-safe-pdf-metadata-update.cs) | Thread‑Safe PDF Metadata Update with PdfFileInfo | `PdfFileInfo`, `BindPdf`, `SaveNewInfo` | Demonstrates how to safely modify PDF metadata concurrently by creating a separate PdfFileInfo in... |
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
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->
