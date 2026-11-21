using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fade.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document to obtain the page count (Document is the reliable source for this information)
            Document pdfDoc = new Document(inputPath);
            int pageCount = pdfDoc.Pages.Count;

            // Use PdfPageEditor to set a Fade transition for every page.
            // TransitionType is a property of PdfPageEditor that expects an integer value.
            // According to Aspose.PDF documentation, the integer value for the Fade transition is 11.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);
                editor.ProcessPages = Enumerable.Range(1, pageCount).ToArray(); // all pages (1‑based)
                editor.TransitionType = 11; // Fade transition (int value)
                editor.TransitionDuration = 1; // 1‑second duration
                editor.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
