using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page2.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Register a fallback to Times New Roman for any missing font
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));

            // Ensure the document has at least two pages
            if (pdfDoc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF does not contain a second page.");
                return;
            }

            // Aspose.Pdf uses 1‑based page indexing; get the second page
            Page page = pdfDoc.Pages[2];

            // Create a PNG device with 300 DPI resolution (devices are not IDisposable)
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = false
            };

            // Render the page to PNG
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pngDevice.Process(page, imageStream);
            }
        }

        Console.WriteLine($"Second page rendered to PNG at 300 DPI: {outputPath}");
    }
}
