---
name: facades-xmp-metadata
description: C# examples for facades-xmp-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-xmp-metadata

> **Facades XMP metadata** in PDF using C# / .NET -- **72** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-xmp-metadata** category.
This folder contains standalone C# examples for facades-xmp-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-xmp-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (42/72 files) ← category-specific
- `using Aspose.Pdf;` (23/72 files)
- `using Aspose.Pdf.Text;` (2/72 files)
- `using Aspose.Pdf.Optimization;` (1/72 files)
- `using Aspose.Pdf.XfaConverter;` (1/72 files)
- `using System;` (44/72 files)
- `using System.IO;` (37/72 files)
- `using System.Text;` (7/72 files)
- `using System.Xml;` (5/72 files)
- `using NUnit.Framework;` (3/72 files)
- `using System.Xml.Linq;` (2/72 files)
- `using Newtonsoft.Json;` (1/72 files)
- `using System.Collections.Generic;` (1/72 files)
- `using System.Diagnostics;` (1/72 files)
- `using System.Drawing;` (1/72 files)
- `using System.Threading.Tasks;` (1/72 files)
- `using System.Xml.Schema;` (1/72 files)

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-creator-metadata-to-pdf-document](./add-creator-metadata-to-pdf-document.cs) | Add creator metadata to pdf document |  | Add creator metadata to pdf document |
| [add-minimal-xmp-metadata-fallback](./add-minimal-xmp-metadata-fallback.cs) | Add Minimal XMP Metadata Fallback to PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to detect missing XMP metadata in a PDF and inject a minimal set of metadata entries us... |
| [add-timestamp-to-pdf-xmp-metadata](./add-timestamp-to-pdf-xmp-metadata.cs) | Add timestamp to pdf xmp metadata |  | Add timestamp to pdf xmp metadata |
| [add-xmp-timestamp-to-pdf](./add-xmp-timestamp-to-pdf.cs) | Add XMP Timestamp to PDF during Generation | `Document`, `Page`, `TextFragment` | Shows how to create a PDF with Aspose.Pdf, add a page with sample text, and automatically embed a... |
| [benchmark-xmp-metadata-extraction](./benchmark-xmp-metadata-extraction.cs) | Benchmark xmp metadata extraction |  | Benchmark xmp metadata extraction |
| [bind-pdf-byte-array-to-pdfxmpmetadata](./bind-pdf-byte-array-to-pdfxmpmetadata.cs) | Bind PDF Byte Array to PdfXmpMetadata and Extract XMP | `Document`, `Add`, `Save` | Demonstrates how to create a PDF in memory, bind it from a byte array to the PdfXmpMetadata facad... |
| [clear-all-xmp-metadata](./clear-all-xmp-metadata.cs) | Clear all xmp metadata |  | Clear all xmp metadata |
| [clear-xmp-metadata-pdf](./clear-xmp-metadata-pdf.cs) | Clear All XMP Metadata from PDF while Preserving Header | `PdfXmpMetadata`, `BindPdf`, `Clear` | Demonstrates how to remove all XMP metadata from a PDF using Aspose.Pdf.Facades, keeping only the... |
| [compress-pdf-read-xmp-metadata](./compress-pdf-read-xmp-metadata.cs) | Compress PDF with High Settings and Read XMP Metadata | `Document`, `OptimizationOptions`, `OptimizeResources` | Demonstrates compressing a PDF using high‑compression optimization options and then extracting it... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata and Merge PDFs | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to extract XMP metadata from a source PDF with the PdfXmpMetadata facade, apply it to a... |
| [create-pdf-with-xmp-metadata](./create-pdf-with-xmp-metadata.cs) | Create PDF with XMP Metadata | `Document`, `Add`, `RegisterNamespaceUri` | Shows how to generate a new PDF, register XMP namespaces, add custom XMP metadata entries, and sa... |
| [create-xmp-metadata-pdf](./create-xmp-metadata-pdf.cs) | Create xmp metadata pdf |  | Create xmp metadata pdf |
| [decrypt-encrypted-pdf-with-pdffilesecurity](./decrypt-encrypted-pdf-with-pdffilesecurity.cs) | Decrypt encrypted pdf with pdffilesecurity |  | Decrypt encrypted pdf with pdffilesecurity |
| [decrypt-update-creator-reencrypt-pdf](./decrypt-update-creator-reencrypt-pdf.cs) | Decrypt update creator reencrypt pdf |  | Decrypt update creator reencrypt pdf |
| [decrypt-update-creatortool-reencrypt-pdf](./decrypt-update-creatortool-reencrypt-pdf.cs) | Decrypt PDF, Update CreatorTool, and Re‑encrypt | `Document`, `PdfFileSecurity`, `PdfFileInfo` | Demonstrates how to open a password‑protected PDF, decrypt it, modify the Creator metadata (Creat... |
| [detect-and-modify-xmp-metadata](./detect-and-modify-xmp-metadata.cs) | Detect and Modify XMP Metadata in PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF with Aspose.Pdf.Facades.PdfXmpMetadata, check for existing XMP met... |
| [detect-xmp-metadata-in-pdf](./detect-xmp-metadata-in-pdf.cs) | Detect xmp metadata in pdf |  | Detect xmp metadata in pdf |
| [disable-baseurl-injection-for-pdf-generation](./disable-baseurl-injection-for-pdf-generation.cs) | Disable baseurl injection for pdf generation |  | Disable baseurl injection for pdf generation |
| [disable-baseurl-injection-html-to-pdf](./disable-baseurl-injection-html-to-pdf.cs) | Disable BaseUrl Injection When Converting HTML to PDF | `Document`, `HtmlLoadOptions`, `PdfViewer` | Shows how to use a configuration switch to optionally omit the BaseUrl in HtmlLoadOptions while c... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a PDF using Aspose.Pdf.Facades and convert it into JSON fo... |
| [export-xmp-metadata-from-pdf](./export-xmp-metadata-from-pdf.cs) | Export XMP Metadata from PDF to a Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF document, retrieve its XMP metadata using Aspose.Pdf.Facades, and ... |
| [export-xmp-metadata-to-json](./export-xmp-metadata-to-json.cs) | Export xmp metadata to json |  | Export xmp metadata to json |
| [export-xmp-metadata-to-sidecar](./export-xmp-metadata-to-sidecar.cs) | Export xmp metadata to sidecar |  | Export xmp metadata to sidecar |
| [extract-audit-original-xmp-metadata](./extract-audit-original-xmp-metadata.cs) | Extract audit original xmp metadata |  | Extract audit original xmp metadata |
| [extract-xmp-metadata-from-pdf-byte-array](./extract-xmp-metadata-from-pdf-byte-array.cs) | Extract xmp metadata from pdf byte array |  | Extract xmp metadata from pdf byte array |
| [extract-xmp-metadata-from-pdf](./extract-xmp-metadata-from-pdf.cs) | Extract XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF with the PdfXmpMetadata facade, retrieve the raw XMP XML bytes, co... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to read the XMP packet from a PDF using Aspose.Pdf.Facades and convert the XML into a c... |
| [insert-pages-update-xmp-metadata](./insert-pages-update-xmp-metadata.cs) | Insert Pages and Update XMP Metadata in PDF | `PdfFileEditor`, `TryInsert`, `PdfXmpMetadata` | Demonstrates how to insert selected pages from one PDF into another and then add or overwrite XMP... |
| [insert-pdf-pages-update-xmp-metadata](./insert-pdf-pages-update-xmp-metadata.cs) | Insert pdf pages update xmp metadata |  | Insert pdf pages update xmp metadata |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract the XMP metadata packet from a PDF using Aspose.Pdf and enumerate the namesp... |
| ... | | | *and 42 more files* |

## Category Statistics
- Total examples: 72

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-09-10 | Run: `20260910_054408_87a7f4`
<!-- AUTOGENERATED:END -->
