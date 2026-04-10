using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";    // source PDF
        const string previewPath = "preview.pdf";  // preview with reduced zoom
        const string finalPath   = "final.pdf";    // final output with normal zoom

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        // ---------- Preview: zoom to 0.5 (50%) ----------
        using (PdfPageEditor previewEditor = new PdfPageEditor())
        {
            previewEditor.BindPdf(inputPath);
            previewEditor.Zoom = 0.5f; // 50% scaling
            previewEditor.ApplyChanges();
            previewEditor.Save(previewPath);
        }

        // ---------- Final output: zoom to 1.0 (100%) ----------
        using (PdfPageEditor finalEditor = new PdfPageEditor())
        {
            finalEditor.BindPdf(inputPath);
            finalEditor.Zoom = 1.0f; // 100% scaling (no zoom)
            finalEditor.ApplyChanges();
            finalEditor.Save(finalPath);
        }

        Console.WriteLine("Preview and final PDFs have been created.");
    }

    /// <summary>
    /// Creates a minimal PDF document with a single page containing some text.
    /// This method is used only when the expected input file is missing, preventing
    /// a FileNotFoundException at runtime.
    /// </summary>
    /// <param name="path">The file path where the PDF should be saved.</param>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF generated because 'input.pdf' was not found."));
            doc.Save(path);
        }
    }
}
