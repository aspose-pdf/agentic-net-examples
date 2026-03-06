using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path (must exist)
        const string inputPath = "input.pdf";

        // Output paths
        const string outputPath = "output.pdf";                 // saved after basic operations
        const string convertedPath = "output_pdfa.pdf";         // saved after PDF/A conversion
        const string textExtractPath = "extracted_text.txt";    // extracted text file
        const string conversionLog = "conversion_log.txt";      // log for conversion errors

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the PDF document (using statement ensures disposal)
            // -------------------------------------------------
            using (Document doc = new Document(inputPath))
            {
                // -------------------------------------------------
                // 2. Basic document information (overview)
                // -------------------------------------------------
                Console.WriteLine($"Pages: {doc.Pages.Count}");
                Console.WriteLine($"Title: {doc.Info.Title}");
                Console.WriteLine($"Author: {doc.Info.Author}");
                Console.WriteLine($"Subject: {doc.Info.Subject}");

                // -------------------------------------------------
                // 3. Parse the PDF – extract all text using TextAbsorber
                // -------------------------------------------------
                TextAbsorber absorber = new TextAbsorber();
                // Use default extraction options (Pure formatting)
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text ?? string.Empty;

                // Save extracted text to a file (optional)
                File.WriteAllText(textExtractPath, extractedText);
                Console.WriteLine($"Extracted text saved to: {textExtractPath}");

                // -------------------------------------------------
                // 4. Save the original document as PDF (basic operation)
                // -------------------------------------------------
                // Document.Save(string) without SaveOptions always writes PDF.
                doc.Save(outputPath);
                Console.WriteLine($"Original PDF saved to: {outputPath}");

                // -------------------------------------------------
                // 5. Convert the PDF to PDF/A-1B (PDF/A compliance)
                // -------------------------------------------------
                // The Convert method returns a bool indicating success.
                // Provide a log file to capture any conversion errors.
                bool conversionResult = doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                Console.WriteLine($"Conversion to PDF/A-1B {(conversionResult ? "succeeded" : "failed")}. Log: {conversionLog}");

                // Save the converted document as a new PDF file.
                doc.Save(convertedPath);
                Console.WriteLine($"PDF/A-1B document saved to: {convertedPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}