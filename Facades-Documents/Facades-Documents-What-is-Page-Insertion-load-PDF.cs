using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string resultPdfPath = "result.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Error: Source PDF not found at '{sourcePdfPath}'.");
            return;
        }

        // Create a temporary PDF that contains a single blank page.
        // This file will be used as the source for the page‑insertion operation.
        string tempBlankPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        try
        {
            // Create a blank PDF with one page
            using (var blankDoc = new Document())
            {
                blankDoc.Pages.Add();
                blankDoc.Save(tempBlankPdf);
            }

            // Load the original document and the blank‑page document
            var sourceDoc = new Document(sourcePdfPath);
            var blankDocToInsert = new Document(tempBlankPdf);

            // Insert the first (and only) page of the blank document before page number 2
            sourceDoc.Pages.Insert(2, blankDocToInsert.Pages[1]);

            // Save the modified document
            sourceDoc.Save(resultPdfPath);

            Console.WriteLine($"Page insertion completed. Output saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary blank‑page PDF.
            if (File.Exists(tempBlankPdf))
            {
                try { File.Delete(tempBlankPdf); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}
