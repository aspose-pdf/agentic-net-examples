using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;      // Resolution struct
using Aspose.Pdf.Text;         // SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Register a generic font substitution (any missing font -> Arial)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        // PdfConverter handles PDF‑to‑image conversion
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF file to the converter
            converter.BindPdf(inputPdf);

            // Set desired resolution using the Resolution struct (DPI)
            converter.Resolution = new Resolution(150);

            // Prepare the converter for processing
            converter.DoConvert();

            // Iterate through all pages (1‑based indexing)
            for (int page = 1; page <= converter.PageCount; page++)
            {
                string outPath = Path.Combine(outputDir, $"page_{page}.png");

                // Save the current page as a PNG image.
                // The overload without ImageFormat infers the format from the file extension.
                converter.GetNextImage(outPath);
            }

            // Release resources held by the converter
            converter.Close();
        }

        Console.WriteLine("PDF successfully converted to PNG images.");
    }
}
