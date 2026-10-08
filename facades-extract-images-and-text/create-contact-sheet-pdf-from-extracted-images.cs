using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// NOTE: Add the Aspose.Pdf NuGet package to the project (e.g., <PackageReference Include="Aspose.Pdf" Version="23.12" />)
// This resolves the missing "AsposePdfApi.csproj.nuget.g.targets" import error.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "contact_sheet.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // 1. Extract all images from the source PDF using ImagePlacementAbsorber
        // ---------------------------------------------------------------------
        List<MemoryStream> imageStreams = new List<MemoryStream>();
        Document srcDoc = new Document(inputPdfPath);
        ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
        srcDoc.Pages.Accept(absorber);
        foreach (var placement in absorber.ImagePlacements)
        {
            // The ImagePlacement provides an Image object. Save it to a memory stream.
            var img = placement.Image;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png); // PNG preserves quality and works for all image types
            ms.Position = 0;
            imageStreams.Add(ms);
        }

        if (imageStreams.Count == 0)
        {
            Console.WriteLine("No images found in the PDF.");
            return;
        }

        // ---------------------------------------------------------------
        // 2. Create a new PDF that will hold the contact‑sheet thumbnails
        // ---------------------------------------------------------------
        using (Document contactDoc = new Document())
        {
            // Add a single page (default size A4)
            Page page = contactDoc.Pages.Add();

            // Layout parameters
            const int columns = 3;                     // thumbnails per row
            const double thumbWidth = 150;             // width of each thumbnail
            const double thumbHeight = 150;            // height of each thumbnail
            const double spacing = 10;                 // space between thumbnails
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Calculate starting offsets to centre the grid horizontally
            double totalRowWidth = columns * thumbWidth + (columns - 1) * spacing;
            double startX = (pageWidth - totalRowWidth) / 2;
            double startY = pageHeight - spacing - thumbHeight; // start from top

            int currentColumn = 0;
            int currentRow = 0;

            foreach (MemoryStream imgStream in imageStreams)
            {
                // Use ImageStamp for absolute positioning on the page
                ImageStamp stamp = new ImageStamp(imgStream)
                {
                    Width = thumbWidth,
                    Height = thumbHeight,
                    XIndent = startX + currentColumn * (thumbWidth + spacing),
                    YIndent = startY - currentRow * (thumbHeight + spacing)
                };

                // Add the stamp to the page
                page.AddStamp(stamp);

                // Move to next cell in the grid
                currentColumn++;
                if (currentColumn >= columns)
                {
                    currentColumn = 0;
                    currentRow++;
                }
            }

            // Save the contact sheet PDF
            contactDoc.Save(outputPdfPath);
        }

        // Dispose all extracted image streams
        foreach (var ms in imageStreams)
        {
            ms.Dispose();
        }

        Console.WriteLine($"Contact sheet created: {outputPdfPath}");
    }
}
