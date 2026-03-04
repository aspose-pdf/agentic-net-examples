using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf1 = "file1.pdf";
        const string sourcePdf2 = "file2.pdf";
        const string mergedPdf  = "merged.pdf";

        // Verify that source files exist before processing
        if (!File.Exists(sourcePdf1) || !File.Exists(sourcePdf2))
        {
            Console.Error.WriteLine("One or more source PDF files are missing.");
            return;
        }

        try
        {
            // PdfFileEditor is a facade for file‑level operations (concatenation, split, etc.)
            PdfFileEditor editor = new PdfFileEditor();

            // Attempt to concatenate the two PDFs.
            // TryConcatenate returns false on failure; detailed info is available via LastException.
            bool concatenated = editor.TryConcatenate(sourcePdf1, sourcePdf2, mergedPdf);
            if (!concatenated)
            {
                Console.Error.WriteLine("Failed to concatenate PDFs.");
                if (editor.LastException != null)
                {
                    Console.Error.WriteLine($"Error: {editor.LastException.Message}");
                    if (editor.LastException.InnerException != null)
                        Console.Error.WriteLine($"Inner error: {editor.LastException.InnerException.Message}");
                }
                return;
            }

            // Load the merged document for further processing.
            using (Document doc = new Document(mergedPdf))
            {
                // Example of a PDF/A conversion that may throw ConvertException.
                try
                {
                    string logFile = "conversion_log.xml";
                    // Convert to PDF/A‑1B; errors are written to the log file.
                    doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                    // Save the (potentially) modified document.
                    doc.Save(mergedPdf);
                }
                catch (ConvertException convEx)
                {
                    Console.Error.WriteLine($"ConvertException: {convEx.Message}");
                }
                catch (PdfException pdfEx)
                {
                    Console.Error.WriteLine($"PdfException during conversion: {pdfEx.Message}");
                }
            }
        }
        // Specific catch for an invalid PDF file format.
        catch (InvalidPdfFileFormatException invEx)
        {
            Console.Error.WriteLine($"InvalidPdfFileFormatException: {invEx.Message}");
        }
        // General PDF‑related exceptions.
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"PdfException: {pdfEx.Message}");
        }
        // Fallback for any other unexpected errors.
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}