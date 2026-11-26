using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPngPath = "page28.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Register Impact font via substitution (AddFont was removed in newer versions)
        string fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        string impactFontPath = Path.Combine(fontsFolder, "impact.ttf");
        if (File.Exists(impactFontPath))
        {
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Impact", impactFontPath));
        }
        else
        {
            Console.WriteLine("Impact font not found; rendering will use fallback fonts.");
        }

        using (Document pdfDoc = new Document(inputPdfPath))
        {
            if (pdfDoc.Pages.Count < 28)
            {
                Console.Error.WriteLine($"Document contains only {pdfDoc.Pages.Count} pages. Page 28 is unavailable.");
                return;
            }

            Page page = pdfDoc.Pages[28];

            // Create a PNG device with 300 DPI. Devices are not IDisposable, so no using block.
            var pngDevice = new PngDevice(new Resolution(300))
            {
                TransparentBackground = true // optional, keeps transparency if present
            };

            // Render the selected page to a PNG file.
            pngDevice.Process(page, outputPngPath);
        }

        Console.WriteLine($"Page 28 rendered to PNG at 300 DPI: {outputPngPath}");
    }
}
