using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";
        const int    pageNumber = 1;          // page to rotate (1‑based)
        const int    rotation   = 90;         // rotation angle in degrees (90, 180, 270)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Rotate the page using the Document API (PdfPageEditor no longer provides RotatePage)
        Document pdf = new Document(inputPath);
        // Map integer rotation to the Rotation enum
        Rotation aspRotation = rotation switch
        {
            90  => Rotation.on90,
            180 => Rotation.on180,
            270 => Rotation.on270,
            _   => Rotation.None
        };
        pdf.Pages[pageNumber].Rotate = aspRotation;
        pdf.Save(outputPath);

        // Load the rotated PDF and retrieve the effective page size
        using (Document doc = new Document(outputPath))
        {
            // Aspose.Pdf uses 1‑based indexing for pages
            Page page = doc.Pages[pageNumber];

            // Original media box dimensions (points)
            double width  = page.PageInfo.Width;
            double height = page.PageInfo.Height;

            // Rotation stored in the page dictionary (0, 90, 180, 270)
            int pageRotation = (int)page.Rotate;

            // Effective dimensions after rotation: swap width/height for 90° or 270°
            double effectiveWidth  = width;
            double effectiveHeight = height;
            if (pageRotation == 90 || pageRotation == 270)
            {
                effectiveWidth  = height;
                effectiveHeight = width;
            }

            Console.WriteLine($"Page {pageNumber} rotation: {pageRotation}°");
            Console.WriteLine($"MediaBox size   : {width} x {height}");
            Console.WriteLine($"Effective size  : {effectiveWidth} x {effectiveHeight}");
        }
    }
}
