using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing for the Pages collection
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputDir, $"page_{i}.jpg");

                // Convert each page to JPEG using JpegDevice (default quality)
                using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
                {
                    JpegDevice jpegDevice = new JpegDevice(); // default quality settings
                    jpegDevice.Process(doc.Pages[i], imageStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to JPEG images.");
    }
}
