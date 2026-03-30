using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "corrupted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        try
        {
            extractor.BindPdf(inputPath);
            Console.WriteLine("PDF bound successfully.");
        }
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine("Invalid PDF file format: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine("Inner error: " + ex.InnerException.Message);
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine("PDF processing error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}