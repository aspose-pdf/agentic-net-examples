using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify that source files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        // Use PdfFileEditor (a Facade) to perform PDF operations.
        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        PdfFileEditor editor = new PdfFileEditor();

        try
        {
            // TryConcatenate returns false on failure without throwing.
            // If it fails, inspect LastException for details.
            bool success = editor.TryConcatenate(firstPdf, secondPdf, outputPdf);
            if (!success)
            {
                Console.Error.WriteLine("Concatenation failed.");
                if (editor.LastException != null)
                {
                    Console.Error.WriteLine($"Error: {editor.LastException.Message}");
                    if (editor.LastException.InnerException != null)
                    {
                        Console.Error.WriteLine($"Inner: {editor.LastException.InnerException.Message}");
                    }
                }
                return;
            }

            // If concatenation succeeded, optionally open the result with a Document
            // to demonstrate further processing and PdfException handling.
            using (Document doc = new Document(outputPdf))
            {
                // Example operation: display page count (1‑based indexing)
                Console.WriteLine($"Merged PDF has {doc.Pages.Count} pages.");

                // Save the document again (demonstrating proper save usage)
                // Document.Save(string) without SaveOptions always writes PDF.
                doc.Save("final_output.pdf");
            }

            Console.WriteLine($"Successfully merged PDFs into '{outputPdf}'.");
        }
        catch (PdfException pdfEx)
        {
            // Catch any Aspose.Pdf PdfException that occurs during processing.
            Console.Error.WriteLine($"PdfException caught: {pdfEx.Message}");
            if (pdfEx.InnerException != null)
            {
                Console.Error.WriteLine($"Inner exception: {pdfEx.InnerException.Message}");
            }
        }
        catch (Exception ex)
        {
            // Catch any other unexpected exceptions.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}