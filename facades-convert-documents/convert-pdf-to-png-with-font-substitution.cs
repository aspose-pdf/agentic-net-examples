using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices; // for Resolution

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

        Directory.CreateDirectory(outputDir);

        // Register font substitution: replace missing Helvetica with Times New Roman
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

        // Initialize the PdfConverter facade
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document
            converter.BindPdf(inputPdf);

            // Set desired resolution (default is 150 DPI)
            converter.Resolution = new Resolution(150);

            // Define the page range to convert (all pages)
            converter.StartPage = 1;
            converter.EndPage = converter.PageCount;

            int pageNumber = converter.StartPage;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                // Save the current page as a PNG image; format inferred from file extension
                converter.GetNextImage(outPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}
