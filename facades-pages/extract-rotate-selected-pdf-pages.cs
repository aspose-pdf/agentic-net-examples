using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, temporary extracted PDF, and final output PDF paths
        const string inputPdf = "input.pdf";
        const string tempPdf = "temp_extracted.pdf";
        const string outputPdf = "rotated_output.pdf";

        // Pages to extract (1‑based indexing)
        int[] selectedPages = new int[] { 2, 4, 5 };

        // Desired rotation angle (must be 0, 90, 180 or 270)
        int rotationAngle = 90;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Extract the selected pages into a temporary PDF file
        // ------------------------------------------------------------
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool extracted = fileEditor.Extract(inputPdf, selectedPages, tempPdf);
        if (!extracted)
        {
            Console.Error.WriteLine("Failed to extract selected pages.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Rotate the pages of the extracted PDF
        // ------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the temporary PDF to the editor
            pageEditor.BindPdf(tempPdf);

            // Apply the rotation to all pages (default processes every page)
            pageEditor.Rotation = rotationAngle;

            // Commit the changes
            pageEditor.ApplyChanges();

            // Save the rotated PDF to the final output path
            pageEditor.Save(outputPdf);
        }

        // Clean up the temporary file
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Rotated PDF created at: {outputPdf}");
    }
}