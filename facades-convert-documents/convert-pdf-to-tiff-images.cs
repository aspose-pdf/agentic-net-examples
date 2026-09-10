using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // added for TextFragment

class PdfToTiffConverter
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF file
        const string outputDir = "TiffPages"; // folder for TIFF pages

        // ------------------------------------------------------------
        // Ensure a sample PDF exists (self‑contained example)
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            using (var doc = new Document())
            {
                // Add a simple page with some text so the conversion has content
                var page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample page for TIFF conversion"));
                doc.Save(inputPdf);
            }
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        var pdfDocument = new Document(inputPdf);

        // Convert each page to an individual TIFF file using TiffDevice (cross‑platform)
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string tiffPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");
            // Resolution can be adjusted; 150 DPI is a reasonable default
            var tiffDevice = new TiffDevice(new Resolution(150)); // TiffDevice does not implement IDisposable, so no using
            tiffDevice.Process(pdfDocument.Pages[pageNumber], tiffPath);
        }

        Console.WriteLine("PDF has been converted to individual TIFF images.");
    }
}
