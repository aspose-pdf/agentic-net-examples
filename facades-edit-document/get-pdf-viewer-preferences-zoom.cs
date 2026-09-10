using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF to access its PageLayout property.
        using (Document doc = new Document(pdfPath))
        {
            // Document's own page layout setting.
            var docLayout = doc.PageLayout;
            Console.WriteLine($"Document PageLayout property: {docLayout}");

            // Retrieve viewer preference flags via PdfContentEditor.
            using (PdfContentEditor viewerEditor = new PdfContentEditor())
            {
                viewerEditor.BindPdf(pdfPath);
                int prefFlags = viewerEditor.GetViewerPreference();

                Console.WriteLine($"Viewer Preference Flags: 0x{prefFlags:X}");

                // Decode and log specific layout preferences.
                if ((prefFlags & ViewerPreference.PageLayoutOneColumn) != 0)
                    Console.WriteLine("Viewer Preference: Page Layout – One Column");
                if ((prefFlags & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
                    Console.WriteLine("Viewer Preference: Page Layout – Two Column Left");
                if ((prefFlags & ViewerPreference.PageLayoutTwoColumnRight) != 0)
                    Console.WriteLine("Viewer Preference: Page Layout – Two Column Right");
                if ((prefFlags & ViewerPreference.PageLayoutSinglePage) != 0)
                    Console.WriteLine("Viewer Preference: Page Layout – Single Page");
            }

            // Retrieve the current zoom coefficient via PdfPageEditor.
            using (PdfPageEditor zoomEditor = new PdfPageEditor())
            {
                zoomEditor.BindPdf(pdfPath);
                float currentZoom = zoomEditor.Zoom; // Default is 1.0 if not set.
                Console.WriteLine($"Current Zoom Coefficient: {currentZoom}");
            }
        }
    }
}