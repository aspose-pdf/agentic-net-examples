using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // Original PDF (will be created if missing)
        const string rotatedPdf = "rotated.pdf";    // Intermediate PDF in landscape
        const string bookletPdf = "booklet.pdf";    // Final booklet PDF

        // Ensure a source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
            Console.WriteLine($"Sample source PDF created: {inputPdf}");
        }

        // Step 1: Rotate all pages to landscape orientation.
        // PdfPageEditor binds the source PDF, applies a 90° rotation,
        // and saves the result to an intermediate file.
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPdf);          // Load source PDF
            pageEditor.Rotation = 90;              // Rotate pages 90 degrees
            pageEditor.PageSize = PageSize.A4;     // Set base page size (rotation makes it landscape)
            pageEditor.Save(rotatedPdf);           // Save rotated PDF
        }

        // Step 2: Generate a booklet from the rotated PDF.
        // PdfFileEditor provides the MakeBooklet method which creates the booklet.
        // NOTE: PdfFileEditor does NOT implement IDisposable, so it must NOT be used in a using block.
        var fileEditor = new PdfFileEditor();
        bool success = fileEditor.MakeBooklet(rotatedPdf, bookletPdf);
        if (!success)
        {
            Console.Error.WriteLine("Failed to create booklet.");
            return;
        }

        Console.WriteLine($"Booklet created successfully: {bookletPdf}");
    }

    /// <summary>
    /// Creates a minimal PDF containing a single page with sample text.
    /// This method is used only when the expected input file is missing, allowing the sample to run
    /// without external resources.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        // Using Aspose.Pdf.Document to build a simple PDF in memory.
        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // Add a text paragraph.
            page.Paragraphs.Add(new TextFragment("Sample PDF for rotation and booklet generation."));

            // Save to the specified path.
            doc.Save(path);
        }
    }
}
