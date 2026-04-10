using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // contains exception types

class Program
{
    static void Main()
    {
        const string inputPath = "corrupted.pdf";

        // Verify that the file exists before attempting to bind it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create a PdfExtractor instance (uses the default constructor)
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF file. If the file is corrupted, an exception will be thrown.
            extractor.BindPdf(inputPath);

            // Binding succeeded – further extraction logic could be placed here.
            Console.WriteLine("PDF bound successfully.");
        }
        // Specific exception for an invalid PDF file format
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine($"Invalid PDF file format: {ex.Message}");
        }
        // Specific exception when an object reference inside the PDF is corrupted
        catch (ObjectReferenceCorruptedException ex)
        {
            Console.Error.WriteLine($"Object reference corrupted: {ex.Message}");
        }
        // General PDF-related exceptions
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        // Fallback for any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}