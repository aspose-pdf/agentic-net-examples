using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document once using the cross‑platform Document class
        using (Document pdfDocument = new Document(inputPdf))
        {
            int pageCount = pdfDocument.Pages.Count;

            // Convert each page to a separate TIFF file
            for (int i = 1; i <= pageCount; i++)
            {
                using (PdfConverter converter = new PdfConverter(pdfDocument))
                {
                    // Set the page range to a single page
                    converter.StartPage = i;
                    converter.EndPage   = i;

                    // Optional: set resolution (default is 96 DPI)
                    // converter.Resolution = new Resolution(150);

                    // Prepare conversion and write the TIFF file
                    converter.DoConvert();
                    string outPath = Path.Combine(outputDir, $"Page_{i}.tiff");
                    converter.SaveAsTIFF(outPath);
                    converter.Close();
                }
            }

            Console.WriteLine($"Converted {pageCount} pages to TIFF images in '{outputDir}'.");
        }
    }
}
