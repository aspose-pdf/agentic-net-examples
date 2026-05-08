using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution
using Aspose.Pdf.Text;   // for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "BmpImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Apply custom font substitution: Times New Roman → Calibri
            // Use FontRepository.Substitutions with SimpleFontSubstitution
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Calibri"));

            // Initialize PdfConverter (facade for PDF‑to‑image conversion)
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(pdfDoc);

                // Convert all pages (page indexing is 1‑based)
                converter.StartPage = 1;
                converter.EndPage   = pdfDoc.Pages.Count;

                // Set desired resolution (dots per inch) using Resolution object
                converter.Resolution = new Resolution(300);

                // Prepare converter for processing
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through pages and save each as BMP
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    // Save current page image in BMP format
                    converter.GetNextImage(outPath, ImageFormat.Bmp);
                    pageNumber++;
                }

                // Release internal resources
                converter.Close();
            }
        }

        Console.WriteLine("PDF successfully converted to BMP images.");
    }
}
