using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";   // original PDF
        const string outputPdf = "booklet.pdf";  // final booklet PDF

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Pages to remove (1‑based page numbers). Adjust as needed.
        int[] pagesToDelete = new int[] { 2, 5 };

        // Temporary file that will hold the PDF after page deletion.
        string tempPdf = Path.GetTempFileName();

        try
        {
            // ---------- Delete unwanted pages ----------
            // Load the PDF with Document (cross‑platform, always available).
            Document pdfDoc = new Document(sourcePdf);

            // Delete pages in descending order so that page numbers remain valid.
            foreach (int pageNum in pagesToDelete.OrderByDescending(p => p))
            {
                if (pageNum >= 1 && pageNum <= pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages.Delete(pageNum);
                }
                else
                {
                    Console.Error.WriteLine($"Page number {pageNum} is out of range and will be ignored.");
                }
            }

            // Save the cleaned PDF to a temporary file.
            pdfDoc.Save(tempPdf);

            // ---------- Create booklet from the cleaned PDF ----------
            // PdfFileEditor provides the MakeBooklet method that works on all supported platforms.
            PdfFileEditor editor = new PdfFileEditor();
            editor.MakeBooklet(tempPdf, outputPdf);
        }
        finally
        {
            // Remove the temporary file regardless of success or failure.
            if (File.Exists(tempPdf))
                File.Delete(tempPdf);
        }

        Console.WriteLine($"Booklet created successfully: {outputPdf}");
    }
}
