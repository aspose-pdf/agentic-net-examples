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
        const string outputPath = "page24.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Register Tahoma as a fallback for any missing font.
        // The substitution maps any missing font name ("*") to the Tahoma font file.
        string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        string tahomaPath = Path.Combine(fontsFolder, "tahoma.ttf");
        if (File.Exists(tahomaPath))
        {
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", tahomaPath));
        }
        else
        {
            Console.Error.WriteLine($"Tahoma font file not found at {tahomaPath}. Rendering will use original fonts.");
        }

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Verify the document has at least 24 pages (1‑based indexing)
            if (pdfDoc.Pages.Count < 24)
            {
                Console.Error.WriteLine("The document contains fewer than 24 pages.");
                return;
            }

            // Retrieve the 24th page
            Page page = pdfDoc.Pages[24];

            // Create a PNG device with 300 DPI resolution (devices are not IDisposable)
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true
            };

            // Render the page to a PNG file
            pngDevice.Process(page, outputPath);
        }

        Console.WriteLine($"Page 24 rendered to PNG at '{outputPath}'.");
    }
}
