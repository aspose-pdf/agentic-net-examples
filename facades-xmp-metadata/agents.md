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

- `using Aspose.Pdf.Facades;` (42/45 files) ← category-specific
- `using Aspose.Pdf;` (29/45 files) ← category-specific
- `using Aspose.Pdf.Text;` (3/45 files)
- `using Aspose.Pdf.Devices;` (1/45 files)
- `using Aspose.Pdf.Forms;` (1/45 files)
- `using Aspose.Pdf.Optimization;` (1/45 files)
- `using System;` (45/45 files)
- `using System.IO;` (39/45 files)
- `using System.Text;` (6/45 files)
- `using System.Xml;` (5/45 files)
- `using System.Collections.Generic;` (2/45 files)
- `using NUnit.Framework;` (1/45 files)
- `using Newtonsoft.Json;` (1/45 files)
- `using System.Diagnostics;` (1/45 files)
- `using System.Threading;` (1/45 files)
- `using System.Xml.Linq;` (1/45 files)

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
| [add-creator-metadata-to-pdf](./add-creator-metadata-to-pdf.cs) | Add Creator Metadata to PDF using Aspose PDF Facades | `Document`, `PdfFileInfo` | Shows how to create a reusable extension method that sets or updates the Creator metadata of an A... |
| [add-minimal-xmp-metadata-to-pdf](./add-minimal-xmp-metadata-to-pdf.cs) | Add Minimal XMP Metadata to PDF When Missing | `Document`, `PdfXmpMetadata`, `DefaultMetadataProperties` | Demonstrates how to detect the absence of XMP metadata in a PDF, add a minimal set of essential m... |
| [add-timestamp-to-pdf-xmp-metadata](./add-timestamp-to-pdf-xmp-metadata.cs) | Add Timestamp to PDF XMP Metadata | `Document`, `Page`, `TextFragment` | Shows how to create a PDF, add a text page, and embed an ISO‑8601 timestamp into the XMP metadata... |
| [clear-xmp-metadata-pdf](./clear-xmp-metadata-pdf.cs) | Clear XMP Metadata from PDF while Preserving Header | `PdfXmpMetadata`, `BindPdf`, `Clear` | Shows how to remove all XMP metadata from a PDF using Aspose.Pdf.Facades, leaving only the mandat... |
| [compress-pdf-read-xmp-metadata](./compress-pdf-read-xmp-metadata.cs) | Compress PDF with High Settings and Read XMP Metadata | `Document`, `OptimizeResources`, `Save` | Demonstrates applying high‑compression optimization to a PDF using Aspose.Pdf and then extracting... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata and Merge PDFs | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates extracting XMP metadata from one PDF, applying it to another PDF, and then merging t... |
| [create-pdf-with-xmp-metadata](./create-pdf-with-xmp-metadata.cs) | Create PDF with Custom XMP Metadata | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to generate a new PDF, build an XMP metadata block from scratch, add standard and custo... |
| [decrypt-encrypted-pdf-with-error-handling](./decrypt-encrypted-pdf-with-error-handling.cs) | Decrypt Encrypted PDF with Error Handling | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Shows how to bind, decrypt, and save an encrypted PDF using Aspose.Pdf.Facades.PdfFileSecurity wh... |
| [decrypt-update-creator-reencrypt-pdf](./decrypt-update-creator-reencrypt-pdf.cs) | Decrypt, Update Creator Metadata, and Re‑encrypt PDF | `PdfFileSecurity`, `DecryptFile`, `PdfFileInfo` | Demonstrates how to decrypt a password‑protected PDF, modify its Creator metadata using PdfFileIn... |
| [detect-xmp-metadata-in-pdf](./detect-xmp-metadata-in-pdf.cs) | Detect XMP Metadata in a PDF | `Document`, `Save`, `PdfXmpMetadata` | Demonstrates how to determine if a PDF file contains XMP metadata using the Aspose.Pdf Facades AP... |
| [disable-baseurl-injection-for-pdf](./disable-baseurl-injection-for-pdf.cs) | Disable BaseUrl Injection for PDF Generation | `Document`, `HtmlLoadOptions`, `PdfConverter` | Demonstrates how to use a configuration switch to disable BaseUrl injection when converting HTML ... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a PDF with Aspose.Pdf, convert the XML to JSON, and save i... |
| [export-xmp-metadata-to-sidecar](./export-xmp-metadata-to-sidecar.cs) | Export XMP Metadata from PDF to Side‑car File | `Document`, `GetXmpMetadata` | Demonstrates how to load a PDF with Aspose.Pdf, extract its XMP metadata, and write it to a separ... |
| [extract-xmp-metadata-from-in-memory-pdf](./extract-xmp-metadata-from-in-memory-pdf.cs) | Extract XMP Metadata from an In-Memory PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to create a PDF document in memory, bind it to the PdfXmpMetadata facade, and re... |
| [extract-xmp-metadata-from-large-pdf](./extract-xmp-metadata-from-large-pdf.cs) | Extract XMP Metadata from Large PDF and Measure Extraction T... | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF with PdfXmpMetadata, retrieve its XMP packet as XML bytes, and benchmark ... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadata`, `BindPdf`, `XmpValue` | Shows how to read XMP metadata from a PDF with Aspose.Pdf and convert it into a nested dictionary... |
| [insert-blank-page-update-xmp-metadata](./insert-blank-page-update-xmp-metadata.cs) | Insert Blank Page and Update XMP Metadata in PDF | `Document`, `Add`, `PdfXmpMetadata` | Demonstrates how to add a new blank page to a PDF and modify its XMP metadata using Aspose.Pdf be... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract the XMP packet from a PDF with Aspose.Pdf and enumerate the namespace declar... |
| [load-pdf-retrieve-xmp-metadata](./load-pdf-retrieve-xmp-metadata.cs) | Load PDF and Retrieve XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF file to a PdfXmpMetadata facade and extract the document's XMP metadata a... |
| [log-original-xmp-metadata](./log-original-xmp-metadata.cs) | Log Original XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Shows how to extract the XMP metadata from a PDF using Aspose.Pdf, display it on the console, and... |
| [log-warnings-when-overwriting-existing-xmp-propert...](./log-warnings-when-overwriting-existing-xmp-properties.cs) | Log Warnings When Overwriting Existing XMP Properties | `PdfXmpMetadata`, `Document`, `XmpValue` | The example demonstrates how to add XMP metadata to a PDF with Aspose.Pdf while checking for exis... |
| [log-xmp-metadata-size-before-after-modification](./log-xmp-metadata-size-before-after-modification.cs) | Log XMP Metadata Size Before and After Modification | `Document`, `GetXmpMetadata`, `Add` | Demonstrates how to read, modify, and save XMP metadata in a PDF while logging the metadata block... |
| [minimal-aspdf-console-entry](./minimal-aspdf-console-entry.cs) | Minimal Aspose.Pdf Console Entry for Unit Tests |  | Provides a minimal console program required to build a unit‑test‑only project that references Asp... |
| [modify-xmp-metadata-in-pdf](./modify-xmp-metadata-in-pdf.cs) | Modify XMP Metadata in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to load XMP metadata from a PDF, edit an XML node such as dc:creator, and write the upd... |
| [nightly-refresh-pdf-creator-metadata](./nightly-refresh-pdf-creator-metadata.cs) | Nightly Refresh of PDF Creator Metadata | `Document`, `DocumentInfo` | Demonstrates how to update the Creator metadata field for all PDFs in a repository and schedule t... |
| [preserve-xmp-metadata-when-splitting-pdf-pages](./preserve-xmp-metadata-when-splitting-pdf-pages.cs) | Preserve XMP Metadata When Splitting PDF Pages | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to extract XMP metadata from a source PDF, split it into individual pages, and a... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF via UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF located on a network share using a UNC path and extract its XMP metadata ... |
| [read-xmp-metadata-from-pdf](./read-xmp-metadata-from-pdf.cs) | Read XMP Metadata from a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | The example loads a PDF, extracts its XMP packet using Aspose.Pdf, parses the XML, and reads comm... |
| [remove-nickname-from-pdf-xmp-metadata](./remove-nickname-from-pdf-xmp-metadata.cs) | Remove Nickname from PDF XMP Metadata | `PdfXmpMetadata`, `DefaultMetadataProperties`, `BindPdf` | Shows how to load a PDF, delete the Nickname element from its XMP metadata using Aspose.Pdf.Facad... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `Clear` | Shows how to use Aspose.Pdf.Facades.PdfXmpMetadata to clear the XMP metadata block from a PDF and... |
| ... | | | *and 15 more files* |

## Category Statistics
- Total examples: 45

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-10 | Run: `20260410_113311_e93f13`
<!-- AUTOGENERATED:END -->
