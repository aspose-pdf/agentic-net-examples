---
name: facades-xmp-metadata
description: C# examples for facades-xmp-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-xmp-metadata

> **Facades XMP metadata** in PDF using C# / .NET -- **44** verified, compile-tested examples for **Aspose.PDF for .NET** 26.8.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

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
- `using Aspose.Pdf;` (23/44 files) ← category-specific
- `using Aspose.Pdf.Text;` (2/44 files)
- `using Aspose.Pdf.Optimization;` (1/44 files)
- `using Aspose.Pdf.XfaConverter;` (1/44 files)
- `using System;` (44/44 files)
- `using System.IO;` (37/44 files)
- `using System.Text;` (7/44 files)
- `using System.Xml;` (5/44 files)
- `using NUnit.Framework;` (3/44 files)
- `using System.Xml.Linq;` (2/44 files)
- `using Newtonsoft.Json;` (1/44 files)
- `using System.Collections.Generic;` (1/44 files)
- `using System.Diagnostics;` (1/44 files)
- `using System.Drawing;` (1/44 files)
- `using System.Threading.Tasks;` (1/44 files)
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
| [add-minimal-xmp-metadata-fallback](./add-minimal-xmp-metadata-fallback.cs) | Add Minimal XMP Metadata Fallback to PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to detect missing XMP metadata in a PDF and inject a minimal set of metadata entries us... |
| [add-xmp-timestamp-to-pdf](./add-xmp-timestamp-to-pdf.cs) | Add XMP Timestamp to PDF during Generation | `Document`, `Page`, `TextFragment` | Shows how to create a PDF with Aspose.Pdf, add a page with sample text, and automatically embed a... |
| [bind-pdf-byte-array-to-pdfxmpmetadata](./bind-pdf-byte-array-to-pdfxmpmetadata.cs) | Bind PDF Byte Array to PdfXmpMetadata and Extract XMP | `Document`, `Add`, `Save` | Demonstrates how to create a PDF in memory, bind it from a byte array to the PdfXmpMetadata facad... |
| [clear-xmp-metadata-pdf](./clear-xmp-metadata-pdf.cs) | Clear All XMP Metadata from PDF while Preserving Header | `PdfXmpMetadata`, `BindPdf`, `Clear` | Demonstrates how to remove all XMP metadata from a PDF using Aspose.Pdf.Facades, keeping only the... |
| [compress-pdf-read-xmp-metadata](./compress-pdf-read-xmp-metadata.cs) | Compress PDF with High Settings and Read XMP Metadata | `Document`, `OptimizationOptions`, `OptimizeResources` | Demonstrates compressing a PDF using high‑compression optimization options and then extracting it... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata and Merge PDFs | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to extract XMP metadata from a source PDF with the PdfXmpMetadata facade, apply it to a... |
| [create-pdf-with-xmp-metadata](./create-pdf-with-xmp-metadata.cs) | Create PDF with XMP Metadata | `Document`, `Add`, `RegisterNamespaceUri` | Shows how to generate a new PDF, register XMP namespaces, add custom XMP metadata entries, and sa... |
| [decrypt-update-creatortool-reencrypt-pdf](./decrypt-update-creatortool-reencrypt-pdf.cs) | Decrypt PDF, Update CreatorTool, and Re‑encrypt | `Document`, `PdfFileSecurity`, `PdfFileInfo` | Demonstrates how to open a password‑protected PDF, decrypt it, modify the Creator metadata (Creat... |
| [detect-and-modify-xmp-metadata](./detect-and-modify-xmp-metadata.cs) | Detect and Modify XMP Metadata in PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF with Aspose.Pdf.Facades.PdfXmpMetadata, check for existing XMP met... |
| [disable-baseurl-injection-html-to-pdf](./disable-baseurl-injection-html-to-pdf.cs) | Disable BaseUrl Injection When Converting HTML to PDF | `Document`, `HtmlLoadOptions`, `PdfViewer` | Shows how to use a configuration switch to optionally omit the BaseUrl in HtmlLoadOptions while c... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a PDF using Aspose.Pdf.Facades and convert it into JSON fo... |
| [export-xmp-metadata-from-pdf](./export-xmp-metadata-from-pdf.cs) | Export XMP Metadata from PDF to a Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF document, retrieve its XMP metadata using Aspose.Pdf.Facades, and ... |
| [extract-xmp-metadata-from-pdf](./extract-xmp-metadata-from-pdf.cs) | Extract XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF with the PdfXmpMetadata facade, retrieve the raw XMP XML bytes, co... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to read the XMP packet from a PDF using Aspose.Pdf.Facades and convert the XML into a c... |
| [insert-pages-update-xmp-metadata](./insert-pages-update-xmp-metadata.cs) | Insert Pages and Update XMP Metadata in PDF | `PdfFileEditor`, `TryInsert`, `PdfXmpMetadata` | Demonstrates how to insert selected pages from one PDF into another and then add or overwrite XMP... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract the XMP metadata packet from a PDF using Aspose.Pdf and enumerate the namesp... |
| [load-encrypted-pdf-decrypt-save](./load-encrypted-pdf-decrypt-save.cs) | Load Encrypted PDF, Decrypt with Password, and Save Unprotec... | `Document`, `ctor(string)`, `ctor(string, string)` | Demonstrates how to detect an encrypted PDF, handle the InvalidPasswordException, load the docume... |
| [log-original-xmp-metadata](./log-original-xmp-metadata.cs) | Log Original XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF using the PdfXmpMetadata facade, extract the XMP metadata as XML, and sav... |
| [log-xmp-metadata-size-before-after-modification](./log-xmp-metadata-size-before-after-modification.cs) | Log XMP Metadata Size Before and After Modification | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Demonstrates how to retrieve, log, and modify the XMP metadata block of a PDF using Aspose.Pdf, s... |
| [modify-xmp-creator-property](./modify-xmp-creator-property.cs) | Modify XMP Creator Property in PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates reading XMP metadata from a PDF, updating the dc:creator element, and writing the mo... |
| [persist-custom-xmp-metadata-pdf](./persist-custom-xmp-metadata-pdf.cs) | Persist Custom XMP Metadata in PDF | `Document`, `PdfFileInfo`, `Creator` | The example creates a PDF, sets the Creator property and custom XMP metadata entries (BaseUrl and... |
| [read-xmp-metadata-from-large-pdf](./read-xmp-metadata-from-large-pdf.cs) | Read XMP Metadata from Large PDF and Measure Performance | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a large PDF file, extract its XMP metadata using Aspose.Pdf.Facades, and... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF via UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF located on a network share using a UNC path and extract its XMP metadata ... |
| [read-xmp-metadata-from-pdf](./read-xmp-metadata-from-pdf.cs) | Read XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF file to a PdfXmpMetadata facade and retrieve its XMP metadata as a... |
| [read-xmp-metadata-from-pdf__v2](./read-xmp-metadata-from-pdf__v2.cs) | Read and Parse XMP Metadata from a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF, extract its XMP metadata as XML, and read specific schema propert... |
| [refresh-creatortool-metadata-for-pdfs](./refresh-creatortool-metadata-for-pdfs.cs) | Refresh CreatorTool Metadata for PDFs | `PdfFileInfo`, `SaveNewInfo`, `Creator` | Shows how to scan a repository of PDF files and update the CreatorTool‑related XMP metadata (Crea... |
| [remove-nickname-from-xmp-metadata](./remove-nickname-from-xmp-metadata.cs) | Remove Nickname Element from PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Remove` | Shows how to bind a PDF, remove the Nickname property from its XMP metadata using the PdfXmpMetad... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `Clear` | Shows how to delete the entire XMP metadata block from a PDF using Aspose.Pdf.Facades and save a ... |
| [replace-pdf-xmp-metadata](./replace-pdf-xmp-metadata.cs) | Replace PDF XMP Metadata from External File | `Document`, `SetXmpMetadata`, `Save` | Demonstrates loading a PDF, reading an external XMP file and replacing the document's XMP metadat... |
| [set-baseurl-in-pdf-xmp-metadata](./set-baseurl-in-pdf-xmp-metadata.cs) | Set BaseURL in PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to add or replace the BaseURL property in a PDF's XMP metadata using the PdfXmpM... |
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
Updated: 2026-08-20 | Run: `20260820_001147_12d7b3`
<!-- AUTOGENERATED:END -->
