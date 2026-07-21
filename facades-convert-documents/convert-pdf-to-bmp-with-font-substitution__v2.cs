using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for font substitution

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output directory for BMP images
        const string outputDir = "BmpImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Register a global font substitution: if a font used in the PDF is missing, replace it with Arial.
        // This replaces the obsolete FontSubstitution event which attempted to set a read‑only property.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Arial"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded PDF document to the converter
                converter.BindPdf(pdfDoc);
                // Prepare the converter (loads internal structures)
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate over all pages and save each as a BMP image
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    // Save the current page as BMP
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF conversion to BMP completed.");
    }
}
