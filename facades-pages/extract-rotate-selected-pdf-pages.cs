using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, temporary extracted PDF, and final output PDF paths
        const string inputPdf   = "input.pdf";
        const string tempPdf    = "temp_extracted.pdf";
        const string outputPdf  = "output.pdf";

        // Pages to keep (1‑based indexing) and rotation angle (0, 90, 180, 270)
        int[] pagesToExtract = new int[] { 2, 4, 5 };
        int   rotationDegree = 90;

        // Validate input file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // Step 1: Extract the selected pages into a temporary PDF file
            // ------------------------------------------------------------
            PdfFileEditor fileEditor = new PdfFileEditor(); // not disposable
            fileEditor.Extract(inputPdf, pagesToExtract, tempPdf);

            // ------------------------------------------------------------
            // Step 2: Rotate the extracted pages using PdfPageEditor
            // ------------------------------------------------------------
            PdfPageEditor pageEditor = new PdfPageEditor(); // not disposable
            pageEditor.BindPdf(tempPdf);                 // Load the temporary PDF
            pageEditor.ProcessPages = pagesToExtract;    // Pages to apply rotation
            pageEditor.Rotation = rotationDegree;        // Set desired rotation
            pageEditor.ApplyChanges();                   // Apply modifications
            pageEditor.Save(outputPdf);                  // Save final PDF

            // ------------------------------------------------------------
            // Cleanup: delete the temporary file
            // ------------------------------------------------------------
            if (File.Exists(tempPdf))
                File.Delete(tempPdf);

            Console.WriteLine($"Created rotated PDF: '{outputPdf}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
