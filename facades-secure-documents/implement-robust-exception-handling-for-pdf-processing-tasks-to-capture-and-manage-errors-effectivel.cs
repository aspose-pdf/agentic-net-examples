using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for demonstration – adjust as needed.
        const string sourcePdf      = "source.pdf";
        const string secondPdf      = "second.pdf";
        const string mergedPdf      = "merged.pdf";
        const string sanitizedPdf   = "sanitized.pdf";
        const string extractedText  = "extracted.txt";

        // -----------------------------------------------------------------
        // 1. Extract text from a PDF using PdfExtractor (Facade API)
        // -----------------------------------------------------------------
        try
        {
            // Create the extractor facade.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF document.
                extractor.BindPdf(sourcePdf);

                // Perform extraction. The modern API does not expose a TextExtractionMode property;
                // the default mode extracts text with formatting which matches the original intent.
                extractor.ExtractText();

                // Save extracted text to a file.
                extractor.GetText(extractedText);
                Console.WriteLine($"Text extracted to '{extractedText}'.");
            }
        }
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine($"Invalid PDF format: {ex.Message}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during text extraction: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 2. Sanitize a PDF using PdfFileSanitization (Facade API)
        // -----------------------------------------------------------------
        try
        {
            // Create the sanitization facade.
            using (PdfFileSanitization sanitizer = new PdfFileSanitization())
            {
                // Load the PDF to be sanitized.
                sanitizer.BindPdf(sourcePdf);

                // Optional: enable rebuilding of cross‑reference table and trailer.
                sanitizer.UseRebuildXrefAndTrailer = true;

                // Perform sanitization (recover corrupted structures, trim excess data, etc.).
                sanitizer.Recover();

                // Save the sanitized PDF.
                sanitizer.Save(sanitizedPdf);
                Console.WriteLine($"Sanitized PDF saved as '{sanitizedPdf}'.");
            }
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during sanitization: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 3. Concatenate two PDFs using PdfFileEditor (Facade API)
        // -----------------------------------------------------------------
        try
        {
            // PdfFileEditor does not implement IDisposable in recent versions, so we instantiate it
            // without a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // The modern API provides a Concatenate method that accepts an array of source files.
            // It throws PdfException on failure, which we catch below.
            string[] sourceFiles = new[] { sourcePdf, secondPdf };
            editor.Concatenate(sourceFiles, mergedPdf);

            Console.WriteLine($"PDFs concatenated successfully into '{mergedPdf}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF editing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during concatenation: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 4. Convert a PDF to PDF/A using Document.Convert (core API) with robust handling
        // -----------------------------------------------------------------
        const string pdfaOutput = "output_pdfa.pdf";
        const string conversionLog = "conversion_log.xml";

        try
        {
            // Use a using block for deterministic disposal of the Document.
            using (Document doc = new Document(sourcePdf))
            {
                // Convert to PDF/A‑1B. The overload with a log file captures conversion details.
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                doc.Save(pdfaOutput);
                Console.WriteLine($"PDF/A document saved as '{pdfaOutput}'.");
            }
        }
        catch (ConvertException ex)
        {
            Console.Error.WriteLine($"Conversion to PDF/A failed: {ex.Message}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF error during conversion: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error during PDF/A conversion: {ex.Message}");
        }
    }
}
