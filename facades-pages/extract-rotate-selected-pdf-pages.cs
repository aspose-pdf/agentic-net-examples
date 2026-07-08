using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, pages to extract, rotation angle, and final output PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "rotated_selected_pages.pdf";
        int[] pagesToExtract = new int[] { 2, 4, 5 }; // example page numbers (1‑based)
        int rotationAngle = 90; // must be 0, 90, 180 or 270

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Extract the selected pages to a temporary PDF file
        // -----------------------------------------------------------------
        string tempExtractPath = Path.GetTempFileName(); // creates a temp file with .tmp extension
        try
        {
            // PdfFileEditor.Extract writes the selected pages to the specified file
            PdfFileEditor fileEditor = new PdfFileEditor();
            bool extractSuccess = fileEditor.Extract(inputPdfPath, pagesToExtract, tempExtractPath);
            if (!extractSuccess)
            {
                Console.Error.WriteLine("Page extraction failed.");
                return;
            }

            // -----------------------------------------------------------------
            // Step 2: Rotate the extracted pages and save the final PDF
            // -----------------------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the temporary PDF that contains only the extracted pages
                pageEditor.BindPdf(tempExtractPath);

                // Rotate all pages in the extracted document
                pageEditor.ProcessPages = new int[pageEditor.GetPages()]; // allocate array for all pages
                for (int i = 0; i < pageEditor.ProcessPages.Length; i++)
                {
                    // Pages are 1‑based; fill the array with page numbers 1..Count
                    pageEditor.ProcessPages[i] = i + 1;
                }
                pageEditor.Rotation = rotationAngle;

                // Apply the rotation changes
                pageEditor.ApplyChanges();

                // Save the modified document to the final output path
                pageEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully created '{outputPdfPath}' with rotated pages.");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempExtractPath))
            {
                try { File.Delete(tempExtractPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}