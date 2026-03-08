using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";      // Existing PDF to take pages from
        const string outputPdf = "combined.pdf";    // Resulting PDF file
        const int startPage = 2;                    // First page to copy (1‑based)
        const int endPage   = 4;                    // Last page to copy (inclusive)

        // Verify the source file exists
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Create a temporary empty PDF document (the new document to which pages will be appended)
        string tempPdfPath = Path.GetTempFileName();
        try
        {
            // Document disposal follows the recommended using pattern
            using (Document emptyDoc = new Document())
            {
                // The default constructor creates a PDF with a single blank page.
                emptyDoc.Save(tempPdfPath);
            }

            // Use PdfFileEditor to append the selected page range from the source PDF
            PdfFileEditor editor = new PdfFileEditor();
            bool appended = editor.Append(tempPdfPath, sourcePdf, startPage, endPage, outputPdf);

            if (appended)
                Console.WriteLine($"Successfully appended pages {startPage}-{endPage} to '{outputPdf}'.");
            else
                Console.Error.WriteLine("Failed to append pages.");
        }
        finally
        {
            // Clean up the temporary file regardless of success or failure
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);
        }
    }
}