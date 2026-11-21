using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF to know how many pages it contains
        Document doc = new Document(pdfPath);
        int totalPages = doc.Pages.Count;

        // Process each page individually so we can embed the page number in the file name
        for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the same PDF for every page iteration
                extractor.BindPdf(pdfPath);

                // Restrict extraction to the current page only
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Optional – extract images defined in resources as well
                // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

                // Perform the image extraction for the selected page
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build the required file name pattern
                    string fileName = $"Image_Page{pageNumber}_Index{imageIndex}.png";
                    string outPath  = Path.Combine(outputDir, fileName);

                    // GetNextImage requires an output path and an ImageFormat
                    extractor.GetNextImage(outPath, ImageFormat.Png);

                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Image extraction completed. Files saved to '{outputDir}'.");
    }
}
