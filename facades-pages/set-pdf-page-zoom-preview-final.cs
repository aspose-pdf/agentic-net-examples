using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution, PngDevice

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string previewImage = "preview_page1.png";
        const string finalPdfPath = "final_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrap in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---------- Preview with 0.5 zoom (half size) ----------
            // Use PngDevice (Aspose.Pdf.Devices) to render the first page as PNG.
            // A resolution of 72 DPI (half of the default 144 DPI) gives roughly 50 % scaling.
            var pngDevice = new PngDevice(new Resolution(72));
            using (FileStream outStream = new FileStream(previewImage, FileMode.Create, FileAccess.Write))
            {
                pngDevice.Process(doc.Pages[1], outStream);
            }

            // ---------- Final output with 1.0 zoom (original size) ----------
            // Apply an identity zoom (1.0) to all pages using PdfPageEditor.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                // Target every page (1‑based indexing).
                editor.ProcessPages = Enumerable.Range(1, doc.Pages.Count).ToArray();
                editor.Zoom = 1.0f; // 100 % magnification – no scaling.
                editor.Save(finalPdfPath);
            }
        }

        Console.WriteLine("Preview image and final PDF have been generated.");
    }
}
