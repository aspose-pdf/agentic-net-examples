---
name: working-with-images
description: C# examples for working-with-images using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-images

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-images** category.
This folder contains standalone C# examples for working-with-images operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-images**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (69/72 files) ← category-specific
- `using Aspose.Pdf.Facades;` (17/72 files)
- `using Aspose.Pdf.Devices;` (10/72 files)
- `using Aspose.Pdf.Vector;` (5/72 files)
- `using Aspose.Pdf.Text;` (3/72 files)
- `using Aspose.Pdf.Annotations;` (2/72 files)
- `using Aspose.Pdf.Drawing;` (2/72 files)
- `using Aspose.Pdf.Optimization;` (2/72 files)
- `using Aspose.Pdf.Tagged;` (1/72 files)
- `using System;` (72/72 files)
- `using System.IO;` (71/72 files)
- `using System.Collections.Generic;` (7/72 files)
- `using System.Drawing;` (7/72 files)
- `using System.Drawing.Imaging;` (6/72 files)
- `using System.Text.Json;` (3/72 files)
- `using System.Linq;` (1/72 files)
- `using System.Security.Cryptography;` (1/72 files)

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
| [add-a-background-image-to-a-pdf-and-set-its-blend-...](./add-a-background-image-to-a-pdf-and-set-its-blend-mode-to-multiply-for-subtle-shading-effect.cs) | Add A Background Image To A Pdf And Set Its Blend Mode To Mu... | `PdfFileStamp` | Add A Background Image To A Pdf And Set Its Blend Mode To Multiply For Subtle Shading Effect |
| [add-a-background-image-to-each-page-and-set-its-bl...](./add-a-background-image-to-each-page-and-set-its-blend-mode-to-overlay-for-subtle-texture-effect.cs) | Add A Background Image To Each Page And Set Its Blend Mode T... |  | Add A Background Image To Each Page And Set Its Blend Mode To Overlay For Subtle Texture Effect |
| [add-a-background-image-to-every-page-of-a-pdf-and-...](./add-a-background-image-to-every-page-of-a-pdf-and-set-its-opacity-to-30-percent.cs) | Add A Background Image To Every Page Of A Pdf And Set Its Op... | `ImageStamp` | Add A Background Image To Every Page Of A Pdf And Set Its Opacity To 30 Percent |
| [add-a-background-pattern-image-to-each-page-and-se...](./add-a-background-pattern-image-to-each-page-and-set-its-opacity-to-10-percent-for-subtle-effect.cs) | Add A Background Pattern Image To Each Page And Set Its Opac... |  | Add A Background Pattern Image To Each Page And Set Its Opacity To 10 Percent For Subtle Effect |
| [add-a-background-pattern-image-to-each-page-and-se...](./add-a-background-pattern-image-to-each-page-and-set-its-opacity-to-5-percent-for-a-subtle-effect.cs) | Add A Background Pattern Image To Each Page And Set Its Opac... |  | Add A Background Pattern Image To Each Page And Set Its Opacity To 5 Percent For A Subtle Effect |
| [add-a-collection-of-extracted-vector-graphics-to-a...](./add-a-collection-of-extracted-vector-graphics-to-a-new-pdf-page-using-the-addrange-method.cs) | Add A Collection Of Extracted Vector Graphics To A New Pdf P... | `Rectangle`, `Line`, `Ellipse` | Add A Collection Of Extracted Vector Graphics To A New Pdf Page Using The Addrange Method |
| [add-a-company-logo-to-the-first-page-only-position...](./add-a-company-logo-to-the-first-page-only-positioning-it-at-the-center-of-the-page.cs) | Add A Company Logo To The First Page Only Positioning It At ... | `ImageStamp` | Add A Company Logo To The First Page Only Positioning It At The Center Of The Page |
| [add-a-decorative-footer-image-to-each-page-and-ens...](./add-a-decorative-footer-image-to-each-page-and-ensure-it-scales-proportionally-with-page-width.cs) | Add A Decorative Footer Image To Each Page And Ensure It Sca... | `Rectangle` | Add A Decorative Footer Image To Each Page And Ensure It Scales Proportionally With Page Width |
| [add-a-decorative-header-image-to-the-top-of-each-p...](./add-a-decorative-header-image-to-the-top-of-each-page-ensuring-it-does-not-overlap-existing-content.cs) | Add A Decorative Header Image To The Top Of Each Page Ensuri... | `Rectangle` | Add A Decorative Header Image To The Top Of Each Page Ensuring It Does Not Overlap Existing Content |
| [add-a-dicom-medical-image-to-a-pdf-page-using-a-fi...](./add-a-dicom-medical-image-to-a-pdf-page-using-a-filestream-and-the-image-constructor.cs) | Add A Dicom Medical Image To A Pdf Page Using A Filestream A... |  | Add A Dicom Medical Image To A Pdf Page Using A Filestream And The Image Constructor |
| [add-a-raster-image-to-a-new-pdf-page-using-the-ima...](./add-a-raster-image-to-a-new-pdf-page-using-the-image-class-and-paragraphs-collection.cs) | Add A Raster Image To A New Pdf Page Using The Image Class A... |  | Add A Raster Image To A New Pdf Page Using The Image Class And Paragraphs Collection |
| [add-a-semi-transparent-overlay-image-to-the-entire...](./add-a-semi-transparent-overlay-image-to-the-entire-pdf-to-create-a-unified-visual-theme.cs) | Add A Semi Transparent Overlay Image To The Entire Pdf To Cr... | `ImageStamp` | Add A Semi Transparent Overlay Image To The Entire Pdf To Create A Unified Visual Theme |
| [add-a-semi-transparent-overlay-to-each-page-and-ad...](./add-a-semi-transparent-overlay-to-each-page-and-adjust-its-color-based-on-a-theme-configuration-file.cs) | Add A Semi Transparent Overlay To Each Page And Adjust Its C... |  | Add A Semi Transparent Overlay To Each Page And Adjust Its Color Based On A Theme Configuration File |
| [add-a-semi-transparent-watermark-image-to-each-pag...](./add-a-semi-transparent-watermark-image-to-each-page-and-set-its-opacity-based-on-a-configuration-setting.cs) | Add A Semi Transparent Watermark Image To Each Page And Set ... | `ImageStamp` | Add A Semi Transparent Watermark Image To Each Page And Set Its Opacity Based On A Configuration ... |
| [add-a-transparent-png-overlay-to-each-page-and-adj...](./add-a-transparent-png-overlay-to-each-page-and-adjust-its-z-order-to-appear-above-existing-content.cs) | Add A Transparent Png Overlay To Each Page And Adjust Its Z ... | `ImageStamp` | Add A Transparent Png Overlay To Each Page And Adjust Its Z Order To Appear Above Existing Content |
| [add-a-watermark-image-to-pdfs-and-set-its-rotation...](./add-a-watermark-image-to-pdfs-and-set-its-rotation-angle-to-45-degrees-for-diagonal-placement.cs) | Add A Watermark Image To Pdfs And Set Its Rotation Angle To ... | `ImageStamp` | Add A Watermark Image To Pdfs And Set Its Rotation Angle To 45 Degrees For Diagonal Placement |
| [add-an-extracted-vector-graphic-to-another-pdf-pag...](./add-an-extracted-vector-graphic-to-another-pdf-page-individually-by-inserting-it-into-the-paragraphs-collection.cs) | Add An Extracted Vector Graphic To Another Pdf Page Individu... | `GraphicsAbsorber` | Add An Extracted Vector Graphic To Another Pdf Page Individually By Inserting It Into The Paragra... |
| [assign-alt-text-to-a-newly-added-image-to-provide-...](./assign-alt-text-to-a-newly-added-image-to-provide-descriptive-information-for-assistive-technologies.cs) | Assign Alt Text To A Newly Added Image To Provide Descriptiv... |  | Assign Alt Text To A Newly Added Image To Provide Descriptive Information For Assistive Technologies |
| [batch-extract-vector-graphics-from-multiple-pdf-fi...](./batch-extract-vector-graphics-from-multiple-pdf-files-and-store-each-set-in-separate-folders-for-analysis.cs) | Batch Extract Vector Graphics From Multiple Pdf Files And St... |  | Batch Extract Vector Graphics From Multiple Pdf Files And Store Each Set In Separate Folders For ... |
| [batch-process-multiple-pdf-files-to-extract-embedd...](./batch-process-multiple-pdf-files-to-extract-embedded-images-into-a-designated-output-folder.cs) | Batch Process Multiple Pdf Files To Extract Embedded Images ... |  | Batch Process Multiple Pdf Files To Extract Embedded Images Into A Designated Output Folder |
| [convert-an-entire-pdf-document-to-a-multi-page-tif...](./convert-an-entire-pdf-document-to-a-multi-page-tiff-file-while-applying-a-default-font-for-rendering.cs) | Convert An Entire Pdf Document To A Multi Page Tiff File Whi... | `TiffDevice` | Convert An Entire Pdf Document To A Multi Page Tiff File While Applying A Default Font For Rendering |
| [delete-a-specific-raster-image-from-a-pdf-page-by-...](./delete-a-specific-raster-image-from-a-pdf-page-by-removing-its-reference-from-page-contents.cs) | Delete A Specific Raster Image From A Pdf Page By Removing I... |  | Delete A Specific Raster Image From A Pdf Page By Removing Its Reference From Page Contents |
| [delete-all-images-from-a-pdf-by-iterating-each-pag...](./delete-all-images-from-a-pdf-by-iterating-each-page-and-calling-removeat-on-the-images-collection.cs) | Delete All Images From A Pdf By Iterating Each Page And Call... |  | Delete All Images From A Pdf By Iterating Each Page And Calling Removeat On The Images Collection |
| [delete-images-from-a-pdf-based-on-their-dpi-being-...](./delete-images-from-a-pdf-based-on-their-dpi-being-lower-than-72-using-imageplacementabsorber-filters.cs) | Delete Images From A Pdf Based On Their Dpi Being Lower Than... | `ImagePlacementAbsorber` | Delete Images From A Pdf Based On Their Dpi Being Lower Than 72 Using Imageplacementabsorber Filters |
| [delete-images-from-a-pdf-whose-dpi-is-lower-than-7...](./delete-images-from-a-pdf-whose-dpi-is-lower-than-72-using-imageplacementabsorber-filters.cs) | Delete Images From A Pdf Whose Dpi Is Lower Than 72 Using Im... | `ImagePlacementAbsorber` | Delete Images From A Pdf Whose Dpi Is Lower Than 72 Using Imageplacementabsorber Filters |
| [export-pdf-pages-as-bmp-images-at-300-dpi-using-cu...](./export-pdf-pages-as-bmp-images-at-300-dpi-using-custom-rendering-options-for-high-quality.cs) | Export Pdf Pages As Bmp Images At 300 Dpi Using Custom Rende... | `BmpDevice` | Export Pdf Pages As Bmp Images At 300 Dpi Using Custom Rendering Options For High Quality |
| [extract-all-raster-images-from-a-pdf-document-and-...](./extract-all-raster-images-from-a-pdf-document-and-save-each-as-a-separate-png-file.cs) | Extract All Raster Images From A Pdf Document And Save Each ... | `PdfExtractor` | Extract All Raster Images From A Pdf Document And Save Each As A Separate Png File |
| [extract-raster-images-from-a-pdf-and-preserve-thei...](./extract-raster-images-from-a-pdf-and-preserve-their-original-formats-when-writing-to-disk.cs) | Extract Raster Images From A Pdf And Preserve Their Original... | `PdfExtractor` | Extract Raster Images From A Pdf And Preserve Their Original Formats When Writing To Disk |
| [generate-thumbnail-images-for-each-pdf-page-as-150...](./generate-thumbnail-images-for-each-pdf-page-as-150x200-pixel-png-files-using-saveformat.png.cs) | Generate Thumbnail Images For Each Pdf Page As 150X200 Pixel... | `ThumbnailDevice` | Generate Thumbnail Images For Each Pdf Page As 150X200 Pixel Png Files Using Saveformat.Png |
| [insert-a-dicom-image-with-custom-resolution-by-adj...](./insert-a-dicom-image-with-custom-resolution-by-adjusting-its-width-and-height-before-adding.cs) | Insert A Dicom Image With Custom Resolution By Adjusting Its... |  | Insert A Dicom Image With Custom Resolution By Adjusting Its Width And Height Before Adding |
| ... | | | *and 42 more files* |

