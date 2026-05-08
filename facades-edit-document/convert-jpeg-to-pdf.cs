using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "Photo.jpg";
        const string outputPath = "Photo.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a new PDF document.
            using (var doc = new Document())
            {
                // Add a single page (default size and margins).
                var page = doc.Pages.Add();

                // Load the JPEG image and add it to the page.
                using (var imgStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    var image = new Aspose.Pdf.Image
                    {
                        ImageStream = imgStream
                    };
                    // The image will be placed at the origin (0,0). Aspose.Pdf will scale it
                    // to fit the page while respecting the default margins.
                    page.Paragraphs.Add(image);
                }

                // Save the document as a PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF created successfully at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
