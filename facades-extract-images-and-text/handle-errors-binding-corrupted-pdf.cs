using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for exception types

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "corrupted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfExtractor instance
        PdfExtractor extractor = new PdfExtractor();

        try
        {
            // Attempt to bind the PDF file; this may throw if the file is corrupted
            extractor.BindPdf(inputPath);
            Console.WriteLine("PDF bound successfully.");
            // Extraction operations can be performed here after successful binding
        }
        catch (InvalidPdfFileFormatException ex)
        {
            // Thrown when the PDF file format is invalid or corrupted
            Console.Error.WriteLine("Invalid PDF file format:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (ObjectReferenceCorruptedException ex)
        {
            // Thrown when an object reference inside the PDF is corrupted
            Console.Error.WriteLine("Object reference corrupted in PDF:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (PdfException ex)
        {
            // General PDF processing errors
            Console.Error.WriteLine("PDF processing error:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            // Any other unexpected errors
            Console.Error.WriteLine("Unexpected error:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}