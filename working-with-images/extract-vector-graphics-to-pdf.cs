using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string destPdfPath   = "destination.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF (vector graphics will be extracted from its first page)
        using (Document srcDoc = new Document(sourcePdfPath))
        // Create a new empty PDF that will receive the extracted graphics
        using (Document destDoc = new Document())
        {
            // Ensure the destination has at least one page
            Page destPage = destDoc.Pages.Add();

            // Absorb vector graphics from the source page using the correct absorber class
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(srcDoc.Pages[1]); // 1‑based indexing

            // Temporary folder for SVG files
            string tempDir = Path.Combine(Path.GetTempPath(), "AsposeVectorExtract");
            Directory.CreateDirectory(tempDir);

            int index = 0;
            foreach (var element in absorber.Elements)
            {
                // Export each graphic element to an SVG file
                string svgPath = Path.Combine(tempDir, $"graphic_{index}.svg");
                element.SaveToSvg(svgPath);

                // Load the SVG as an Image and insert it into the Paragraphs collection
                Aspose.Pdf.Image svgImage = new Aspose.Pdf.Image();
                svgImage.File = svgPath;               // set the SVG file
                svgImage.FixWidth = 200;                // optional sizing
                svgImage.FixHeight = 200;
                destPage.Paragraphs.Add(svgImage);      // insert into Paragraphs

                index++;
            }

            // Save the destination PDF
            destDoc.Save(destPdfPath);
            Console.WriteLine($"Extracted vector graphics saved to '{destPdfPath}'.");
        }
    }
}
