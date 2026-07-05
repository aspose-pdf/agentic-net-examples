using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "viewerPreferences.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF using PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);

        // Retrieve the combined viewer preference flags as an integer
        int prefValue = editor.GetViewerPreference();

        // Build an XML document that lists each known flag and whether it is set
        XDocument xmlDoc = new XDocument(
            new XElement("ViewerPreferences",
                new XElement("PreferenceValue", prefValue),
                new XElement("Flags",
                    CreateFlagElement("CenterWindow", ViewerPreference.CenterWindow, prefValue),
                    CreateFlagElement("DirectionL2R", ViewerPreference.DirectionL2R, prefValue),
                    CreateFlagElement("DirectionR2L", ViewerPreference.DirectionR2L, prefValue),
                    CreateFlagElement("DisplayDocTitle", ViewerPreference.DisplayDocTitle, prefValue),
                    CreateFlagElement("DuplexFlipLongEdge", ViewerPreference.DuplexFlipLongEdge, prefValue),
                    CreateFlagElement("DuplexFlipShortEdge", ViewerPreference.DuplexFlipShortEdge, prefValue),
                    CreateFlagElement("FitWindow", ViewerPreference.FitWindow, prefValue),
                    CreateFlagElement("HideMenubar", ViewerPreference.HideMenubar, prefValue),
                    CreateFlagElement("HideToolbar", ViewerPreference.HideToolbar, prefValue),
                    CreateFlagElement("HideWindowUI", ViewerPreference.HideWindowUI, prefValue),
                    CreateFlagElement("NonFullScreenPageModeUseNone", ViewerPreference.NonFullScreenPageModeUseNone, prefValue),
                    CreateFlagElement("NonFullScreenPageModeUseOC", ViewerPreference.NonFullScreenPageModeUseOC, prefValue),
                    CreateFlagElement("NonFullScreenPageModeUseOutlines", ViewerPreference.NonFullScreenPageModeUseOutlines, prefValue),
                    CreateFlagElement("NonFullScreenPageModeUseThumbs", ViewerPreference.NonFullScreenPageModeUseThumbs, prefValue),
                    CreateFlagElement("PageLayoutOneColumn", ViewerPreference.PageLayoutOneColumn, prefValue),
                    CreateFlagElement("PageLayoutSinglePage", ViewerPreference.PageLayoutSinglePage, prefValue),
                    CreateFlagElement("PageLayoutTwoColumnLeft", ViewerPreference.PageLayoutTwoColumnLeft, prefValue),
                    CreateFlagElement("PageLayoutTwoColumnRight", ViewerPreference.PageLayoutTwoColumnRight, prefValue),
                    CreateFlagElement("PageModeFullScreen", ViewerPreference.PageModeFullScreen, prefValue),
                    CreateFlagElement("PageModeUseAttachment", ViewerPreference.PageModeUseAttachment, prefValue),
                    CreateFlagElement("PageModeUseNone", ViewerPreference.PageModeUseNone, prefValue),
                    CreateFlagElement("PageModeUseOC", ViewerPreference.PageModeUseOC, prefValue),
                    CreateFlagElement("PageModeUseOutlines", ViewerPreference.PageModeUseOutlines, prefValue),
                    CreateFlagElement("PageModeUseThumbs", ViewerPreference.PageModeUseThumbs, prefValue)
                )
            )
        );

        // Save the XML for legacy integration
        xmlDoc.Save(xmlPath);
        Console.WriteLine($"Viewer preferences exported to {xmlPath}");
    }

    // Adjusted to accept an int flag value because Aspose.Pdf.Facades.ViewerPreference defines flags as int constants.
    private static XElement CreateFlagElement(string name, int flag, int combinedValue)
    {
        bool isSet = (combinedValue & flag) == flag;
        return new XElement(name, isSet);
    }
}
