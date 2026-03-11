using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class SvgToBmpConverter
{
    static void Main()
    {
        // Input PDF containing SVG graphics
        const string pdfPath = "input.pdf";

        // Directory where extracted SVGs and resulting BMPs will be saved
        const string outputDir = "Output";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter (a Facade) to work with pages
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(pdfPath);

            // Process all pages
            converter.StartPage = 1;
            converter.EndPage   = converter.Document.Pages.Count;

            for (int pageNum = converter.StartPage; pageNum <= converter.EndPage; pageNum++)
            {
                Page pdfPage = converter.Document.Pages[pageNum];

                // Attempt to extract vector graphics (SVG) from the page
                string svgPath = Path.Combine(outputDir, $"page_{pageNum}.svg");
                bool hasVectorGraphics = pdfPage.TrySaveVectorGraphics(svgPath);

                if (!hasVectorGraphics)
                {
                    Console.WriteLine($"Page {pageNum}: No SVG graphics found.");
                    continue;
                }

                // Load the extracted SVG as a PDF document (SVG is treated as a source format)
                using (Document svgDoc = new Document(svgPath, new SvgLoadOptions()))
                {
                    // Prepare BMP device with desired resolution (e.g., 300 DPI)
                    BmpDevice bmpDevice = new BmpDevice(new Resolution(300));

                    // Convert the first (and only) page of the SVG document to BMP
                    string bmpPath = Path.Combine(outputDir, $"page_{pageNum}.bmp");
                    using (FileStream bmpStream = new FileStream(bmpPath, FileMode.Create))
                    {
                        bmpDevice.Process(svgDoc.Pages[1], bmpStream);
                    }

                    Console.WriteLine($"Page {pageNum}: SVG extracted and saved as BMP -> {bmpPath}");
                }

                // Optionally delete the intermediate SVG file
                try { File.Delete(svgPath); } catch { /* ignore cleanup errors */ }
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("Conversion completed.");
    }
}
