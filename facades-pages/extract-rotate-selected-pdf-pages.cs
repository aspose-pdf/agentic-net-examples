using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";
        // Output PDF file containing only the selected, rotated pages
        const string outputPdf = "selected_rotated.pdf";

        // Pages to extract (1‑based indexing)
        int[] pagesToExtract = { 2, 4, 5 };
        // Desired rotation for the extracted pages (must be 0, 90, 180 or 270)
        int rotationDegree = 90;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Extract the selected pages into a temporary PDF file
        // -----------------------------------------------------------------
        string tempExtractedPdf = Path.GetTempFileName();

        try
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            // Extract the specified pages and write them to the temporary file
            bool extractSuccess = fileEditor.Extract(inputPdf, pagesToExtract, tempExtractedPdf);
            if (!extractSuccess)
            {
                Console.Error.WriteLine("Page extraction failed.");
                return;
            }

            // -----------------------------------------------------------------
            // Step 2: Rotate the extracted pages
            // -----------------------------------------------------------------
            using (Document doc = new Document(tempExtractedPdf))
            {
                // Bind the document to the page editor
                PdfPageEditor pageEditor = new PdfPageEditor();
                pageEditor.BindPdf(doc);

                // Apply rotation to all pages in the extracted document
                pageEditor.Rotation = rotationDegree; // valid values: 0, 90, 180, 270

                // Commit the changes
                pageEditor.ApplyChanges();

                // -----------------------------------------------------------------
                // Step 3: Save the final PDF containing only the modified pages
                // -----------------------------------------------------------------
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Successfully created '{outputPdf}' with pages {string.Join(",", pagesToExtract)} rotated {rotationDegree}°.");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempExtractedPdf))
            {
                try { File.Delete(tempExtractedPdf); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}