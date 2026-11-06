using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string nupPdfPath     = "temp_nup.pdf";
        const string bookletPdfPath = "booklet_output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Create an N‑up layout (e.g., 2‑up) PDF
        // -------------------------------------------------
        // Use PdfFileEditor.MakeNUp – it returns a bool indicating success.
        var pdfEditor = new PdfFileEditor();
        string[] sourceFiles = new[] { inputPdfPath };
        bool isLandscape = false; // false => portrait (2‑up will be stacked vertically)
        bool nupSuccess = pdfEditor.MakeNUp(sourceFiles, nupPdfPath, isLandscape);
        if (!nupSuccess)
        {
            Console.Error.WriteLine("Failed to create N‑up PDF.");
            return;
        }

        // -------------------------------------------------
        // Step 2: Convert the N‑up PDF into a booklet
        // -------------------------------------------------
        // PdfFileEditor provides a MakeBooklet overload (no PageSize argument).
        bool bookletSuccess = pdfEditor.MakeBooklet(nupPdfPath, bookletPdfPath);
        if (!bookletSuccess)
        {
            Console.Error.WriteLine("Failed to create booklet PDF.");
            return;
        }

        // -------------------------------------------------
        // Optional: Verify the result by opening it with Document
        // -------------------------------------------------
        using (Document resultDoc = new Document(bookletPdfPath))
        {
            Console.WriteLine($"Booklet created successfully. Pages: {resultDoc.Pages.Count}");
        }

        // Clean up temporary N‑up file
        try
        {
            File.Delete(nupPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}
