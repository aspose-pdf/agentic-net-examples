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

        // Load the PDF document to access its properties (e.g., PageLayout)
        using (Document doc = new Document(pdfPath))
        {
            // -------- Viewer Preferences --------
            // PdfContentEditor provides GetViewerPreference which returns a bitmask of ViewerPreference flags
            PdfContentEditor viewerEditor = new PdfContentEditor();
            viewerEditor.BindPdf(pdfPath);
            int viewerPref = viewerEditor.GetViewerPreference();
            Console.WriteLine($"Viewer Preference flags: 0x{viewerPref:X8}");
            // Example of checking a specific flag (optional)
            // if ((viewerPref & ViewerPreference.PageModeUseOutline) != 0) { /* ... */ }
            viewerEditor.Close();

            // -------- Page Layout --------
            // Document.PageLayout indicates how pages are displayed when the PDF is opened
            Console.WriteLine($"Page Layout: {doc.PageLayout}");

            // -------- Zoom Setting --------
            // PdfPageEditor exposes the Zoom property (default 1.0 = 100%)
            PdfPageEditor zoomEditor = new PdfPageEditor();
            zoomEditor.BindPdf(pdfPath);
            float zoomFactor = zoomEditor.Zoom;
            Console.WriteLine($"Zoom coefficient: {zoomFactor}");
            zoomEditor.Close();
        }
    }
}