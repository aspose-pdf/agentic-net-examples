using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <input_epub_path> <output_pdf_path>
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input_epub_path> <output_pdf_path>");
            return;
        }

        string epubPath = args[0];
        string outputPath = args[1];

        // Verify that the EPUB file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Error: EPUB file not found at '{epubPath}'.");
            return;
        }

        try
        {
            // Load the EPUB document with default options
            using (Document epubDocument = new Document(epubPath, new EpubLoadOptions()))
            {
                // Prepare conversion options for PDF/E-1 format
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_E_1);

                // Convert the loaded document to PDF/E-1 (Convert returns a bool, not a Document)
                bool conversionSucceeded = epubDocument.Convert(conversionOptions);
                if (!conversionSucceeded)
                {
                    Console.Error.WriteLine("Conversion failed: Convert method returned false.");
                    return;
                }

                // Save the resulting PDF/E-1 file (the same Document instance is now a PDF/E-1 document)
                epubDocument.Save(outputPath);
                Console.WriteLine($"Successfully converted EPUB to PDF/E-1 and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
