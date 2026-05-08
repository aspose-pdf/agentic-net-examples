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

- `using Aspose.Pdf.Facades;` (38/43 files) ← category-specific
- `using Aspose.Pdf;` (29/43 files) ← category-specific
- `using Aspose.Pdf.Optimization;` (1/43 files)
- `using Aspose.Pdf.XfaConverter;` (1/43 files)
- `using System;` (43/43 files)
- `using System.IO;` (39/43 files)
- `using System.Xml;` (4/43 files)
- `using System.Text;` (3/43 files)
- `using NUnit.Framework;` (2/43 files)
- `using System.Collections.Generic;` (2/43 files)
- `using System.Runtime.InteropServices;` (2/43 files)
- `using Newtonsoft.Json;` (1/43 files)
- `using System.Diagnostics;` (1/43 files)
- `using System.Drawing;` (1/43 files)
- `using System.Reflection;` (1/43 files)
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
| [add-minimal-xmp-metadata-to-pdf](./add-minimal-xmp-metadata-to-pdf.cs) | Add Minimal XMP Metadata to PDF When Missing | `Document`, `Metadata`, `RegisterNamespaceUri` | Demonstrates checking for existing XMP metadata in a PDF and adding a minimal XMP packet if none ... |
| [add-timestamp-to-pdf-xmp-metadata](./add-timestamp-to-pdf-xmp-metadata.cs) | Add Timestamp to PDF XMP Metadata | `Document`, `PdfXmpMetadata`, `Add` | Shows how to insert an ISO‑8601 ModifyDate timestamp into a PDF's XMP metadata using Aspose.Pdf.F... |
| [bind-pdf-byte-array-to-pdfxmpmetadata](./bind-pdf-byte-array-to-pdfxmpmetadata.cs) | Bind PDF from Byte Array to PdfXmpMetadata and Retrieve XMP | `Document`, `PdfXmpMetadata`, `Save` | Demonstrates how to create a PDF in memory, load it from a byte array, bind it to the PdfXmpMetad... |
| [check-pdf-xmp-metadata](./check-pdf-xmp-metadata.cs) | Check for XMP Metadata in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to determine whether a PDF file contains XMP metadata using Aspose.Pdf.Facades before a... |
| [clear-xmp-metadata-from-pdf](./clear-xmp-metadata-from-pdf.cs) | Clear XMP Metadata from PDF while Preserving Header | `PdfXmpMetadata`, `BindPdf`, `Clear` | Demonstrates how to remove all XMP metadata entries from a PDF using Aspose.Pdf.Facades, leaving ... |
| [copy-xmp-metadata-and-merge-pdfs](./copy-xmp-metadata-and-merge-pdfs.cs) | Copy XMP Metadata to PDF and Merge Pages | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a source PDF using PdfXmpMetadata, apply it to a target PD... |
| [create-pdf-with-xmp-metadata](./create-pdf-with-xmp-metadata.cs) | Create PDF with XMP Metadata | `Document`, `Add`, `RegisterNamespaceUri` | Demonstrates generating a new PDF, registering XMP namespaces, adding custom XMP metadata entries... |
| [decrypt-encrypted-pdf-with-error-handling](./decrypt-encrypted-pdf-with-error-handling.cs) | Decrypt Encrypted PDF with Error Handling | `PdfFileSecurity`, `BindPdf`, `DecryptFile` | Demonstrates binding a PDF using PdfFileSecurity, detecting encryption, providing a password to o... |
| [decrypt-update-creatortool-reencrypt-pdf](./decrypt-update-creatortool-reencrypt-pdf.cs) | Decrypt PDF, Update CreatorTool Metadata, and Re‑encrypt | `PdfFileSecurity`, `DecryptFile`, `EncryptFile` | Demonstrates how to decrypt a password‑protected PDF, modify its Creator (or CreatorTool) metadat... |
| [export-pdf-xmp-metadata-to-json](./export-pdf-xmp-metadata-to-json.cs) | Export PDF XMP Metadata to JSON | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to extract XMP metadata from a PDF using Aspose.Pdf and convert it to a JSON fil... |
| [export-xmp-metadata-to-sidecar](./export-xmp-metadata-to-sidecar.cs) | Export XMP Metadata from PDF to Side‑car File | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF file, retrieve its XMP metadata as a byte array, and write it to a... |
| [extract-xmp-metadata-from-pdf](./extract-xmp-metadata-from-pdf.cs) | Extract XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF file to a PdfXmpMetadata facade, retrieve the XMP metadata as an XML byte... |
| [extract-xmp-metadata-from-pdf__v2](./extract-xmp-metadata-from-pdf__v2.cs) | Extract XMP Metadata from PDF using Aspose.Pdf | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to bind a PDF, retrieve its XMP metadata as XML, and read common properties such... |
| [extract-xmp-metadata-to-dictionary](./extract-xmp-metadata-to-dictionary.cs) | Extract XMP Metadata from PDF to Dictionary | `PdfXmpMetadata`, `BindPdf`, `Keys` | Shows how to read XMP metadata from a PDF file using Aspose.Pdf and return it as a case‑insensiti... |
| [html-to-pdf-disable-baseurl-in-test](./html-to-pdf-disable-baseurl-in-test.cs) | HTML to PDF Conversion with Configurable BaseUrl Injection | `Document`, `HtmlLoadOptions`, `Save` | Converts an HTML file to PDF using Aspose.Pdf and demonstrates how to disable BaseUrl injection v... |
| [insert-pages-update-xmp-metadata](./insert-pages-update-xmp-metadata.cs) | Insert Pages and Update XMP Metadata in PDF | `PdfFileEditor`, `Insert`, `PdfXmpMetadata` | Demonstrates how to insert pages from one PDF into another and then add or replace XMP metadata b... |
| [list-xmp-namespaces-in-pdf](./list-xmp-namespaces-in-pdf.cs) | List XMP Namespaces in a PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract XMP metadata from a PDF with Aspose.Pdf.Facades and enumerate all namespace ... |
| [log-original-xmp-metadata](./log-original-xmp-metadata.cs) | Log Original XMP Metadata from PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates how to extract the original XMP metadata from a PDF using Aspose.Pdf and save it to ... |
| [log-xmp-metadata-size-before-after-modification](./log-xmp-metadata-size-before-after-modification.cs) | Log XMP Metadata Size Before and After Modification | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF to the XMP metadata facade, read the XMP block size, add a custom propert... |
| [modify-save-xmp-metadata-pdf](./modify-save-xmp-metadata-pdf.cs) | Modify and Save XMP Metadata in PDF | `PdfXmpMetadata`, `BindPdf`, `Add` | Demonstrates binding a PDF with PdfXmpMetadata, adding standard and custom XMP properties, regist... |
| [modify-xmp-metadata-property](./modify-xmp-metadata-property.cs) | Modify XMP Metadata Property in PDF | `Document`, `PdfXmpMetadata`, `BindPdf` | Demonstrates extracting XMP metadata from a PDF, updating a specific XML node (e.g., dc:creator),... |
| [preserve-xmp-metadata-when-splitting-pdf](./preserve-xmp-metadata-when-splitting-pdf.cs) | Preserve XMP Metadata When Splitting PDF into Single Pages | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to extract the original XMP metadata from a multi‑page PDF and re‑apply it to each sing... |
| [read-xmp-metadata-after-high-compression](./read-xmp-metadata-after-high-compression.cs) | Read XMP Metadata from a Highly Compressed PDF | `Document`, `All`, `OptimizeResources` | The example loads a PDF, applies high compression using OptimizationOptions, saves the compressed... |
| [read-xmp-metadata-from-large-pdf](./read-xmp-metadata-from-large-pdf.cs) | Read XMP Metadata from Large PDF and Measure Performance | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates loading a PDF with the PdfXmpMetadata facade, extracting its XMP metadata as a byte ... |
| [read-xmp-metadata-from-pdf-unc](./read-xmp-metadata-from-pdf-unc.cs) | Read XMP Metadata from PDF via UNC Path | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Demonstrates how to load a PDF located on a network share using a UNC path and extract its XMP me... |
| [refresh-creatortool-metadata-nightly](./refresh-creatortool-metadata-nightly.cs) | Refresh CreatorTool Metadata for PDFs Nightly | `PdfFileInfo`, `Creator`, `ModDate` | Shows how to scan a folder of PDF files and update the Creator (CreatorTool) metadata and modific... |
| [remove-nickname-from-xmp-metadata](./remove-nickname-from-xmp-metadata.cs) | Remove Nickname Element from PDF XMP Metadata | `PdfXmpMetadata`, `BindPdf`, `Remove` | Shows how to bind a PDF to the PdfXmpMetadata facade, delete the Nickname element from its XMP me... |
| [remove-xmp-metadata-from-pdf](./remove-xmp-metadata-from-pdf.cs) | Remove XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `Clear` | Shows how to delete the entire XMP metadata block from a PDF using Aspose.Pdf.Facades and save a ... |
| [replace-pdf-xmp-metadata](./replace-pdf-xmp-metadata.cs) | Replace PDF XMP Metadata from External File | `Document`, `SetXmpMetadata`, `Save` | Shows how to load a PDF, read an external .xmp file, replace the PDF's existing XMP metadata bloc... |
| [retrieve-xmp-metadata-from-pdf](./retrieve-xmp-metadata-from-pdf.cs) | Retrieve XMP Metadata from PDF | `PdfXmpMetadata`, `BindPdf`, `GetXmpMetadata` | Shows how to bind a PDF with PdfXmpMetadata and extract its raw XMP XML as a UTF-8 string. |
| ... | | | *and 13 more files* |

## Category Statistics
- Total examples: 43

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-xmp-metadata patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-05-08 | Run: `20260508_152439_700cf4`
<!-- AUTOGENERATED:END -->
