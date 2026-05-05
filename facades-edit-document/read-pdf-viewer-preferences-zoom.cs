using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // ----- Get viewer preferences -----
        using (PdfContentEditor viewerEditor = new PdfContentEditor())
        {
            viewerEditor.BindPdf(pdfPath);
            int prefValue = viewerEditor.GetViewerPreference();

            // Determine page layout from ViewerPreference flags
            string layout = prefValue switch
            {
                var v when (v & ViewerPreference.PageLayoutOneColumn) != 0 => "One Column",
                var v when (v & ViewerPreference.PageLayoutSinglePage) != 0 => "Single Page",
                var v when (v & ViewerPreference.PageLayoutTwoColumnLeft) != 0 => "Two Column Left",
                var v when (v & ViewerPreference.PageLayoutTwoColumnRight) != 0 => "Two Column Right",
                _ => "Unknown"
            };

            Console.WriteLine($"Viewer Preference - Page Layout: {layout}");

            // Log other relevant flags (example: Hide Menubar)
            bool hideMenubar = (prefValue & ViewerPreference.HideMenubar) != 0;
            Console.WriteLine($"Viewer Preference - Hide Menubar: {hideMenubar}");
        }

        // ----- Get current zoom factor (default view zoom) -----
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(pdfPath);
            float zoomFactor = pageEditor.Zoom; // default is 1.0 (100%)
            Console.WriteLine($"Current Zoom Factor: {zoomFactor * 100}%");
        }
    }
}