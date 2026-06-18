using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF to read document‑level properties (e.g., PageLayout)
        using (Document doc = new Document(pdfPath))
        {
            // Get the page layout setting from the Document object
            PageLayout layout = doc.PageLayout;
            Console.WriteLine($"Page Layout: {layout}");

            // Use PdfContentEditor to retrieve viewer preference flags
            PdfContentEditor viewerEditor = new PdfContentEditor();
            viewerEditor.BindPdf(pdfPath);
            int prefFlags = viewerEditor.GetViewerPreference();

            // Example: check a few common flags
            if ((prefFlags & ViewerPreference.PageLayoutOneColumn) != 0)
                Console.WriteLine("Viewer Preference: One Column layout");
            if ((prefFlags & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
                Console.WriteLine("Viewer Preference: Two Column Left layout");
            if ((prefFlags & ViewerPreference.PageLayoutTwoColumnRight) != 0)
                Console.WriteLine("Viewer Preference: Two Column Right layout");
            if ((prefFlags & ViewerPreference.PageLayoutSinglePage) != 0)
                Console.WriteLine("Viewer Preference: Single Page layout");

            // Use PdfPageEditor to obtain the current zoom coefficient
            PdfPageEditor zoomEditor = new PdfPageEditor();
            zoomEditor.BindPdf(pdfPath);
            float zoom = zoomEditor.Zoom;
            Console.WriteLine($"Current Zoom: {zoom * 100}%");
        }
    }
}