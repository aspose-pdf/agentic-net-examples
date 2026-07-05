using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string logoPath   = "logo.png";           // company logo image
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Ensure a HeaderFooter object exists for the page
                if (page.Header == null)
                    page.Header = new HeaderFooter();

                // Create an Image object for the logo
                Image logo = new Image
                {
                    File = logoPath
                };

                // Optionally set a fixed size for the logo (adjust as needed)
                // logo.FixWidth = 100;   // width in points
                // logo.FixHeight = 50;   // height in points

                // Add the image to the header's paragraph collection.
                // The header is rendered left‑aligned by default.
                page.Header.Paragraphs.Add(logo);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header logo: {outputPath}");
    }
}