---
name: facades-xmp-metadata
description: C# examples for facades-xmp-metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-xmp-metadata

> **Facades XMP metadata** in PDF using C# / .NET -- **43** verified, compile-tested examples for **Aspose.PDF for .NET** 26.10.0. Each `.cs` file is a standalone, build-validated console example, generated and runtime-checked by an AI agent before publishing.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-xmp-metadata** category.
This folder contains standalone C# examples for facades-xmp-metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-xmp-metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (38/43 files) ← category-specific
- `using Aspose.Pdf.Facades;` (25/43 files) ← category-specific
- `using Aspose.Pdf.Text;` (2/43 files)
- `using Aspose.Pdf.Annotations;` (1/43 files)
- `using Aspose.Pdf.LogicalStructure;` (1/43 files)
- `using Aspose.Pdf.Optimization;` (1/43 files)
- `using Aspose.Pdf.Tagged;` (1/43 files)
- `using System;` (43/43 files)
- `using System.IO;` (41/43 files)
- `using System.Text;` (8/43 files)
- `using System.Collections.Generic;` (4/43 files)
- `using System.Linq;` (3/43 files)
- `using System.Xml.Linq;` (3/43 files)
- `using System.Text.Json;` (2/43 files)
- `using System.Diagnostics;` (1/43 files)
- `using System.Reflection;` (1/43 files)
- `using System.Xml;` (1/43 files)
- `using System.Xml.Schema;` (1/43 files)

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
| [add-minimal-xmp-metadata-fallback](./add-minimal-xmp-metadata-fallback.cs) | Generate Minimal XMP Metadata When PDF Lacks Metadata | `Document`, `Metadata`, `PdfXmpMetadata` | The example checks a PDF for existing metadata, copies it if present, and otherwise creates minim... |
| [add-xmp-timestamp-to-pdf](./add-xmp-timestamp-to-pdf.cs) | Add XMP Timestamp Metadata to PDF | `Document`, `PdfXmpMetadata`, `TextFragment` | Creates a PDF with visible text and attaches XMP metadata that includes a standard creation date ... |
| [clear-xmp-metadata-pdf](./clear-xmp-metadata-pdf.cs) | Clear All XMP Metadata from a PDF | `Document`, `Metadata`, `Save` | Shows how to remove all custom XMP metadata entries from a PDF while keeping the mandatory PDF sc... |
| [compress-pdf-read-xmp-metadata](./compress-pdf-read-xmp-metadata.cs) | Compress PDF with High Compression and Read XMP Metadata | `Document`, `OptimizationOptions`, `All` | The example loads a PDF, applies high‑compression optimization settings, saves the compressed fil... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata and Merge PDFs | `Document`, `Metadata`, `Pages` | Demonstrates how to copy XMP metadata from one PDF to another using Aspose.Pdf and then merge the... |
| [create-pdf-with-xmp-metadata](./create-pdf-with-xmp-metadata.cs) | Create PDF with XMP Metadata from Scratch | `Document`, `PdfXmpMetadata`, `Add` | Demonstrates how to generate a new PDF document, build a fresh XMP metadata block using the PdfXm... |
| [decrypt-update-creatortool-reencrypt-pdf](./decrypt-update-creatortool-reencrypt-pdf.cs) | Decrypt PDF, Update Creator Metadata, and Re‑encrypt | `Document`, `DocumentInfo`, `Permissions` | Shows how to open a password‑protected PDF, modify its Creator metadata, and then re‑encrypt it w... |
| [detect-and-modify-xmp-metadata](./detect-and-modify-xmp-metadata.cs) | Detect XMP Metadata and Annotate PDF | `Document`, `Page`, `TextAnnotation` | The example checks whether a PDF contains XMP metadata and, if present, adds a text annotation to... |
| [disable-baseurl-injection-html-to-pdf](./disable-baseurl-injection-html-to-pdf.cs) | Conditional BaseFolder Injection for HTML to PDF Conversion | `Document`, `HtmlLoadOptions`, `Save` | Shows how to convert an HTML file to PDF with Aspose.Pdf while using a configuration switch to di... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `Document`, `Metadata` | Shows how to read XMP metadata from a PDF with Aspose.Pdf and serialize it into a formatted JSON ... |
| [export-xmp-metadata-from-pdf](./export-xmp-metadata-from-pdf.cs) | Export XMP Metadata from PDF to Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF, retrieve its XMP packet using Aspose.Pdf.Facades, and write the m... |
| [extract-xmp-metadata-from-pdf__v2](./extract-xmp-metadata-from-pdf__v2.cs) | Extract Raw XMP Metadata from a PDF | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | Shows how to load a PDF with Aspose.Pdf and retrieve its embedded XMP metadata as a UTF-8 XML str... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `Document`, `Metadata`, `XmpValue` | Shows how to load a PDF using Aspose.Pdf, read its XMP metadata and return the entries as a flat ... |
| [insert-pages-update-xmp-metadata](./insert-pages-update-xmp-metadata.cs) | Copy XMP Metadata and Insert PDF Pages | `Document`, `PdfFileEditor`, `Metadata` | Shows how to copy XMP metadata from a source PDF to a target PDF, concatenate the documents to in... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF with Aspose.Pdf.Facades, retrieve its XMP metadata packet, and enumerate ... |
| [load-encrypted-pdf-decrypt-save](./load-encrypted-pdf-decrypt-save.cs) | Handle Encrypted PDFs with PdfFileMend BindPdf | `PdfFileMend`, `BindPdf`, `Save` | Demonstrates how to catch encryption errors when binding a PDF with PdfFileMend, decrypt the docu... |
| [log-original-xmp-metadata](./log-original-xmp-metadata.cs) | Log Original XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a PDF using the PdfXmpMetadata facade, write it to an XML ... |
| [log-xmp-metadata-size-before-after-modification](./log-xmp-metadata-size-before-after-modification.cs) | Log XMP Metadata Size Before and After Modification | `Document`, `XmpValue`, `Save` | Shows how to read the XMP block size, update a metadata entry, and log the size again using Aspos... |
| [modify-xmp-creator-property](./modify-xmp-creator-property.cs) | Update XMP Metadata Property in PDF | `Document`, `Metadata`, `Save` | Demonstrates how to read, modify, and write an XMP metadata property (e.g., dc:creator) in a PDF ... |
| [persist-custom-xmp-metadata-pdf](./persist-custom-xmp-metadata-pdf.cs) | Write and Verify PDF Metadata (BaseUrl, Creator, Nickname) | `Document`, `DocumentInfo`, `Save` | Demonstrates how to set custom and standard PDF metadata using Aspose.Pdf, save the document, and... |
| [read-xmp-metadata-from-large-pdf](./read-xmp-metadata-from-large-pdf.cs) | Benchmark XMP Metadata Extraction from Large PDF | `Document`, `Metadata`, `MetadataDictionary` | Demonstrates how to read XMP metadata from a PDF using Aspose.Pdf and measures the time required ... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF on UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates loading a PDF located on a network share via a UNC path and extracting its XMP metad... |
| [read-xmp-metadata-from-pdf](./read-xmp-metadata-from-pdf.cs) | Bind PDF to XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `SetTitle` | Shows how to load an existing PDF, bind it to a PdfXmpMetadata object, optionally modify XMP fiel... |
| [read-xmp-metadata-from-pdf__v2](./read-xmp-metadata-from-pdf__v2.cs) | Read XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `GetXmpMetadata` | The example loads a PDF, extracts its XMP packet using the PdfXmpMetadata facade, parses the XML ... |
| [refresh-creatortool-metadata-for-pdfs](./refresh-creatortool-metadata-for-pdfs.cs) | Refresh Creator Metadata for PDFs | `Document`, `Info`, `Creator` | Shows how to scan a repository of PDF files and update each document's Creator metadata field and... |
| [remove-nickname-from-xmp-metadata](./remove-nickname-from-xmp-metadata.cs) | Remove Nickname Element from PDF XMP Metadata | `Document`, `Metadata`, `Save` | Shows how to load a PDF, access its XMP metadata dictionary, delete the "xmp:Nickname" entry, and... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `Document`, `Metadata`, `MetadataDictionary` | Shows how to delete the XMP metadata block from a PDF by clearing the Document.Metadata dictionar... |
| [replace-pdf-xmp-metadata](./replace-pdf-xmp-metadata.cs) | Replace PDF XMP Metadata from External File | `Document`, `Metadata`, `Save` | Shows how to import an external .xmp file and replace the existing XMP metadata block in a PDF us... |
| [set-baseurl-in-pdf-xmp-metadata](./set-baseurl-in-pdf-xmp-metadata.cs) | Set BaseUrl in PDF XMP Metadata | `Document`, `Metadata`, `Save` | Demonstrates how to assign a BaseURL value to a PDF's XMP metadata using Aspose.Pdf and save the ... |
| [set-creator-tool-xmp-metadata](./set-creator-tool-xmp-metadata.cs) | Set XMP CreatorTool Property to Application Version | `Document`, `Metadata`, `Save` | Shows how to obtain the current assembly version and assign it to the XMP CreatorTool metadata fi... |
| ... | | | *and 13 more files* |

## Category Statistics
- Total examples: 43

## General Tips
- See parent [AGENTS.md](../AGENTS.md) for:
  - **Boundaries** -- Always / Ask First / Never rules for all examples
  - **Common Mistakes** -- verified anti-patterns that cause build failures
  - **Domain Knowledge** -- cross-cutting API-specific gotchas
  - **Testing Guide** -- build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-11-12 | Run: `20261112_033301_d7e852`
<!-- AUTOGENERATED:END -->
