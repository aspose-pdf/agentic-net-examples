using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ----- Get viewer preferences (page layout, etc.) -----
        PdfContentEditor viewerEditor = new PdfContentEditor();
        viewerEditor.BindPdf(inputPath);
        int prefFlags = viewerEditor.GetViewerPreference();

        string pageLayout = prefFlags switch
        {
            var f when (f & ViewerPreference.PageLayoutOneColumn) != 0       => "OneColumn",
            var f when (f & ViewerPreference.PageLayoutSinglePage) != 0      => "SinglePage",
            var f when (f & ViewerPreference.PageLayoutTwoColumnLeft) != 0   => "TwoColumnLeft",
            var f when (f & ViewerPreference.PageLayoutTwoColumnRight) != 0  => "TwoColumnRight",
            _                                                               => "Unknown"
        };

        // ----- Get current zoom factor (default viewer zoom) -----
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPath);
        float zoomFactor = pageEditor.Zoom; // 1.0 = 100%

        // ----- Log the information -----
        Console.WriteLine($"Viewer Preferences for '{inputPath}':");
        Console.WriteLine($"  Page Layout : {pageLayout}");
        Console.WriteLine($"  Zoom Factor : {zoomFactor * 100}%");
    }
}