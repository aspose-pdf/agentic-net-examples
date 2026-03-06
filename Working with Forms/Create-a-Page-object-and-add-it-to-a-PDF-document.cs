using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a new empty page to the document (pages are 1‑based)
            Page newPage = pdfDoc.Pages.Add();

            // Example: set the page size to A4 (595 x 842 points)
            newPage.SetPageSize(595, 842);

            // Save the document as PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with a new page saved to '{outputPath}'.");
    }
}