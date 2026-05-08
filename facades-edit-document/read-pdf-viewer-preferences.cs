using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfContentEditor is a facade that implements IDisposable
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF file into the editor
            editor.BindPdf(inputPdf);

            // Retrieve the viewer preference flags as an integer
            int prefValue = editor.GetViewerPreference();

            Console.WriteLine($"Viewer Preference value: {prefValue}");

            // Decode and display each flag that is set
            if ((prefValue & ViewerPreference.CenterWindow) != 0)
                Console.WriteLine("CenterWindow");
            if ((prefValue & ViewerPreference.DirectionL2R) != 0)
                Console.WriteLine("DirectionL2R");
            if ((prefValue & ViewerPreference.DirectionR2L) != 0)
                Console.WriteLine("DirectionR2L");
            if ((prefValue & ViewerPreference.DisplayDocTitle) != 0)
                Console.WriteLine("DisplayDocTitle");
            if ((prefValue & ViewerPreference.DuplexFlipLongEdge) != 0)
                Console.WriteLine("DuplexFlipLongEdge");
            if ((prefValue & ViewerPreference.DuplexFlipShortEdge) != 0)
                Console.WriteLine("DuplexFlipShortEdge");
            if ((prefValue & ViewerPreference.FitWindow) != 0)
                Console.WriteLine("FitWindow");
            if ((prefValue & ViewerPreference.HideMenubar) != 0)
                Console.WriteLine("HideMenubar");
            if ((prefValue & ViewerPreference.HideToolbar) != 0)
                Console.WriteLine("HideToolbar");
            if ((prefValue & ViewerPreference.HideWindowUI) != 0)
                Console.WriteLine("HideWindowUI");
            if ((prefValue & ViewerPreference.NonFullScreenPageModeUseNone) != 0)
                Console.WriteLine("NonFullScreenPageModeUseNone");
            if ((prefValue & ViewerPreference.NonFullScreenPageModeUseOC) != 0)
                Console.WriteLine("NonFullScreenPageModeUseOC");
            if ((prefValue & ViewerPreference.NonFullScreenPageModeUseOutlines) != 0)
                Console.WriteLine("NonFullScreenPageModeUseOutlines");
            if ((prefValue & ViewerPreference.NonFullScreenPageModeUseThumbs) != 0)
                Console.WriteLine("NonFullScreenPageModeUseThumbs");
            if ((prefValue & ViewerPreference.PageLayoutOneColumn) != 0)
                Console.WriteLine("PageLayoutOneColumn");
            if ((prefValue & ViewerPreference.PageLayoutSinglePage) != 0)
                Console.WriteLine("PageLayoutSinglePage");
            if ((prefValue & ViewerPreference.PageLayoutTwoColumnLeft) != 0)
                Console.WriteLine("PageLayoutTwoColumnLeft");
            if ((prefValue & ViewerPreference.PageLayoutTwoColumnRight) != 0)
                Console.WriteLine("PageLayoutTwoColumnRight");
            if ((prefValue & ViewerPreference.PageModeFullScreen) != 0)
                Console.WriteLine("PageModeFullScreen");
            if ((prefValue & ViewerPreference.PageModeUseAttachment) != 0)
                Console.WriteLine("PageModeUseAttachment");
            if ((prefValue & ViewerPreference.PageModeUseNone) != 0)
                Console.WriteLine("PageModeUseNone");
            if ((prefValue & ViewerPreference.PageModeUseOC) != 0)
                Console.WriteLine("PageModeUseOC");
            if ((prefValue & ViewerPreference.PageModeUseOutlines) != 0)
                Console.WriteLine("PageModeUseOutlines");
            if ((prefValue & ViewerPreference.PageModeUseThumbs) != 0)
                Console.WriteLine("PageModeUseThumbs");
            if ((prefValue & ViewerPreference.PickTrayByPDFSize) != 0)
                Console.WriteLine("PickTrayByPDFSize");
            if ((prefValue & ViewerPreference.PrintScalingAppDefault) != 0)
                Console.WriteLine("PrintScalingAppDefault");
            if ((prefValue & ViewerPreference.PrintScalingNone) != 0)
                Console.WriteLine("PrintScalingNone");
            if ((prefValue & ViewerPreference.Simplex) != 0)
                Console.WriteLine("Simplex");
        }
    }
}