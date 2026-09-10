using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPng   = "logo.png";

        // Verify that the source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPng))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPng}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            Page firstPage = doc.Pages[1];

            // Define the rectangle where the PNG will be placed.
            // Coordinates are (llx, lly, urx, ury) in points.
            // Adjust these values to the desired position.
            Aspose.Pdf.Rectangle logoRect = new Aspose.Pdf.Rectangle(50, 750, 150, 850);

            // Add the PNG image to the first page at the specified rectangle.
            // Using the overload that takes a file path is the simplest approach.
            firstPage.AddImage(logoPng, logoRect);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdf}'.");
    }
}