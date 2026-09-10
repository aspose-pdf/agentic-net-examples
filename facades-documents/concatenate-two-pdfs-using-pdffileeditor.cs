using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Create sample source PDFs so the files exist in the sandbox.
        CreateSamplePdf(firstPdf, "First PDF");
        CreateSamplePdf(secondPdf, "Second PDF");

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly.
        PdfFileEditor editor = new PdfFileEditor();

        // Use the two‑file overload to concatenate the PDFs.
        bool result = editor.Concatenate(firstPdf, secondPdf, outputPdf);

        Console.WriteLine(result
            ? $"Successfully concatenated to '{outputPdf}'."
            : "Failed to concatenate the PDF files.");
    }

    // Helper that creates a minimal PDF with a single page and optional title text.
    static void CreateSamplePdf(string path, string title)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment(title));
            doc.Save(path);
        }
    }
}
