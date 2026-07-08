using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document and convert each page to PNG at 300 DPI
        using (Document doc = new Document(inputPath))
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(doc);

            // Set the desired resolution (300 DPI). The property expects a Resolution object.
            converter.Resolution = new Resolution(300);

            // Initialise the conversion process
            converter.DoConvert();

            int pageNumber = 1;
            // Iterate through all pages and save each as a PNG image
            while (converter.HasNextImage())
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                // The overload infers the image format from the file extension, avoiding System.Drawing.
                converter.GetNextImage(outputPath);
                pageNumber++;
            }
        }

        Console.WriteLine("PDF has been successfully converted to PNG images.");
    }
}
