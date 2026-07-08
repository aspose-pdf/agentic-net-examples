using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string logoPngPath   = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoPngPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPngPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page firstPage = doc.Pages[1];

            // Define the rectangle where the logo will be placed.
            // Coordinates are (llx, lly, urx, ury) in points.
            // Adjust these values to position the logo as needed.
            Aspose.Pdf.Rectangle logoRect = new Aspose.Pdf.Rectangle(50, 750, 150, 850);

            // Add the PNG image to the specified rectangle on the first page
            firstPage.AddImage(logoPngPath, logoRect);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdfPath}'.");
    }
}