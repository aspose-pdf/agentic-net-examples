using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output PDF path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ReportDocument <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Perform a simple operation – retrieve the number of pages
            int pageCount = pdfDocument.Pages.Count;
            Console.WriteLine($"PDF loaded successfully. Page count: {pageCount}");

            // Save a copy of the document to the specified output path
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Document saved to: {outputPath}");
        }
        // Capture specific Aspose.Pdf exceptions for detailed reporting
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine("Invalid PDF file format:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (ObjectReferenceCorruptedException ex)
        {
            Console.Error.WriteLine("Corrupted object reference detected:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine("General PDF processing error:");
            Console.Error.WriteLine(ex.Message);
        }
        // Fallback for any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine("An unexpected error occurred:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}
