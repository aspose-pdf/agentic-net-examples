using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "viewer_preferences.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and retrieve viewer preference flags
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        int prefValue = editor.GetViewerPreference();

        // Build an XML document describing the active flags
        XElement root = new XElement("ViewerPreferences",
            new XAttribute("Value", prefValue));

        void AddFlag(int flag, string name)
        {
            if ((prefValue & flag) != 0)
                root.Add(new XElement("Flag", name));
        }

        AddFlag(ViewerPreference.CenterWindow, "CenterWindow");
        AddFlag(ViewerPreference.DirectionL2R, "DirectionL2R");
        AddFlag(ViewerPreference.DirectionR2L, "DirectionR2L");
        AddFlag(ViewerPreference.DisplayDocTitle, "DisplayDocTitle");
        AddFlag(ViewerPreference.DuplexFlipLongEdge, "DuplexFlipLongEdge");
        AddFlag(ViewerPreference.DuplexFlipShortEdge, "DuplexFlipShortEdge");
        AddFlag(ViewerPreference.FitWindow, "FitWindow");
        AddFlag(ViewerPreference.HideMenubar, "HideMenubar");
        AddFlag(ViewerPreference.HideToolbar, "HideToolbar");
        AddFlag(ViewerPreference.HideWindowUI, "HideWindowUI");
        AddFlag(ViewerPreference.NonFullScreenPageModeUseNone, "NonFullScreenPageModeUseNone");
        AddFlag(ViewerPreference.NonFullScreenPageModeUseOC, "NonFullScreenPageModeUseOC");
        AddFlag(ViewerPreference.NonFullScreenPageModeUseOutlines, "NonFullScreenPageModeUseOutlines");
        AddFlag(ViewerPreference.NonFullScreenPageModeUseThumbs, "NonFullScreenPageModeUseThumbs");
        AddFlag(ViewerPreference.PageLayoutOneColumn, "PageLayoutOneColumn");
        AddFlag(ViewerPreference.PageLayoutSinglePage, "PageLayoutSinglePage");
        AddFlag(ViewerPreference.PageLayoutTwoColumnLeft, "PageLayoutTwoColumnLeft");
        AddFlag(ViewerPreference.PageLayoutTwoColumnRight, "PageLayoutTwoColumnRight");
        AddFlag(ViewerPreference.PageModeFullScreen, "PageModeFullScreen");
        AddFlag(ViewerPreference.PageModeUseAttachment, "PageModeUseAttachment");
        AddFlag(ViewerPreference.PageModeUseNone, "PageModeUseNone");
        AddFlag(ViewerPreference.PageModeUseOC, "PageModeUseOC");
        AddFlag(ViewerPreference.PageModeUseOutlines, "PageModeUseOutlines");
        AddFlag(ViewerPreference.PageModeUseThumbs, "PageModeUseThumbs");
        AddFlag(ViewerPreference.PickTrayByPDFSize, "PickTrayByPDFSize");
        AddFlag(ViewerPreference.PrintScalingAppDefault, "PrintScalingAppDefault");
        AddFlag(ViewerPreference.PrintScalingNone, "PrintScalingNone");
        AddFlag(ViewerPreference.Simplex, "Simplex");

        // Save the XML representation to a file
        XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        xmlDoc.Save(outputXml);

        Console.WriteLine($"Viewer preferences exported to '{outputXml}'.");
    }
}