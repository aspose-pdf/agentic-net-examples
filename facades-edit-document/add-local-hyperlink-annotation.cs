using System;
using System.IO; // for File operations
using Aspose.Pdf; // core PDF classes
using Aspose.Pdf.Facades; // PdfContentEditor
using Aspose.Pdf.Text; // TextFragment and Position

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf"; // PDF with the hyperlink annotation
        const int linkPage = 1;                 // page where the link rectangle will be placed (1‑based)
        const int targetPage = 3;               // page to navigate to when the link is clicked (1‑based)

        // ------------------------------------------------------------
        // Ensure the source PDF exists and has at least 'targetPage' pages.
        // If it does not exist, create a simple multi‑page PDF on‑the‑fly.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf, targetPage);
        }
        else
        {
            // If the file exists but has fewer pages than required, add the missing pages.
            var doc = new Document(inputPdf);
            if (doc.Pages.Count < targetPage)
            {
                for (int i = doc.Pages.Count + 1; i <= targetPage; i++)
                {
                    var page = doc.Pages.Add();
                    var tf = new TextFragment($"Page {i}");
                    // Position expects Aspose.Pdf.Text.Position, not Aspose.Pdf.Point
                    tf.Position = new Position(100, 700);
                    page.Paragraphs.Add(tf);
                }
                doc.Save(inputPdf);
            }
        }

        // Define the clickable area (left, top, width, height) in points.
        // Use the System.Drawing.Rectangle that the PdfContentEditor API expects.
        System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);

        // Create the facade, bind the PDF, add the local link, and save.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPdf);

            // Create a local link on 'linkPage' that jumps to 'targetPage'.
            // The rectangle will be drawn in red for visibility.
            editor.CreateLocalLink(linkRect, targetPage, linkPage, System.Drawing.Color.Red);

            // Persist the changes.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Hyperlink annotation added. Output saved to '{outputPdf}'.");
    }

    // Helper method that creates a simple PDF with the requested number of pages.
    private static void CreateSamplePdf(string path, int pageCount)
    {
        var doc = new Document();
        for (int i = 1; i <= pageCount; i++)
        {
            var page = doc.Pages.Add();
            var tf = new TextFragment($"Page {i}");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);
        }
        doc.Save(path);
    }
}
