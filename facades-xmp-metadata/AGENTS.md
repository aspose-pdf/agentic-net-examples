---
name: facades-xmp-metadata
description: C# examples for facades-xmp-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-xmp-metadata

> **Facades XMP metadata** in PDF using C# / .NET -- **45** verified, compile-tested examples for **Aspose.PDF for .NET** 26.6.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-xmp-metadata** category.
This folder contains standalone C# examples for facades-xmp-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-xmp-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (41/45 files) ← category-specific
- `using Aspose.Pdf;` (31/45 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (1/45 files)
- `using Aspose.Pdf.Optimization;` (1/45 files)
- `using Aspose.Pdf.XfaConverter;` (1/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (38/45 files)
- `using System.Text;` (6/45 files)
- `using System.Xml;` (3/45 files)
- `using NUnit.Framework;` (2/45 files)
- `using System.Collections.Generic;` (2/45 files)
- `using System.Diagnostics;` (1/45 files)
- `using System.Drawing;` (1/45 files)
- `using System.Reflection;` (1/45 files)
- `using System.Text.Json;` (1/45 files)
- `using System.Xml.Schema;` (1/45 files)

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
| [add-creator-metadata-to-pdf-document](./add-creator-metadata-to-pdf-document.cs) | Add Creator Metadata to PDF Document | `Document`, `PdfFileInfo`, `BindPdf` | Shows how to set the Creator metadata field of an Aspose.Pdf Document using the PdfFileInfo facad... |
| [add-minimal-xmp-metadata-to-pdf](./add-minimal-xmp-metadata-to-pdf.cs) | Add Minimal XMP Metadata to PDF When Missing | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Shows how to detect the absence of XMP metadata in a PDF and create a minimal XMP packet using As... |
| [add-timestamp-to-pdf-xmp-metadata](./add-timestamp-to-pdf-xmp-metadata.cs) | Add Timestamp to PDF XMP Metadata | `Document`, `PdfXmpMetadata`, `Add` | Shows how to create a PDF, bind XMP metadata, add a custom ISO‑8601 timestamp property, and save ... |
| [benchmark-xmp-metadata-extraction](./benchmark-xmp-metadata-extraction.cs) | Benchmark XMP Metadata Extraction from Large PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a large PDF file and retrieve its XMP metadata while measuring the extraction t... |
| [bind-pdf-byte-array-to-pdfxmpmetadata](./bind-pdf-byte-array-to-pdfxmpmetadata.cs) | Bind PDF Byte Array to PdfXmpMetadata and Extract XMP | `Document`, `PdfXmpMetadata`, `Save` | Demonstrates how to create a PDF in memory, bind its byte array to the PdfXmpMetadata facade, and... |
| [clear-xmp-metadata-from-pdf](./clear-xmp-metadata-from-pdf.cs) | Clear XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `Clear` | Shows how to remove all XMP metadata fields from a PDF using Aspose.Pdf.Facades, keeping only the... |
| [compress-pdf-read-xmp-metadata](./compress-pdf-read-xmp-metadata.cs) | Compress PDF and Read XMP Metadata | `Document`, `OptimizationOptions`, `OptimizeResources` | Demonstrates how to apply high‑level compression to a PDF using Aspose.Pdf optimization options, ... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata Between PDFs and Merge | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a source PDF, apply it to a target PDF, and then concatena... |
| [create-xmp-metadata-pdf](./create-xmp-metadata-pdf.cs) | Create XMP Metadata and Attach to New PDF | `Document`, `Add`, `PdfXmpMetadata` | Shows how to generate a new PDF, build an XMP metadata block from scratch, add Dublin Core proper... |
| [detect-modify-xmp-metadata-pdf](./detect-modify-xmp-metadata-pdf.cs) | Detect and Modify XMP Metadata in PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates binding a PDF via stream, checking for existing XMP metadata, optionally updating a ... |
| [disable-baseurl-injection-html-to-pdf](./disable-baseurl-injection-html-to-pdf.cs) | Disable BaseUrl Injection for HTML to PDF Conversion in Test... | `Document`, `HtmlLoadOptions`, `PPdfConverter` | Demonstrates how to add a configuration switch that disables BaseUrl injection when converting HT... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to extract XMP metadata from a PDF using Aspose.Pdf and serialize it as a JSON file for... |
| [export-xmp-metadata-to-sidecar](./export-xmp-metadata-to-sidecar.cs) | Export XMP Metadata from PDF to Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF document, retrieve its full XMP metadata using Aspose.Pdf.Facades,... |
| [extract-pdf-xmp-metadata-to-dictionary](./extract-pdf-xmp-metadata-to-dictionary.cs) | Extract PDF XMP Metadata to Dictionary | `PdfXmpMetadata`, `BindPdf`, `XmpValue` | Demonstrates how to read XMP metadata from a PDF using Aspose.Pdf and convert the key/value pairs... |
| [extract-xmp-metadata-from-pdf](./extract-xmp-metadata-from-pdf.cs) | Extract XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF with PdfXmpMetadata, retrieve its raw XMP XML as a UTF‑8 string, and opti... |
| [handle-encrypted-pdf-bindpdf-decrypt](./handle-encrypted-pdf-bindpdf-decrypt.cs) | Handle Encrypted PDF with BindPdf and Decrypt | `PdfExtractor`, `PdfFileSecurity`, `BindPdf` | Demonstrates catching InvalidPasswordException when binding an encrypted PDF, decrypting it with ... |
| [insert-pages-update-xmp-metadata](./insert-pages-update-xmp-metadata.cs) | Insert Pages and Update XMP Metadata in PDF | `TryInsert`, `PdfFileEditor`, `PdfXmpMetadata` | Shows how to insert selected pages from one PDF into another and then add or update XMP metadata ... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `Keys` | Demonstrates how to bind a PDF file with Aspose.Pdf.Facades.PdfXmpMetadata, extract all XMP prope... |
| [load-pdf-retrieve-xmp-metadata](./load-pdf-retrieve-xmp-metadata.cs) | Load PDF and Retrieve XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF file to a PdfXmpMetadata facade and extract its XMP metadata as XM... |
| [log-original-xmp-metadata](./log-original-xmp-metadata.cs) | Log Original XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to extract the original XMP XML from a PDF using Aspose.Pdf's PdfXmpMetadata facade and... |
| [log-warning-existing-xmp-properties](./log-warning-existing-xmp-properties.cs) | Log Warning When Updating Existing XMP Metadata | `Document`, `PdfXmpMetadata`, `DefaultMetadataProperties` | Demonstrates how to add XMP metadata to a PDF only if the property is empty, logging a warning wh... |
| [log-xmp-metadata-size-before-after-modification](./log-xmp-metadata-size-before-after-modification.cs) | Log XMP Metadata Size Before and After Modification | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Demonstrates how to read, modify, and log the size of XMP metadata in a PDF using Aspose.Pdf's XM... |
| [modify-xmp-metadata-in-pdf](./modify-xmp-metadata-in-pdf.cs) | Modify XMP Metadata in PDF using Aspose.Pdf | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to load a PDF, extract its XMP metadata as XML, change a property (e.g., dc:creator) an... |
| [preserve-xmp-metadata-when-splitting-pdf](./preserve-xmp-metadata-when-splitting-pdf.cs) | Preserve XMP Metadata When Splitting PDF into Single Pages | `PdfXmpMetadata`, `SplitToPages`, `Document` | Demonstrates how to extract XMP metadata from a source PDF, split the document into individual pa... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF via UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF located on a network share using a UNC path and extract its XMP metadata,... |
| [read-xmp-metadata-from-pdf](./read-xmp-metadata-from-pdf.cs) | Read XMP Metadata from PDF using Aspose.Pdf | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF file with Aspose.Pdf.Facades.PdfXmpMetadata, retrieve its XMP pack... |
| [refresh-creator-tool-metadata-nightly](./refresh-creator-tool-metadata-nightly.cs) | Refresh CreatorTool Metadata for All PDFs Nightly | `Document`, `DocumentInfo`, `Creator` | Shows how to walk through a PDF repository, update the Creator metadata field for each document, ... |
| [remove-nickname-from-pdf-xmp-metadata](./remove-nickname-from-pdf-xmp-metadata.cs) | Remove Nickname Element from PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Remove` | Demonstrates how to bind a PDF, remove the Nickname element from its XMP metadata, and save the u... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `Clear` | Shows how to delete the entire XMP metadata block from a PDF using Aspose.Pdf.Facades and save th... |
| [replace-pdf-xmp-metadata](./replace-pdf-xmp-metadata.cs) | Replace PDF XMP Metadata from External File | `Document`, `SetXmpMetadata`, `PdfFileInfo` | Shows how to load a PDF, replace its existing XMP metadata with an external .xmp file, and save t... |
| ... | | | *and 15 more files* |

## Category Statistics
- Total examples: 45

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-07-05 | Run: `20260705_005655_3d29fa`
<!-- AUTOGENERATED:END -->
