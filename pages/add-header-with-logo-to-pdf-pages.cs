using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";
        const string logoPath   = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPath))
        {
            // Add a header with the logo to each page
            foreach (Page page in doc.Pages)
            {
                // Create a new header (HeaderFooter) for the page
                page.Header = new HeaderFooter();

                // Create an Image object and set its source file (rule: use parameterless ctor then set File)
                Image logo = new Image();
                logo.File = logoPath;

                // Add the image to the header's paragraph collection.
                // By default, content in the header is left‑aligned.
                page.Header.Paragraphs.Add(logo);
            }

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header to '{outputPath}'.");
    }
}