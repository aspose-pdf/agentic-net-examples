using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF (contains the vector graphic) and target PDF
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "merged_output.pdf";

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        try
        {
            // Load source and target documents
            using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(sourcePdfPath))
            using (Aspose.Pdf.Document targetDoc = new Aspose.Pdf.Document(targetPdfPath))
            {
                // Assume the vector graphic is on the first page of the source PDF.
                // Extract that page.
                Aspose.Pdf.Page vectorPage = sourceDoc.Pages[1];

                // Insert the extracted page into the target document.
                // Here we insert it after the first page of the target PDF.
                // Pages.Insert takes the position (1‑based) and the page to insert.
                targetDoc.Pages.Insert(2, vectorPage);

                // Save the resulting document.
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Vector graphic page inserted and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}