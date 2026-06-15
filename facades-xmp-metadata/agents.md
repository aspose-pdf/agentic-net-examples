---
name: facades-xmp-metadata
description: C# examples for facades-xmp-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-xmp-metadata

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-xmp-metadata** category.
This folder contains standalone C# examples for facades-xmp-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-xmp-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (40/41 files) ← category-specific
- `using Aspose.Pdf;` (23/41 files) ← category-specific
- `using Aspose.Pdf.Optimization;` (1/41 files)
- `using Aspose.Pdf.Text;` (1/41 files)
- `using System;` (41/41 files)
- `using System.IO;` (37/41 files)
- `using System.Collections.Generic;` (4/41 files)
- `using System.Xml;` (4/41 files)
- `using System.Text;` (3/41 files)
- `using NUnit.Framework;` (2/41 files)
- `using System.Diagnostics;` (1/41 files)
- `using System.Runtime.InteropServices;` (1/41 files)
- `using System.Text.Json;` (1/41 files)

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
| [add-creator-metadata-to-pdf](./add-creator-metadata-to-pdf.cs) | Add Creator Metadata to PDF Document | `Document`, `DocumentInfo`, `Creator` | Shows how to create a reusable extension method that sets the Creator field in a PDF's metadata u... |
| [add-minimal-xmp-metadata-to-pdf](./add-minimal-xmp-metadata-to-pdf.cs) | Add Minimal XMP Metadata to PDF When Missing | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Shows how to detect the absence of XMP metadata in a PDF and add a minimal set (title and creator... |
| [add-timestamp-to-pdf-xmp-metadata](./add-timestamp-to-pdf-xmp-metadata.cs) | Add Timestamp to PDF XMP Metadata | `Document`, `Page`, `TextFragment` | Demonstrates creating a PDF, inserting text, binding the XMP metadata facade, adding a custom ISO... |
| [benchmark-xmp-metadata-extraction](./benchmark-xmp-metadata-extraction.cs) | Benchmark XMP Metadata Extraction from Large PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to read XMP metadata from a large PDF and measure the elapsed time using Aspose.Pdf.Fac... |
| [check-xmp-metadata-in-pdf](./check-xmp-metadata-in-pdf.cs) | Check for XMP Metadata in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to detect whether a PDF file contains XMP metadata using Aspose.Pdf.Facades befo... |
| [clear-xmp-metadata-from-pdf](./clear-xmp-metadata-from-pdf.cs) | Clear XMP Metadata from PDF while Preserving Header | `PdfXmpMetadata`, `BindPdf`, `Clear` | Demonstrates how to remove all XMP metadata from a PDF document using Aspose.Pdf.Facades, leaving... |
| [compress-pdf-high-compression-read-xmp-metadata](./compress-pdf-high-compression-read-xmp-metadata.cs) | Compress PDF with High Settings and Read XMP Metadata | `Document`, `OptimizationOptions`, `OptimizeResources` | The example loads a PDF, applies aggressive optimization options to achieve high compression, sav... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata Between PDFs and Merge | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a source PDF, apply it to a target PDF, and then concatena... |
| [decrypt-encrypted-pdf-with-error-handling](./decrypt-encrypted-pdf-with-error-handling.cs) | Decrypt Encrypted PDF with Error Handling using PdfFileSecur... | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Demonstrates binding an encrypted PDF with PdfFileSecurity, handling InvalidPasswordException by ... |
| [decrypt-update-creator-metadata-reencrypt-pdf](./decrypt-update-creator-metadata-reencrypt-pdf.cs) | Decrypt PDF, Update Creator Metadata, and Re‑Encrypt | `Document`, `Decrypt`, `Info` | Demonstrates opening a password‑protected PDF, removing its encryption, modifying the Creator met... |
| [disable-baseurl-injection-for-pdf-generation](./disable-baseurl-injection-for-pdf-generation.cs) | Disable BaseUrl Injection for PDF Generation in Test Environ... | `Document`, `HtmlLoadOptions` | Demonstrates using an environment variable to toggle BaseUrl injection when converting HTML to PD... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to extract XMP metadata from a PDF using Aspose.Pdf.Facades.PdfXmpMetadata, conv... |
| [export-xmp-metadata-from-pdf](./export-xmp-metadata-from-pdf.cs) | Export XMP Metadata from PDF to Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF with the PdfXmpMetadata facade, extract its XMP metadata, and save... |
| [extract-audit-original-xmp-metadata](./extract-audit-original-xmp-metadata.cs) | Extract and Audit Original XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to load a PDF, retrieve its XMP metadata packet using Aspose.Pdf, and save the o... |
| [extract-xmp-metadata-from-pdf](./extract-xmp-metadata-from-pdf.cs) | Extract XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF with PdfXmpMetadata, retrieve the raw XMP XML as a UTF‑8 string, and save... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadata`, `BindPdf`, `GetEnumerator` | Shows how to read XMP metadata from a PDF file using Aspose.Pdf and return it as a case‑insensiti... |
| [insert-page-update-xmp-metadata](./insert-page-update-xmp-metadata.cs) | Insert Page and Update XMP Metadata in PDF | `Document`, `Add`, `PdfXmpMetadata` | Shows how to add a blank page to an existing PDF and modify its XMP metadata, including standard ... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `Keys` | Shows how to load XMP metadata from a PDF, extract unique namespace prefixes from the metadata ke... |
| [load-pdf-retrieve-xmp-metadata](./load-pdf-retrieve-xmp-metadata.cs) | Load PDF and Retrieve XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF file to a PdfXmpMetadata facade and extract its XMP metadata as a ... |
| [log-modify-xmp-metadata-size](./log-modify-xmp-metadata-size.cs) | Log and Modify XMP Metadata Size in PDF | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Demonstrates how to read the XMP metadata from a PDF, log its byte size before and after modifica... |
| [modify-xmp-metadata-node](./modify-xmp-metadata-node.cs) | Modify XMP Metadata Node in PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to read XMP metadata from a PDF, edit a specific node using XML and XPath, and write th... |
| [preserve-xmp-metadata-when-splitting-pdf-pages](./preserve-xmp-metadata-when-splitting-pdf-pages.cs) | Preserve XMP Metadata When Splitting PDF Pages | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract the original XMP metadata from a multi‑page PDF, split the document into sin... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF via UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to open a PDF located on a network share using a UNC path and extract its XMP metadata ... |
| [read-xmp-metadata-from-pdf](./read-xmp-metadata-from-pdf.cs) | Read XMP Metadata from PDF using Aspose.Pdf | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF with Aspose.Pdf, extract its XMP metadata as XML, and parse specific sche... |
| [remove-nickname-from-xmp-metadata](./remove-nickname-from-xmp-metadata.cs) | Remove Nickname Element from PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Remove` | Shows how to bind a PDF to the XMP metadata facade, delete the Nickname property, and save the mo... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `Clear` | Shows how to delete the entire XMP metadata block from a PDF using Aspose.Pdf.Facades and save th... |
| [replace-pdf-xmp-metadata-from-external-file](./replace-pdf-xmp-metadata-from-external-file.cs) | Replace PDF XMP Metadata from External File | `Document`, `SetXmpMetadata`, `PdfFileInfo` | Demonstrates how to import an external .xmp file and replace the existing XMP metadata block in a... |
| [set-baseurl-in-pdf-xmp-metadata](./set-baseurl-in-pdf-xmp-metadata.cs) | Set BaseURL in PDF XMP Metadata | `Document`, `PdfXmpMetadata`, `DefaultMetadataProperties` | Demonstrates how to bind an XMP metadata facade to a PDF document and set the BaseURL property, t... |
| [set-default-creatortool-metadata-pdf](./set-default-creatortool-metadata-pdf.cs) | Set Default CreatorTool Metadata in PDF | `Document`, `Info`, `Creator` | Demonstrates how to check the Creator metadata of a PDF and assign a default value from configura... |
| [set-pdf-xmp-nickname](./set-pdf-xmp-nickname.cs) | Set PDF XMP Nickname Property | `PdfXmpMetadata`, `BindPdf`, `Add` | Shows how to bind a PDF, add a custom Nickname to its XMP metadata, and save the updated file usi... |
| ... | | | *and 11 more files* |

## Category Statistics
- Total examples: 41

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-06-15 | Run: `20260615_022900_0adbaa`
<!-- AUTOGENERATED:END -->
