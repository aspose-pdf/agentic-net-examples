using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class BookletTestSuite
{
    // Create a simple PDF with the specified page size (width x height in points)
    static void CreatePdf(string filePath, double width, double height)
    {
        // Ensure the directory exists
        string dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add four pages to have enough content for booklet layout
            for (int i = 1; i <= 4; i++)
            {
                // Add a blank page
                Aspose.Pdf.Page page = doc.Pages.Add();

                // Set custom page size
                page.SetPageSize(width, height);

                // Add simple text indicating page number
                Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment($"Page {i}");
                tf.Position = new Aspose.Pdf.Text.Position(50, height - 50);
                page.Paragraphs.Add(tf);
            }

            // Save the PDF
            doc.Save(filePath);
        }
    }

    // Perform booklet conversion on the given PDF
    static bool ConvertToBooklet(string inputPath, string outputPath)
    {
        // PdfFileEditor does NOT implement IDisposable; no using block needed
        Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

        // Use the overload that does not require a PageSize argument
        bool success = editor.MakeBooklet(inputPath, outputPath);

        // No need to call Close() on editor
        return success;
    }

    static void Main()
    {
        // Define test cases with custom page sizes (width, height in points)
        var testCases = new[]
        {
            new { Name = "LetterSize", Width = 612.0, Height = 792.0 },   // 8.5" x 11"
            new { Name = "LegalSize",  Width = 612.0, Height = 1008.0 }, // 8.5" x 14"
            new { Name = "CustomA5",   Width = 420.0, Height = 595.0 }, // approx A5
            new { Name = "CustomSquare", Width = 600.0, Height = 600.0 }
        };

        string baseDir = Path.Combine(Directory.GetCurrentDirectory(), "BookletTests");
        if (!Directory.Exists(baseDir))
            Directory.CreateDirectory(baseDir);

        foreach (var tc in testCases)
        {
            string sourcePdf = Path.Combine(baseDir, $"{tc.Name}_source.pdf");
            string bookletPdf = Path.Combine(baseDir, $"{tc.Name}_booklet.pdf");

            // Create source PDF with the specified page size
            CreatePdf(sourcePdf, tc.Width, tc.Height);
            Console.WriteLine($"Created source PDF: {sourcePdf}");

            // Convert to booklet
            bool ok = ConvertToBooklet(sourcePdf, bookletPdf);
            if (ok)
                Console.WriteLine($"Booklet created successfully: {bookletPdf}");
            else
                Console.WriteLine($"Failed to create booklet for: {sourcePdf}");
        }

        Console.WriteLine("Booklet test suite completed.");
    }
}