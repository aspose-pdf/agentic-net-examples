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
        const string inputPdf = "input.pdf";                 // Path to source PDF
        const string outputDir = "png_pages";                // Directory for PNG images

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Determine total page count using a Document (disposed via using)
        int pageCount;
        using (Document doc = new Document(inputPdf))
        {
            pageCount = doc.Pages.Count;
        }

        // Parallel conversion: each page gets its own PdfConverter instance
        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        Parallel.For(1, pageCount + 1, options, pageNumber =>
        {
            try
            {
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the PDF file
                    converter.BindPdf(inputPdf);

                    // Restrict conversion to a single page
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;

                    // Prepare the converter
                    converter.DoConvert();

                    // Output file name for the current page
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                    // Save the page as PNG
                    converter.GetNextImage(outPath, ImageFormat.Png);
                }

                Console.WriteLine($"Page {pageNumber} converted to PNG.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting page {pageNumber}: {ex.Message}");
            }
        });

        Console.WriteLine("All pages have been processed.");
    }
}