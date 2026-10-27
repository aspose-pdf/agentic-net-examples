using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input files
        const string sourcePdfPath   = "source.pdf";      // PDF from which pages will be removed
        const string secondPdfPath   = "second.pdf";      // PDF to concatenate after editing
        const string finalOutputPath = "merged_output.pdf";

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"File not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Temporary file to hold the edited source PDF
        string tempEditedPath = Path.GetTempFileName();

        try
        {
            // Load source PDF, delete pages, and save to temporary file
            using (Document srcDoc = new Document(sourcePdfPath))
            {
                // Example: delete page 2 (1‑based indexing)
                // Adjust or loop as needed for multiple pages
                if (srcDoc.Pages.Count >= 2)
                {
                    srcDoc.Pages.Delete(2);
                }

                // Save the edited document to the temporary location
                srcDoc.Save(tempEditedPath);
            }

            // Concatenate the edited PDF with the second PDF
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // The order of files determines the sequence in the merged result
            editor.Concatenate(tempEditedPath, secondPdfPath, finalOutputPath);

            Console.WriteLine($"Pages deleted and PDFs concatenated successfully. Output: {finalOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary edited file
            if (File.Exists(tempEditedPath))
            {
                try { File.Delete(tempEditedPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}