using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string logoPath  = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create an ImageStamp for the PNG logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Set the position (lower‑left corner) and size of the logo
                // Coordinates are in points (1/72 inch). Adjust as needed.
                XIndent = 50,   // distance from the left edge
                YIndent = 750,  // distance from the bottom edge
                Width   = 100,  // logo width
                Height  = 50    // logo height
            };

            // Add the stamp to the first page only
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdf}'.");
    }
}