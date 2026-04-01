---
name: Facades - XMP metadata
description: C# examples for Facades - XMP metadata using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Facades - XMP metadata

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Facades - XMP metadata** category.
This folder contains standalone C# examples for Facades - XMP metadata operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Facades - XMP metadata**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (34/43 files) ← category-specific
- `using Aspose.Pdf.Facades;` (32/43 files) ← category-specific
- `using Aspose.Pdf.Optimization;` (1/43 files)
- `using Aspose.Pdf.Text;` (1/43 files)
- `using Aspose.Pdf.XfaConverter;` (1/43 files)
- `using System;` (43/43 files)
- `using System.IO;` (41/43 files)
- `using System.Text;` (4/43 files)
- `using System.Xml;` (3/43 files)
- `using System.Collections.Generic;` (2/43 files)
- `using NUnit.Framework;` (1/43 files)
- `using System.Diagnostics;` (1/43 files)
- `using System.Drawing;` (1/43 files)
- `using System.Runtime.CompilerServices;` (1/43 files)
- `using System.Runtime.InteropServices;` (1/43 files)
- `using System.Text.Json;` (1/43 files)
- `using System.Xml.Linq;` (1/43 files)
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
| [add-creator-metadata](./add-creator-metadata.cs) | Add Creator Metadata to PDF via Extension Method | `Document`, `PdfFileInfo` | Demonstrates a reusable extension method that sets the Creator metadata on any Aspose.Pdf Document. |
| [add-timestamp-xmp-metadata](./add-timestamp-xmp-metadata.cs) | Add Timestamp to XMP Metadata in PDF | `Document`, `Page`, `TextFragment` | Creates a PDF, adds a page with sample text, and inserts a timestamp into the XMP metadata using ... |
| [benchmark-xmp-metadata](./benchmark-xmp-metadata.cs) | Benchmark XMP Metadata Extraction from Large PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Measures the time required to read XMP metadata from a PDF with many pages using Aspose.Pdf. |
| [bind-pdf-byte-array-xmpmetadata](./bind-pdf-byte-array-xmpmetadata.cs) | Bind PDF from Byte Array to PdfXmpMetadata | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF loaded from a byte array to the PdfXmpMetadata facade and extract ... |
| [bind-pdf-xmpmetadata](./bind-pdf-xmpmetadata.cs) | Bind PDF to PdfXmpMetadata and Retrieve XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates loading a PDF file and binding it to a PdfXmpMetadata object to read the XMP metadata. |
| [clear-xmp-metadata](./clear-xmp-metadata.cs) | Clear XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `Clear` | Demonstrates how to remove all XMP metadata from a PDF, leaving only the required schema header. |
| [compress-pdf-read-xmp](./compress-pdf-read-xmp.cs) | Compress PDF and Read XMP Metadata | `Document`, `CompressionLevel`, `PdfXmpMetadata` | Demonstrates compressing a PDF with high compression settings and then reading its XMP metadata. |
| [copy-xmp-metadata-merge-pdf](./copy-xmp-metadata-merge-pdf.cs) | Copy XMP Metadata and Merge PDFs | `PdfXmpMetadata`, `SetXmpMetadata`, `Add` | Demonstrates extracting XMP metadata from a source PDF, applying it to a target PDF, and then mer... |
| [decrypt-update-reencrypt-pdf](./decrypt-update-reencrypt-pdf.cs) | Decrypt, Update CreatorTool, and Re‑Encrypt PDF | `Document`, `Decrypt`, `Encrypt` | Shows how to open a password‑protected PDF, decrypt it, modify the CreatorTool metadata, and re‑e... |
| [detect-xmp-metadata](./detect-xmp-metadata.cs) | Detect and Add XMP Metadata to PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to check if a PDF contains XMP metadata using PdfXmpMetadata and optionally add a custo... |
| [export-xmp-metadata-json](./export-xmp-metadata-json.cs) | Export PDF XMP Metadata as JSON | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to extract XMP metadata from a PDF using Aspose.Pdf and convert it to JSON for i... |
| [export-xmp-metadata](./export-xmp-metadata.cs) | Export XMP Metadata from PDF to Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to extract XMP metadata from a PDF using Aspose.Pdf and save it as a separate .x... |
| [extract-xmp-metadata](./extract-xmp-metadata.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadata`, `BindPdf`, `ToStringValue` | Demonstrates how to read XMP metadata from a PDF file and convert it into a simple Dictionary<str... |
| [generate-minimal-xmp-metadata](./generate-minimal-xmp-metadata.cs) | Generate Minimal XMP Metadata When Missing | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Loads a PDF, checks for existing XMP metadata, and adds a minimal set of metadata if none is pres... |
| [handle-encrypted-pdf-binding](./handle-encrypted-pdf-binding.cs) | Handle Encrypted PDF Binding Errors | `BindPdf`, `Save`, `InvalidPasswordException` | Demonstrates how to catch InvalidPasswordException when binding an encrypted PDF using a facade. |
| [html-to-pdf-baseurl-switch](./html-to-pdf-baseurl-switch.cs) | Generate PDF from HTML with optional BaseUrl injection | `Document`, `HtmlLoadOptions` | Demonstrates how to disable BaseUrl injection when converting HTML to PDF by using HtmlLoadOption... |
| [insert-page-update-xmp](./insert-page-update-xmp.cs) | Insert Page and Update XMP Metadata in PDF | `Document`, `Page`, `Insert` | Demonstrates how to insert a new page into a PDF and update its XMP metadata before saving using ... |
| [list-xmp-namespaces](./list-xmp-namespaces.cs) | List XMP Namespaces in PDF | `PdfXmpMetadata`, `BindPdf`, `Keys` | Demonstrates how to extract and display all XMP namespace prefixes and their URIs from a PDF file... |
| [log-original-xmp-metadata](./log-original-xmp-metadata.cs) | Log Original XMP Metadata from PDF | `Document`, `GetXmpMetadata` | Demonstrates how to read and log the original XMP XML metadata from a PDF document before any cha... |
| [log-xmp-overwrite-warning](./log-xmp-overwrite-warning.cs) | Log Warning When Overwriting Existing XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates checking existing XMP metadata and logging a warning if a property already has a non... |
| [log-xmp-size](./log-xmp-size.cs) | Log XMP Metadata Size Before and After Modification | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to read the XMP metadata block size, modify it, and log the size before and afte... |
| [modify-xmp-metadata](./modify-xmp-metadata.cs) | Modify XMP Metadata Property in PDF | `Document`, `PdfXmpMetadata`, `SetXmpMetadata` | Demonstrates how to read XMP metadata from a PDF, change a property, and write the updated metada... |
| [modify-xmp-metadata__v2](./modify-xmp-metadata__v2.cs) | Modify XMP Metadata and Save PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates how to edit XMP metadata of a PDF using Aspose.Pdf and save the updated document. |
| [read-xmp-metadata-unc](./read-xmp-metadata-unc.cs) | Read XMP Metadata from PDF on UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to load a PDF from a network share using a UNC path and extract its XMP metadata... |
| [read-xmp-schema-properties](./read-xmp-schema-properties.cs) | Read Specific XMP Schema Properties from PDF | `PdfXmpMetadata`, `XmlDocument`, `XmlNamespaceManager` | Demonstrates how to retrieve XMP metadata from a PDF, parse the XML, and extract specific schema ... |
| [refresh-creator-tool](./refresh-creator-tool.cs) | Refresh CreatorTool Metadata for PDFs Nightly | `Document`, `Creator`, `Save` | A console app that scans a repository of PDF files, updates the Creator metadata field, and saves... |
| [remove-nickname-xmp](./remove-nickname-xmp.cs) | Remove Nickname from PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Remove` | Demonstrates how to delete the Nickname element from a PDF's XMP metadata using Aspose.Pdf. |
| [remove-xmp-metadata](./remove-xmp-metadata.cs) | Remove XMP Metadata from PDF | `Document`, `RemoveMetadata`, `Save` | Demonstrates how to delete the entire XMP metadata block from a PDF, producing a metadata‑free file. |
| [replace-pdf-xmp-metadata](./replace-pdf-xmp-metadata.cs) | Replace PDF XMP Metadata from External File | `SetXmpMetadata`, `Save` | Loads a PDF, replaces its XMP metadata block with the contents of an external .xmp file, and save... |
| [retrieve-xmp-metadata](./retrieve-xmp-metadata.cs) | Retrieve XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to extract the raw XMP XML metadata from a PDF using Aspose.Pdf's PdfXmpMetadata... |
| ... | | | *and 13 more files* |

## Category Statistics
- Total examples: 43

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for Facades - XMP metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-01 | Run: `20260401_153432_9ad69f`
<!-- AUTOGENERATED:END -->
