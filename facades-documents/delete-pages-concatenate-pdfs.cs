using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string sourcePdf      = "source.pdf";        // PDF from which pages will be deleted
        const string secondPdf      = "second.pdf";        // PDF to concatenate after deletion
        const string tempDeletedPdf = "source_deleted.pdf";// Intermediate file after page deletion
        const string outputPdf      = "merged.pdf";        // Final concatenated result

        // Pages to delete (1‑based indexing). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 3 };

        // Verify that the required files exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Error: File not found – {sourcePdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdf}");
            return;
        }

        try
        {
            // Create the PdfFileEditor instance (does NOT implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // OPTIONAL: close streams automatically after each operation
            editor.CloseConcatenatedStreams = true;

            // 1) Delete the specified pages from the first PDF.
            // The Delete method writes the result to tempDeletedPdf.
            bool deleteSuccess = editor.Delete(sourcePdf, pagesToDelete, tempDeletedPdf);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine("Error: Page deletion failed.");
                return;
            }

            // 2) Concatenate the edited PDF with the second PDF.
            // The Concatenate method creates the final merged PDF.
            bool concatSuccess = editor.Concatenate(tempDeletedPdf, secondPdf, outputPdf);
            if (!concatSuccess)
            {
                Console.Error.WriteLine("Error: Concatenation failed.");
                return;
            }

            Console.WriteLine($"Success: Pages deleted and PDFs concatenated. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}