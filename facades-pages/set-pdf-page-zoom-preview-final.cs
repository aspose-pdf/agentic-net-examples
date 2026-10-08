using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string previewDir = "PreviewImages";
        const string finalDir   = "FinalImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directories exist
        Directory.CreateDirectory(previewDir);
        Directory.CreateDirectory(finalDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // ---------- Preview rendering (zoom = 0.5) ----------
            // Zoom of 0.5 can be simulated by using a lower DPI resolution.
            // The default rendering DPI for Aspose.Pdf is 144. Half of that is 72.
            var previewResolution = new Resolution(72);
            var previewDevice     = new PngDevice(previewResolution);

            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                string previewPath = Path.Combine(previewDir, $"Page_{pageNum}_preview.png");
                previewDevice.Process(doc.Pages[pageNum], previewPath);
            }

            // ---------- Final rendering (zoom = 1.0) ----------
            // Full‑size rendering uses the default DPI (144). We can set it explicitly.
            var finalResolution = new Resolution(144);
            var finalDevice     = new PngDevice(finalResolution);

            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                string finalPath = Path.Combine(finalDir, $"Page_{pageNum}_final.png");
                finalDevice.Process(doc.Pages[pageNum], finalPath);
            }
        }

        Console.WriteLine("Preview and final images generated successfully.");
    }
}
