using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "Thumbnails";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create a concrete GIF device. Width = 0 preserves aspect ratio, Height = 80 limits the image height.
            var gifDevice = new GifDevice(0, 80);

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                string outPath = Path.Combine(outputFolder, $"thumb_page_{i}.gif");

                // Convert the current page to a GIF and write it to a file stream
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    gifDevice.Process(pdfDoc.Pages[i], outStream);
                }
            }
        }

        Console.WriteLine("GIF thumbnails created successfully.");
    }
}