## Category Statistics
- Total examples: 72

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Devices.BmpDevice`
- `Aspose.Pdf.Devices.ColorDepth`
- `Aspose.Pdf.Devices.CompressionType`
- `Aspose.Pdf.Devices.EmfDevice`
- `Aspose.Pdf.Devices.PngDevice`
- `Aspose.Pdf.Devices.Resolution`
- `Aspose.Pdf.Devices.ShapeType`
- `Aspose.Pdf.Devices.TiffDevice`
- `Aspose.Pdf.Devices.TiffSettings`
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Facades.PdfExtractor`
- `Aspose.Pdf.Facades.PdfProducer`
- `Aspose.Pdf.Image`
- `Aspose.Pdf.ImportFormat`
- `Aspose.Pdf.Optimization.ImageCompressionOptions`

### Rules
- Load a PDF document: Document {doc} = new Document("{input_pdf}");
- Iterate over pages using 1‑based index: for (int {page}=1; {page} <= {doc}.Pages.Count; {page}++) { ... }
- Create a Resolution object for desired DPI: Resolution {resolution} = new Resolution({int});
- Instantiate a PngDevice with the resolution: PngDevice pngDevice = new PngDevice({resolution});
- Render a page to an output stream: pngDevice.Process({doc}.Pages[{page}], {output_stream});

### Warnings
- PdfProducer resides in the Aspose.Pdf.Facades namespace and may be deprecated in newer library versions; ensure the correct version is referenced.
- A valid Aspose.PDF license is required for production use.
- Assumes Aspose.Pdf.Devices.EmfDevice and Resolution are the correct fully qualified types; if the library version changes the namespace may differ.
- The example manually closes the MemoryStream; using a 'using' statement is recommended to ensure proper disposal.
- The code reads the entire file into a byte array before creating the MemoryStream; for large images a direct stream copy may be more efficient.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-images patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_212044_4ffbd1`
<!-- AUTOGENERATED:END -->
