---
name: facades-xmp-metadata
description: C# examples for facades-xmp-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-xmp-metadata

> **Facades XMP metadata** in PDF using C# / .NET -- **44** verified, compile-tested examples for **Aspose.PDF for .NET** 26.7.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-xmp-metadata** category.
This folder contains standalone C# examples for facades-xmp-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-xmp-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (42/44 files) ← category-specific
- `using Aspose.Pdf;` (28/44 files) ← category-specific
- `using Aspose.Pdf.Text;` (2/44 files)
- `using Aspose.Pdf.Optimization;` (1/44 files)
- `using Aspose.Pdf.XfaConverter;` (1/44 files)
- `using System;` (44/44 files)
- `using System.IO;` (33/44 files)
- `using System.Text;` (6/44 files)
- `using System.Xml;` (3/44 files)
- `using System.Collections.Generic;` (2/44 files)
- `using System.Xml.Linq;` (2/44 files)
- `using System.Diagnostics;` (1/44 files)
- `using System.Drawing;` (1/44 files)
- `using System.Linq;` (1/44 files)
- `using System.Reflection;` (1/44 files)
- `using System.Text.Json;` (1/44 files)
- `using System.Threading;` (1/44 files)
- `using System.Xml.Schema;` (1/44 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-creator-metadata-to-pdf-document](./add-creator-metadata-to-pdf-document.cs) | Add Creator Metadata to PDF Document | `Document`, `PdfFileInfo`, `BindPdf` | Shows how to add or update the Creator XMP metadata of an Aspose.Pdf.Document by using the PdfFil... |
| [add-minimal-xmp-metadata-fallback](./add-minimal-xmp-metadata-fallback.cs) | Add Minimal XMP Metadata Fallback to PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates binding a PDF to the PdfXmpMetadata facade, checking for existing XMP metadata, and ... |
| [add-timestamp-to-pdf-xmp-metadata](./add-timestamp-to-pdf-xmp-metadata.cs) | Add Timestamp to PDF XMP Metadata | `Document`, `PdfXmpMetadata`, `Add` | Shows how to embed a custom ISO‑8601 timestamp into the XMP metadata of a PDF using Aspose.Pdf. |
| [benchmark-xmp-metadata-extraction](./benchmark-xmp-metadata-extraction.cs) | Benchmark XMP Metadata Extraction from Large PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to measure the time required to read XMP metadata from a large PDF using Aspose.Pdf.Fac... |
| [clear-all-xmp-metadata](./clear-all-xmp-metadata.cs) | Clear All XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to remove every XMP metadata entry from a PDF while keeping the mandatory PDF schema he... |
| [compress-pdf-read-xmp-metadata](./compress-pdf-read-xmp-metadata.cs) | Compress PDF with High Compression and Read XMP Metadata | `Document`, `All`, `ImageCompressionOptions` | The example compresses a PDF using high‑compression settings via OptimizationOptions and then ext... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata and Merge PDFs | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to extract XMP metadata from a source PDF with PdfXmpMetadata, apply it to a target PDF... |
| [create-xmp-metadata-pdf](./create-xmp-metadata-pdf.cs) | Create XMP Metadata and Embed in PDF | `Document`, `Page`, `TextFragment` | Shows how to generate a PDF, register XMP namespaces, add custom XMP metadata entries, and save t... |
| [decrypt-encrypted-pdf-with-pdffilesecurity](./decrypt-encrypted-pdf-with-pdffilesecurity.cs) | Decrypt Encrypted PDF with PdfFileSecurity Facade | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Shows how to bind an encrypted PDF using PdfFileSecurity, handle InvalidPasswordException by load... |
| [decrypt-update-creator-reencrypt-pdf](./decrypt-update-creator-reencrypt-pdf.cs) | Decrypt PDF, Update Creator Metadata, and Re‑Encrypt | `Document`, `DocumentInfo`, `Decrypt` | Demonstrates opening a password‑protected PDF using the owner password, changing the Creator meta... |
| [detect-xmp-metadata-in-pdf](./detect-xmp-metadata-in-pdf.cs) | Detect XMP Metadata in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to check whether a PDF file contains XMP metadata using the Aspose.Pdf.Facades.PdfXmpMe... |
| [disable-baseurl-injection-for-pdf-generation](./disable-baseurl-injection-for-pdf-generation.cs) | Disable BaseUrl Injection for PDF Generation in Test Environ... | `Document`, `HtmlLoadOptions`, `Save` | Demonstrates using a configuration switch to control HtmlLoadOptions, disabling BaseUrl injection... |
| [export-xmp-metadata-to-json](./export-xmp-metadata-to-json.cs) | Export XMP Metadata from PDF to JSON | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to extract XMP metadata from a PDF using Aspose.Pdf, convert the XML into a dict... |
| [export-xmp-metadata-to-sidecar](./export-xmp-metadata-to-sidecar.cs) | Export XMP Metadata from PDF to Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to extract XMP metadata from a PDF using Aspose.Pdf.Facades and save it as a sep... |
| [extract-audit-original-xmp-metadata](./extract-audit-original-xmp-metadata.cs) | Extract and Audit Original XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to load a PDF, retrieve its XMP metadata using the PdfXmpMetadata facade, and sa... |
| [extract-xmp-metadata-from-pdf-byte-array](./extract-xmp-metadata-from-pdf-byte-array.cs) | Extract XMP Metadata from PDF Byte Array | `Document`, `Page`, `TextFragment` | Demonstrates how to create a PDF in memory, bind its byte array to the PdfXmpMetadata facade, and... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadata`, `BindPdf`, `Keys` | Shows how to read XMP metadata from a PDF using Aspose.Pdf.Facades.PdfXmpMetadata and convert it ... |
| [insert-pdf-pages-update-xmp-metadata](./insert-pdf-pages-update-xmp-metadata.cs) | Insert PDF Pages and Update XMP Metadata | `PdfFileEditor`, `TryInsert`, `PdfXmpMetadata` | Demonstrates inserting pages from one PDF into another using PdfFileEditor and then updating the ... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract the XMP metadata from a PDF with Aspose.Pdf and enumerate the namespace decl... |
| [load-pdf-retrieve-xmp-metadata](./load-pdf-retrieve-xmp-metadata.cs) | Load PDF and Retrieve XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind an existing PDF file to a PdfXmpMetadata facade and extract its XMP metadata as... |
| [log-xmp-metadata-size-before-after-modification](./log-xmp-metadata-size-before-after-modification.cs) | Log XMP Metadata Size Before and After Modification | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Loads a PDF, reads its XMP metadata using PdfXmpMetadata, logs the original size, adds a custom f... |
| [modify-xmp-creator-property](./modify-xmp-creator-property.cs) | Modify XMP Creator Property in a PDF | `Document`, `BindPdf`, `GetXmpMetadata` | Shows how to read the XMP metadata from a PDF, edit the dc:creator node, and write the updated me... |
| [nightly-creator-metadata-refresh](./nightly-creator-metadata-refresh.cs) | Nightly Creator Metadata Refresh for PDFs | `PdfFileInfo`, `Creator`, `SaveNewInfo` | Shows how to schedule a daily job that scans a folder of PDFs and updates the Creator XMP metadat... |
| [preserve-xmp-metadata-when-splitting-pdf](./preserve-xmp-metadata-when-splitting-pdf.cs) | Preserve XMP Metadata When Splitting PDF Pages | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a multi‑page PDF and attach it to each single‑page PDF gen... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF via UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates loading a PDF located on a network share using a UNC path and extracting its XMP met... |
| [read-xmp-metadata-from-pdf](./read-xmp-metadata-from-pdf.cs) | Read XMP Metadata from a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF file, extract its XMP packet as XML, and query common metadata fie... |
| [remove-nickname-from-pdf-xmp-metadata](./remove-nickname-from-pdf-xmp-metadata.cs) | Remove Nickname Element from PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Remove` | Demonstrates how to delete the Nickname element from a PDF's XMP metadata using the Aspose.Pdf.Fa... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `Clear` | Shows how to delete the entire XMP metadata block from a PDF file using Aspose.Pdf's PdfXmpMetada... |
| [replace-pdf-xmp-metadata](./replace-pdf-xmp-metadata.cs) | Replace PDF XMP Metadata from External File | `Document`, `SetXmpMetadata`, `Save` | Shows how to load a PDF, read an external .xmp file, and replace the PDF's existing XMP metadata ... |
| [retrieve-xmp-metadata-from-pdf](./retrieve-xmp-metadata-from-pdf.cs) | Retrieve XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF using PdfXmpMetadata and extract its raw XMP XML as a UTF‑8 string. |
| ... | | | *and 14 more files* |

## Category Statistics
- Total examples: 44

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-17 | Run: `20260717_171638_da3b3e`
<!-- AUTOGENERATED:END -->
