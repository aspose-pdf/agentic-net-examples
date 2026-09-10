using System;
using System.IO;
using Aspose.Pdf;                 // For InvalidPdfFileFormatException and PdfException
using Aspose.Pdf.Facades;        // For PdfExtractor

class Program
{
    static void Main()
    {
        const string inputPath = "corrupted.pdf";

        // Verify that the file exists before attempting to bind it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create a PdfExtractor instance.
            PdfExtractor extractor = new PdfExtractor();

            // Attempt to bind the PDF file.
            // This call throws InvalidPdfFileFormatException if the PDF is corrupted.
            extractor.BindPdf(inputPath);

            // If binding succeeds, further extraction operations can be performed here.
            Console.WriteLine("PDF bound successfully.");
        }
        // Specific exception for an invalid or corrupted PDF file.
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine("Invalid PDF file format:");
            Console.Error.WriteLine(ex.Message);
        }
        // General PDF processing errors (e.g., other format issues).
        catch (PdfException ex)
        {
            Console.Error.WriteLine("PDF processing error:");
            Console.Error.WriteLine(ex.Message);
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine("Inner exception: " + ex.InnerException.Message);
            }
        }
        // Fallback for any unexpected exceptions.
        catch (Exception ex)
        {
            Console.Error.WriteLine("Unexpected error:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}