using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added namespace for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_letter.pdf";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!System.IO.File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        // Letter size in points (1 inch = 72 points)
        const double letterWidth = 8.5 * 72; // 612 points
        const double letterHeight = 11 * 72; // 792 points

        // Facade that provides PDF editing operations
        PdfFileEditor editor = new PdfFileEditor();

        // Resize all pages (pages = null) to the specified width and height.
        // This overload writes the resized document directly to the destination path.
        editor.ResizeContents(
            source: inputPath,
            destination: outputPath,
            pages: null,          // null = all pages
            newWidth: letterWidth,
            newHeight: letterHeight);

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a very simple PDF so the resize operation has something to work on.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for resizing."));
            doc.Save(path);
        }
    }
}
