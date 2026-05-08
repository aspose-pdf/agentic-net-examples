using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string logoPng   = "logo.png";
        const string outputPdf = "output.pdf";

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Define the rectangle where the logo will be placed:
            // lower‑left (llx, lly) = (50, 750), upper‑right (urx, ury) = (150, 800)
            // Adjust these values to position the logo as needed.
            Aspose.Pdf.Rectangle logoRect = new Aspose.Pdf.Rectangle(50, 750, 150, 800);

            // Add the PNG logo to the specified rectangle on the first page.
            firstPage.AddImage(logoPng, logoRect);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdf}'.");
    }
}