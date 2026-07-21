using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat for BMP
using Aspose.Pdf;                           // Document, etc.
using Aspose.Pdf.Facades;                  // PdfConverter (Facade)
using Aspose.Pdf.Devices;                  // Resolution
using Aspose.Pdf.Text;                     // FontRepository & SimpleFontSubstitution

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory for BMP images
        const string outputDir = "BmpImages";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Apply custom font substitution: replace Times New Roman with Calibri
            // Use FontRepository.Substitutions with SimpleFontSubstitution (requires Aspose.Pdf.Text namespace)
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Calibri"));

            // Initialize PdfConverter with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Set desired resolution (e.g., 300 DPI) for higher quality BMPs
                converter.Resolution = new Resolution(300);

                // Prepare the converter for conversion
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through all pages and save each as a BMP image
                while (converter.HasNextImage())
                {
                    string bmpPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");
                    // GetNextImage saves the next page image to the specified file using the given format
                    converter.GetNextImage(bmpPath, ImageFormat.Bmp);
                    pageNumber++;
                }

                // Release resources held by the converter
                converter.Close();
            }
        }

        Console.WriteLine("PDF conversion to BMP completed successfully.");
    }
}
