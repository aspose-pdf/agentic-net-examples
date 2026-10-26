using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputDir = "ExportedImages";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the XML and convert it to a PDF document in memory
            using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
            {
                // Define the resolution for the exported images (e.g., 300 DPI)
                var resolution = new Resolution(300);
                // PngDevice does NOT implement IDisposable, so instantiate it directly
                var pngDevice = new PngDevice(resolution) { TransparentBackground = true };

                // Iterate over pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    string outPath = Path.Combine(outputDir, $"page_{i}.png");

                    // Render a single page to PNG. Only the stream needs a using block.
                    using (var outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        pngDevice.Process(pdfDoc.Pages[i], outStream);
                    }

                    Console.WriteLine($"Saved page {i} as image: {outPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
