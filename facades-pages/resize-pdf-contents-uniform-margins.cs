using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const string destinationPath = "output.pdf";

        // Ensure the source PDF exists; create a minimal one if it does not.
        if (!File.Exists(sourcePath))
        {
            CreateSamplePdf(sourcePath);
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
        PdfFileEditor fileEditor = new PdfFileEditor();
        // Define uniform margins of 10% on all sides.
        var resizeParams = PdfFileEditor.ContentsResizeParameters.MarginsPercent(
            left:   10,
            right:  10,
            top:    10,
            bottom: 10);

        // Resize the contents of all pages (pages = null) using the defined margins.
        bool success = fileEditor.ResizeContents(
            source:      sourcePath,
            destination: destinationPath,
            pages:       null,          // null = all pages
            parameters:  resizeParams);

        Console.WriteLine(success
            ? $"Contents resized successfully. Output saved to '{destinationPath}'."
            : "Failed to resize contents.");
    }

    // Helper that creates a very simple PDF file so the example can run without external files.
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Sample PDF for resizing."));
            doc.Save(path);
        }
    }
}
