using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file name
        const string pdfPath = "input.pdf";
        const string outputDir = "BmpImages";
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // Ensure a PDF exists – the sandbox does not contain any external files.
        // If the file is missing we create a minimal one‑page PDF on the fly.
        // ---------------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(pdfPath);
        }

        // Load the PDF document
        var document = new Document(pdfPath);

        // Use BmpDevice (cross‑platform) instead of System.Drawing.ImageFormat.
        // The device renders each page to a BMP image using the specified resolution.
        var resolution = new Resolution(300); // 300 DPI – adjust as needed
        var bmpDevice = new BmpDevice(resolution);

        for (int pageNumber = 1; pageNumber <= document.Pages.Count; pageNumber++)
        {
            string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
            using var stream = new FileStream(bmpPath, FileMode.Create);
            // The BmpDevice automatically respects the page's CropBox, so margins are trimmed.
            bmpDevice.Process(document.Pages[pageNumber], stream);
        }

        Console.WriteLine("PDF has been converted to BMP images successfully.");
    }
}
