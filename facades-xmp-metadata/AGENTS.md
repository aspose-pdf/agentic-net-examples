---
name: facades-xmp-metadata
description: C# examples for facades-xmp-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-xmp-metadata

> **Facades XMP metadata** in PDF using C# / .NET -- **42** verified, compile-tested examples for **Aspose.PDF for .NET** 26.9.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-xmp-metadata** category.
This folder contains standalone C# examples for facades-xmp-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-xmp-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (33/42 files) ← category-specific
- `using Aspose.Pdf.Facades;` (20/42 files)
- `using Aspose.Pdf.Optimization;` (1/42 files)
- `using Aspose.Pdf.Text;` (1/42 files)
- `using System;` (42/42 files)
- `using System.IO;` (38/42 files)
- `using System.Text;` (7/42 files)
- `using System.Xml.Linq;` (6/42 files)
- `using System.Collections.Generic;` (4/42 files)
- `using NUnit.Framework;` (2/42 files)
- `using System.Linq;` (2/42 files)
- `using System.Diagnostics;` (1/42 files)
- `using System.Reflection;` (1/42 files)
- `using System.Text.Json;` (1/42 files)
- `using System.Xml;` (1/42 files)
- `using System.Xml.Schema;` (1/42 files)

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
| [add-minimal-xmp-metadata-fallback](./add-minimal-xmp-metadata-fallback.cs) | Add Minimal XMP Metadata Fallback to PDF | `Document`, `DocumentInfo`, `Save` | Shows how to detect missing PDF metadata, inject a minimal set of XMP‑like properties, and save t... |
| [add-xmp-timestamp-to-pdf](./add-xmp-timestamp-to-pdf.cs) | Add Timestamp to PDF XMP Metadata | `Document`, `Add`, `RegisterNamespaceUri` | Demonstrates how to embed a creation date and a custom timestamp into a PDF's XMP metadata using ... |
| [clear-xmp-metadata-pdf](./clear-xmp-metadata-pdf.cs) | Clear XMP Metadata from PDF, Keeping Only PDF Schema Header | `Document`, `Metadata`, `Save` | Demonstrates how to remove all XMP metadata from a PDF using Aspose.Pdf, leaving only the minimal... |
| [compress-pdf-read-xmp-metadata](./compress-pdf-read-xmp-metadata.cs) | Compress PDF with High Compression and Read XMP Metadata | `Document`, `OptimizationOptions`, `All` | The example compresses a PDF using high‑compression optimization settings and then loads the comp... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata and Merge PDFs | `Document`, `Metadata`, `Save` | Shows how to copy XMP metadata from a source PDF to a target PDF and then concatenate additional ... |
| [create-pdf-with-xmp-metadata](./create-pdf-with-xmp-metadata.cs) | Create PDF with XMP Metadata | `Document`, `Page`, `TextFragment` | Shows how to generate a PDF, register a Dublin Core namespace, add XMP properties, and save the d... |
| [decrypt-update-creatortool-reencrypt-pdf](./decrypt-update-creatortool-reencrypt-pdf.cs) | Decrypt PDF, Update Creator Metadata, and Re‑Encrypt | `Document`, `PdfFileEditor`, `Info` | Demonstrates opening a password‑protected PDF, changing the Creator metadata, and saving it again... |
| [detect-and-modify-xmp-metadata](./detect-and-modify-xmp-metadata.cs) | Detect and Modify PDF XMP Metadata with Aspose.Pdf | `Document`, `Metadata`, `Info` | The example loads a PDF, checks whether XMP metadata is present, and if found adds a custom docum... |
| [disable-baseurl-injection-html-to-pdf](./disable-baseurl-injection-html-to-pdf.cs) | Convert HTML to PDF with Optional BaseUrl Injection | `Document`, `HtmlLoadOptions`, `Save` | Demonstrates converting an HTML file to PDF using Aspose.Pdf while allowing BaseUrl injection to ... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Demonstrates how to read XMP metadata from a PDF using Aspose.Pdf.Facades.PdfXmpMetadata and seri... |
| [export-xmp-metadata-from-pdf](./export-xmp-metadata-from-pdf.cs) | Export XMP Metadata from PDF to Side‑car File | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Demonstrates how to extract raw XMP metadata from a PDF using Aspose.Pdf and save it as a separat... |
| [extract-xmp-metadata-from-pdf](./extract-xmp-metadata-from-pdf.cs) | Retrieve XMP Metadata from PDF | `Document`, `GetXmpMetadata` | Demonstrates how to load a PDF with Aspose.Pdf and extract its raw XMP metadata as a UTF‑8 XML st... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadataExtractor`, `ExtractXmpMetadata` | Demonstrates extracting raw XMP XML from a PDF using Aspose.Pdf and converting leaf elements into... |
| [insert-pages-update-xmp-metadata](./insert-pages-update-xmp-metadata.cs) | Insert Pages and Update XMP Metadata in PDF | `Document`, `Pages`, `Insert` | Demonstrates how to insert pages from one PDF into another and modify XMP‑style metadata using As... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Shows how to retrieve the raw XMP packet from a PDF using Aspose.Pdf and extract the declared XML... |
| [load-encrypted-pdf-decrypt-save](./load-encrypted-pdf-decrypt-save.cs) | Extract First Page from Encrypted PDF with Password Handling | `Document`, `Document(string)`, `Document(string, string)` | Demonstrates loading an encrypted PDF using Aspose.Pdf with password fallback, extracting the fir... |
| [log-original-xmp-metadata](./log-original-xmp-metadata.cs) | Log XMP Metadata and Add Blank Page | `PdfXmpMetadata`, `GetXmpMetadata`, `Document` | The example reads the original XMP metadata from a PDF using the PdfXmpMetadata facade, logs it t... |
| [log-xmp-metadata-size-before-after-modification](./log-xmp-metadata-size-before-after-modification.cs) | Log XMP Metadata Size Before and After Modification | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Loads a PDF, reads its XMP metadata, logs the size of the XMP block before and after adding a cus... |
| [modify-xmp-creator-property](./modify-xmp-creator-property.cs) | Modify XMP Metadata Property in PDF | `BindPdf`, `ExtractXmpMetadata`, `SetXmpMetadata` | Demonstrates extracting XMP metadata from a PDF, updating a specific property (dc:creator) using ... |
| [persist-custom-xmp-metadata-pdf](./persist-custom-xmp-metadata-pdf.cs) | Verify PDF Metadata Writing with Aspose.Pdf | `Document`, `DocumentInfo`, `Save` | Demonstrates setting custom and standard PDF metadata (BaseUrl, Creator, Title) using Aspose.Pdf,... |
| [read-xmp-metadata-from-large-pdf](./read-xmp-metadata-from-large-pdf.cs) | Benchmark Reading XMP Metadata from Large PDF | `PdfXmpMetadata`, `GetXmpMetadata` | Shows how to extract XMP metadata from a large PDF and measure the elapsed time using Aspose.Pdf.... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF via UNC Path | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Demonstrates how to load a PDF located on a network share using a UNC path and extract its XMP me... |
| [read-xmp-metadata-from-pdf](./read-xmp-metadata-from-pdf.cs) | Read XMP Metadata from PDF | `Document`, `Metadata`, `TryGetValue` | Shows how to load a PDF with Aspose.Pdf, retrieve the XMP metadata string, parse it as XML, and e... |
| [refresh-creatortool-metadata-for-pdfs](./refresh-creatortool-metadata-for-pdfs.cs) | Refresh Creator Metadata for PDFs | `Document`, `DocumentInfo`, `Info` | Shows how to run a console job that scans a repository of PDF files, updates the Creator metadata... |
| [remove-nickname-from-xmp-metadata](./remove-nickname-from-xmp-metadata.cs) | Remove Nickname from PDF XMP Metadata | `Document`, `Metadata`, `Save` | Demonstrates how to load a PDF, delete the XMP "Nickname" entry from its metadata dictionary, and... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `Document`, `Metadata`, `Save` | Shows how to load a PDF with Aspose.Pdf, clear its XMP metadata dictionary, and save a metadata‑f... |
| [replace-pdf-xmp-metadata](./replace-pdf-xmp-metadata.cs) | Replace PDF XMP Metadata from External File | `Document`, `Metadata`, `Save` | Demonstrates how to load a PDF, read an external .xmp file, replace the existing XMP metadata blo... |
| [set-baseurl-in-pdf-xmp-metadata](./set-baseurl-in-pdf-xmp-metadata.cs) | Set BaseURL in PDF XMP Metadata | `Document`, `Metadata`, `Save` | Shows how to load a PDF with Aspose.Pdf, assign a BaseURL value to the XMP metadata dictionary, a... |
| [set-creator-tool-xmp-metadata](./set-creator-tool-xmp-metadata.cs) | Set CreatorTool in PDF XMP Metadata | `Document`, `Metadata`, `Save` | Shows how to assign the XMP CreatorTool property of a PDF to the current application version usin... |
| [set-default-creator-metadata](./set-default-creator-metadata.cs) | Set PDF Creator Metadata with Default Configuration | `PdfFileInfo`, `Creator`, `Save` | Shows how to update the Creator metadata of a PDF using Aspose.Pdf.Facades, applying a configurab... |
| ... | | | *and 12 more files* |

## Category Statistics
- Total examples: 42

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-10-08 | Run: `20261008_043531_e14173`
<!-- AUTOGENERATED:END -->
