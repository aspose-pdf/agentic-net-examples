using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for exception types

class Program
{
    static void Main()
    {
        const string inputPdf = "corrupted.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor does not implement IDisposable, so no using block is required.
        PdfExtractor extractor = new PdfExtractor();

        try
        {
            // Attempt to bind the PDF file. This may throw if the file is corrupted.
            extractor.BindPdf(inputPdf);
            Console.WriteLine("PDF bound successfully.");
            // Further extraction logic would go here.
        }
        catch (InvalidPdfFileFormatException ex)
        {
            // Thrown when the PDF file format is invalid.
            Console.Error.WriteLine("Invalid PDF file format:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (ObjectReferenceCorruptedException ex)
        {
            // Thrown when an object reference inside the PDF is corrupted.
            Console.Error.WriteLine("Object reference corrupted in PDF:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (PdfException ex)
        {
            // General PDF-related errors.
            Console.Error.WriteLine("PDF processing error:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            // Fallback for any other unexpected errors.
            Console.Error.WriteLine("Unexpected error:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}