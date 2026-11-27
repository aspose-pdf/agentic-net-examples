using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices; // for Resolution, BmpDevice

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        // PdfConverter also implements IDisposable, so wrap it as well
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded document to the converter
            converter.BindPdf(pdfDoc);

            // Add font substitution BEFORE setting the resolution
            // SimpleFontSubstitution replaces missing fonts with the specified alternative
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Helvetica"));

            // Set the desired resolution for rasterization (e.g., 300 DPI)
            converter.Resolution = new Resolution(300);

            // Define the page range to convert (all pages in this case)
            converter.StartPage = 1;
            converter.EndPage   = pdfDoc.Pages.Count;

            // Perform the conversion – this prepares the internal image buffers
            converter.DoConvert();

            // Use BmpDevice (from Aspose.Pdf.Devices) to save each page as BMP
            // BmpDevice works with the same resolution that was set on the converter
            var bmpDevice = new BmpDevice(new Resolution(300));
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.bmp");
                // Process takes a Page object and an output path (or stream)
                bmpDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
                Console.WriteLine($"Saved page {pageNumber} as BMP to '{outputPath}'.");
            }

            // Close the converter (optional, but good practice)
            converter.Close();
        }
    }
}
