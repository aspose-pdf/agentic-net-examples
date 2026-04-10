using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF once to obtain the total page count
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;

            // Iterate over odd‑numbered pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber += 2)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Convert the current page to PNG using PdfConverter (Facades API)
                using (PdfConverter converter = new PdfConverter())
                {
                    converter.BindPdf(inputPdf);
                    converter.StartPage = pageNumber; // set start page
                    converter.EndPage   = pageNumber; // set end page (single page)
                    converter.DoConvert();            // initialize conversion
                    converter.GetNextImage(outputPath, ImageFormat.Png); // save as PNG
                }
            }
        }

        Console.WriteLine("Odd‑page PNG conversion completed.");
    }
}