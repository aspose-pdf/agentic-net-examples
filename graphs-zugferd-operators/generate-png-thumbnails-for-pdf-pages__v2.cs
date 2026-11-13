using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "Thumbnails";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a PNG device – resolution is supplied via the constructor
            var pngDevice = new PngDevice(new Resolution(72));

            // Iterate pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{i}.png");

                // Convert page to PNG and write to file using the device's Process method
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(doc.Pages[i], imageStream);
                }

                Console.WriteLine($"Thumbnail saved: {outputPath}");
            }
        }
    }
}
