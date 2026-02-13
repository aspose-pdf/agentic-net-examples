using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF file and desired output file (extension determines format)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Convert <input.pdf> <output.ext>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (load rule)
            Document pdfDocument = new Document(inputPath);

            // Choose conversion method based on the target extension
            string ext = Path.GetExtension(outputPath).ToLowerInvariant();

            if (ext == ".tiff" || ext == ".tif")
            {
                // Convert PDF pages to a multi‑page TIFF using PdfConverter (Facades API)
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the PDF to the converter
                    converter.BindPdf(pdfDocument);
                    // Prepare conversion
                    converter.DoConvert();
                    // Save the result as TIFF
                    converter.SaveAsTIFF(outputPath);
                }
            }
            else
            {
                // For other formats (e.g., .docx, .html, .svg) rely on Document.Save.
                // The format is inferred from the file extension.
                // Simple save without options (document-save rule)
                pdfDocument.Save(outputPath);
            }

            Console.WriteLine($"Conversion succeeded: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}