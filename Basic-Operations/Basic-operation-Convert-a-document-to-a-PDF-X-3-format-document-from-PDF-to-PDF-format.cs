using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF document
            Document sourceDoc = new Document(inputPath);

            // Set conversion options to PDF/X-3 format
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3);

            // Perform the conversion; Convert returns a bool indicating success
            bool conversionSucceeded = sourceDoc.Convert(conversionOptions);
            if (!conversionSucceeded)
            {
                Console.Error.WriteLine("Conversion failed: Aspose.Pdf returned false.");
                return;
            }

            // Save the converted document to the desired output file
            sourceDoc.Save(outputPath);

            Console.WriteLine($"Successfully converted '{inputPath}' to PDF/X-3 format as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
