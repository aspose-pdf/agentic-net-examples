using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        // ------------------------------------------------------------
        // Ensure a PDF file exists – create a minimal one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a one‑page PDF with a simple paragraph.
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample PDF – generated because 'input.pdf' was not found."));
                doc.Save(inputPdf);
            }
            Console.WriteLine($"Created placeholder PDF: {inputPdf}");
        }

        // ------------------------------------------------------------
        // Get viewer preferences (page layout, etc.)
        // ------------------------------------------------------------
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(inputPdf);

            // GetViewerPreference returns an int that represents ViewerPreference flags.
            int prefFlags = contentEditor.GetViewerPreference();

            string layout = "Unknown";
            if ((prefFlags & (int)ViewerPreference.PageLayoutOneColumn) != 0)
                layout = "OneColumn";
            else if ((prefFlags & (int)ViewerPreference.PageLayoutSinglePage) != 0)
                layout = "SinglePage";
            else if ((prefFlags & (int)ViewerPreference.PageLayoutTwoColumnLeft) != 0)
                layout = "TwoColumnLeft";
            else if ((prefFlags & (int)ViewerPreference.PageLayoutTwoColumnRight) != 0)
                layout = "TwoColumnRight";

            Console.WriteLine($"Viewer Page Layout: {layout}");
        }

        // ------------------------------------------------------------
        // Get default zoom setting
        // ------------------------------------------------------------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPdf);

            // The Zoom property returns a float where 1.0 = 100%.
            float zoomFactor = pageEditor.Zoom;
            Console.WriteLine($"Default Zoom Factor: {zoomFactor * 100}%");
        }
    }
}
