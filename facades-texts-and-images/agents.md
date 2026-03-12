---
name: facades-texts-and-images
description: C# examples for facades-texts-and-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - facades-texts-and-images

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **facades-texts-and-images** category.
This folder contains standalone C# examples for facades-texts-and-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **facades-texts-and-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf.Facades;` (20/22 files) ← category-specific
- `using Aspose.Pdf;` (16/22 files) ← category-specific
- `using Aspose.Pdf.Devices;` (5/22 files)
- `using Aspose.Pdf.Text;` (1/22 files)
- `using System;` (22/22 files)
- `using System.IO;` (22/22 files)
- `using System.Drawing.Imaging;` (4/22 files)
- `using System.Text;` (2/22 files)
- `using System.Runtime.InteropServices;` (1/22 files)

## Common Code Pattern

Most files in this category use `PdfConverter` from `Aspose.Pdf.Facades`:

```csharp
PdfConverter tool = new PdfConverter();
tool.BindPdf("input.pdf");
// ... PdfConverter operations ...
tool.Save("output.pdf");
```

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [configure-image-handling-options-for-a-loaded-pdf-document-u...](./configure-image-handling-options-for-a-loaded-pdf-document-using-the-facade-pattern-api.cs) | `PdfFileMend` | Configure image handling options for a loaded pdf document using the facade p... |
| [convert-a-loaded-pdf-document-to-bmp-image-format-using-a-fa...](./convert-a-loaded-pdf-document-to-bmp-image-format-using-a-facade-abstraction-layer.cs) | `PdfConverter` | Convert a loaded pdf document to bmp image format using a facade abstraction ... |
| [convert-a-loaded-pdf-document-to-xps-format-using-a-facade-a...](./convert-a-loaded-pdf-document-to-xps-format-using-a-facade-abstraction-to-simplify-api-interactions.cs) |  | Convert a loaded pdf document to xps format using a facade abstraction to sim... |
| [convert-each-page-of-a-loaded-pdf-document-into-individual-e...](./convert-each-page-of-a-loaded-pdf-document-into-individual-emf-images-using-a-facade-abstraction.cs) | `EmfDevice` | Convert each page of a loaded pdf document into individual emf images using a... |
| [convert-each-page-of-a-loaded-pdf-document-into-separate-jpe...](./convert-each-page-of-a-loaded-pdf-document-into-separate-jpeg-images-using-facades.cs) | `PdfConverter` | Convert each page of a loaded pdf document into separate jpeg images using fa... |
| [employ-facade-components-to-apply-image-manipulation-operati...](./employ-facade-components-to-apply-image-manipulation-operations-on-a-pdf-document-after-loading-it.cs) | `PdfPageEditor` | Employ facade components to apply image manipulation operations on a pdf docu... |
| [employ-facades-to-extract-all-embedded-images-from-an-epub-d...](./employ-facades-to-extract-all-embedded-images-from-an-epub-document-loaded-as-epub.cs) | `EpubLoadOptions`, `PdfExtractor` | Employ facades to extract all embedded images from an epub document loaded as... |
| [implement-a-facade-to-render-and-output-a-loaded-markdown-.m...](./implement-a-facade-to-render-and-output-a-loaded-markdown-.md-document-to-the-console.cs) | `MdLoadOptions`, `TextAbsorber` | Implement a facade to render and output a loaded markdown .md document to the... |
| [implement-a-facade-to-transform-an-svg-input-stream-into-a-j...](./implement-a-facade-to-transform-an-svg-input-stream-into-a-jpeg-image-preserving-visual-fidelity.cs) | `SvgLoadOptions`, `PdfConverter` | Implement a facade to transform an svg input stream into a jpeg image preserv... |
| [use-a-fa-ade-pattern-to-transform-a-loaded-pdf-document-into...](./use-a-fa-ade-pattern-to-transform-a-loaded-pdf-document-into-docx-format-while-maintaining-content-f.cs) | `PdfConverter` | Use a fa ade pattern to transform a loaded pdf document into docx format whil... |
| [use-the-facade-pattern-to-insert-an-image-into-an-already-lo...](./use-the-facade-pattern-to-insert-an-image-into-an-already-loaded-pdf-document.cs) | `PdfFileMend` | Use the facade pattern to insert an image into an already loaded pdf document |
| [utilize-a-facade-to-efficiently-transform-a-loaded-pdf-docum...](./utilize-a-facade-to-efficiently-transform-a-loaded-pdf-document-into-plain-text-format.cs) | `PdfExtractor` | Utilize a facade to efficiently transform a loaded pdf document into plain te... |
| [utilize-a-facade-to-retrieve-all-embedded-images-from-a-load...](./utilize-a-facade-to-retrieve-all-embedded-images-from-a-loaded-pdf-document-while-preserving-origina.cs) | `PdfExtractor` | Utilize a facade to retrieve all embedded images from a loaded pdf document w... |
| [utilize-a-facade-to-retrieve-both-images-and-textual-content...](./utilize-a-facade-to-retrieve-both-images-and-textual-content-from-a-loaded-pdf-document.cs) | `PdfExtractor` | Utilize a facade to retrieve both images and textual content from a loaded pd... |
| [utilize-a-facade-to-send-a-loaded-pdf-document-to-the-printe...](./utilize-a-facade-to-send-a-loaded-pdf-document-to-the-printer-preserving-its-original-layout.cs) |  | Utilize a facade to send a loaded pdf document to the printer preserving its ... |
| [utilize-facade-abstractions-to-render-and-print-an-html-docu...](./utilize-facade-abstractions-to-render-and-print-an-html-document-that-has-been-loaded-as-html.cs) | `HtmlLoadOptions` | Utilize facade abstractions to render and print an html document that has bee... |
| [utilize-facade-components-to-extract-images-and-text-from-an...](./utilize-facade-components-to-extract-images-and-text-from-an-html-document-loaded-into-memory.cs) | `HtmlLoadOptions`, `PdfExtractor` | Utilize facade components to extract images and text from an html document lo... |
| [utilize-facades-to-transform-a-loaded-pdf-document-into-png-...](./utilize-facades-to-transform-a-loaded-pdf-document-into-png-images-while-maintaining-visual-fidelity.cs) | `PdfConverter` | Utilize facades to transform a loaded pdf document into png images while main... |
| [utilize-the-facade-api-to-insert-custom-text-into-a-loaded-p...](./utilize-the-facade-api-to-insert-custom-text-into-a-loaded-pdf-document-and-save-the-modifications.cs) | `PdfContentEditor` | Utilize the facade api to insert custom text into a loaded pdf document and s... |
| [utilize-the-facade-layer-to-transform-a-loaded-pdf-document-...](./utilize-the-facade-layer-to-transform-a-loaded-pdf-document-into-high-quality-jpeg-images.cs) | `PdfConverter` | Utilize the facade layer to transform a loaded pdf document into high quality... |
| [utilize-the-facade-pattern-to-transform-a-loaded-pdf-documen...](./utilize-the-facade-pattern-to-transform-a-loaded-pdf-document-into-a-bmp-image-file.cs) | `BmpDevice` | Utilize the facade pattern to transform a loaded pdf document into a bmp imag... |
| [utilize-the-facade-pattern-to-transform-a-loaded-pdf-documen...](./utilize-the-facade-pattern-to-transform-a-loaded-pdf-document-into-a-tiff-image-while-preserving-fid.cs) | `PdfConverter` | Utilize the facade pattern to transform a loaded pdf document into a tiff ima... |

