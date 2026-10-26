using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string newPngPath    = "highres.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(newPngPath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {newPngPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three
            Page page = doc.Pages[3];

            // Remove the first image on the page (assumed to be the JPEG to replace)
            // XImageCollection uses 1‑based indexing, like other Aspose collections
            if (page.Resources.Images.Count > 0)
            {
                page.Resources.Images.Delete(1);
            }

            // Add the higher‑resolution PNG to the page's image resources
            using (FileStream pngStream = File.OpenRead(newPngPath))
            {
                page.Resources.Images.Add(pngStream);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replaced and saved to '{outputPdfPath}'.");
    }
}