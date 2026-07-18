using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and PNG logo paths
        const string inputPdf = "input.pdf";
        const string logoPng  = "logo.png";
        const string outputPdf = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPng))
        {
            Console.Error.WriteLine($"Logo PNG not found: {logoPng}");
            return;
        }

        // Load the PDF document (lifecycle: using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (page indexing is 1‑based)
            Page firstPage = doc.Pages[1];

            // Define the rectangle where the logo will be placed:
            // left = 50, bottom = 750, right = 150, top = 800 (example coordinates)
            Aspose.Pdf.Rectangle logoRect = new Aspose.Pdf.Rectangle(50, 750, 150, 800);

            // Add the PNG image to the first page at the specified rectangle
            firstPage.AddImage(logoPng, logoRect);

            // Save the modified PDF (saving PDF without options writes PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdf}'.");
    }
}