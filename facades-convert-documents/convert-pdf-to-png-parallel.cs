using System;
using System.IO;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfToPngParallel
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Determine the number of pages in the PDF (using a Document inside a using block)
        int pageCount;
        using (Document doc = new Document(inputPdf))
        {
            pageCount = doc.Pages.Count; // 1‑based page count
        }

        // Process each page in parallel
        Parallel.For(1, pageCount + 1, pageNumber =>
        {
            // Each thread works with its own PdfConverter instance
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Restrict conversion to a single page
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Prepare the converter (required before GetNextImage)
                converter.DoConvert();

                // Build the output file name
                string outPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Save the page as PNG
                converter.GetNextImage(outPath, ImageFormat.Png);
            }
        });

        Console.WriteLine($"Conversion completed. PNG files are saved in '{outputFolder}'.");
    }
}