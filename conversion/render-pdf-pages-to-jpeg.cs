using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "PagesImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JpegDevice does NOT implement IDisposable, so instantiate it directly
            JpegDevice jpegDevice = new JpegDevice(300);

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputDir, $"page_{i}.jpg");
                // Write each page image to a separate JPEG file
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(doc.Pages[i], outStream);
                }
            }
        }

        Console.WriteLine("All pages have been rendered to JPEG images.");
    }
}