## Category Statistics
- Total examples: 22

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.ExtractImageMode`
- `Aspose.Pdf.Facades.PdfContentEditor`
- `Aspose.Pdf.Facades.PdfContentEditor.BindPdf(string)`
- `Aspose.Pdf.Facades.PdfContentEditor.DeleteImage()`

### Rules
- Bind a PDF file to a PdfConverter with BindPdf({input_pdf}) before any conversion.
- Invoke DoConvert() on the PdfConverter to initialize the conversion process.
- Export the bound PDF to a TIFF image using SaveAsTIFF({output_tiff}) after DoConvert() has been called.
- Always release resources by calling Close() on the PdfConverter when finished.
- Bind the PDF document to a PdfExtractor instance using BindPdf({input_pdf}) before any extraction operation.

### Warnings
- PdfConverter belongs to the Aspose.Pdf.Facades namespace, which may be considered legacy in newer SDK versions; verify compatibility.
- The example uses System.Drawing.Imaging.ImageFormat for the output format, which may require additional NuGet packages (e.g., System.Drawing.Common) on non‑Windows platforms.
- GetNextImage overwrites files if the generated {output_image_path} collides; ensure unique filenames.
- GetNextImage writes the image data to the provided stream; the stream should be positioned appropriately before further use.
- The example writes images to files using DateTime.Now.Ticks for naming, which may cause naming collisions in rapid successive runs.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for facades-texts-and-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-12 | Run: `20260312_233752_850e17`
<!-- AUTOGENERATED:END -->
