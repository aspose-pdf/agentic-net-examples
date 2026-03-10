using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template PDF and the final output PDF
        const string inputPdfPath  = "template.pdf";
        const string outputPdfPath = "final.pdf";

        // -----------------------------------------------------------------
        // Simulated values coming from HTML controls (e.g., text boxes)
        // In a real web app these would be read from request parameters.
        // -----------------------------------------------------------------
        string title   = "Quarterly Financial Report";
        string author  = "Jane Smith";
        string subject = "Q1 2026 Results";
        string keywords = "finance,quarterly,2026";

        // ---------------------------------------------------------------
        // Validate that the source PDF exists before trying to bind it.
        // ---------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPdfPath}' not found.");
            return; // Exit gracefully – no NullReferenceException will be thrown.
        }

        try
        {
            // Load the PDF, modify its metadata, and save the updated document.
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                // Bind the existing PDF file to the facade.
                pdfInfo.BindPdf(inputPdfPath);

                // Set standard metadata properties.
                pdfInfo.Title   = title;
                pdfInfo.Author  = author;
                pdfInfo.Subject = subject;
                pdfInfo.Keywords = keywords;

                // PdfFileInfo expects dates as strings in PDF date format (yyyyMMddHHmmss).
                string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                pdfInfo.CreationDate = pdfDate;
                pdfInfo.ModDate      = pdfDate;

                // Save the PDF with the new metadata.
                pdfInfo.SaveNewInfo(outputPdfPath);
            }

            Console.WriteLine($"PDF metadata updated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Log unexpected errors without crashing the application.
            Console.Error.WriteLine($"An error occurred while updating PDF metadata: {ex.Message}");
        }
    }
}
