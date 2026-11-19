using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A; this operation may involve internal XML processing.
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the converted document.
                doc.Save(outputPath);
                Console.WriteLine($"Conversion succeeded. Saved to '{outputPath}'.");
            }
        }
        catch (System.Xml.XmlException xmlEx)
        {
            // Handles malformed XML that can arise during conversion.
            Console.Error.WriteLine($"XML error during conversion: {xmlEx.Message}");
        }
        catch (PdfException pdfEx)
        {
            // Handles PDF-specific errors, including XML-related issues inside the PDF.
            Console.Error.WriteLine($"PDF processing error: {pdfEx.Message}");
        }
        catch (Exception ex)
        {
            // Fallback for any other unexpected exceptions.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}