using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // For FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page17.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Register Times New Roman as a fallback for any missing font.
        // The asterisk (*) matches all missing fonts.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Verify the document has at least 17 pages (1‑based indexing)
                if (doc.Pages.Count < 17)
                {
                    Console.Error.WriteLine("Document contains fewer than 17 pages.");
                    return;
                }

                // Create a PNG device with 200 DPI resolution
                PngDevice pngDevice = new PngDevice(new Resolution(200));

                // Render page 17 to PNG
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(doc.Pages[17], imageStream);
                }
            }

            Console.WriteLine($"Page 17 saved as PNG to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
