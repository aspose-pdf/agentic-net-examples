using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXmlPath = "viewer_preferences.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF and retrieve viewer preferences using PdfContentEditor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);
            int prefValue = editor.GetViewerPreference();

            // Define known ViewerPreference flags and their names
            var knownFlags = new (int Flag, string Name)[]
            {
                (ViewerPreference.CenterWindow,               "CenterWindow"),
                (ViewerPreference.DirectionL2R,               "DirectionL2R"),
                (ViewerPreference.DirectionR2L,               "DirectionR2L"),
                (ViewerPreference.DisplayDocTitle,            "DisplayDocTitle"),
                (ViewerPreference.DuplexFlipLongEdge,         "DuplexFlipLongEdge"),
                (ViewerPreference.DuplexFlipShortEdge,        "DuplexFlipShortEdge"),
                (ViewerPreference.FitWindow,                  "FitWindow"),
                (ViewerPreference.HideMenubar,                "HideMenubar"),
                (ViewerPreference.HideToolbar,                "HideToolbar"),
                (ViewerPreference.HideWindowUI,               "HideWindowUI"),
                (ViewerPreference.NonFullScreenPageModeUseNone,   "NonFullScreenPageModeUseNone"),
                (ViewerPreference.NonFullScreenPageModeUseOC,     "NonFullScreenPageModeUseOC"),
                (ViewerPreference.NonFullScreenPageModeUseOutlines,"NonFullScreenPageModeUseOutlines"),
                (ViewerPreference.NonFullScreenPageModeUseThumbs, "NonFullScreenPageModeUseThumbs"),
                (ViewerPreference.PageLayoutOneColumn,        "PageLayoutOneColumn"),
                (ViewerPreference.PageLayoutSinglePage,       "PageLayoutSinglePage"),
                (ViewerPreference.PageLayoutTwoColumnLeft,    "PageLayoutTwoColumnLeft"),
                (ViewerPreference.PageLayoutTwoColumnRight,   "PageLayoutTwoColumnRight"),
                (ViewerPreference.PageModeFullScreen,         "PageModeFullScreen"),
                (ViewerPreference.PageModeUseAttachment,      "PageModeUseAttachment"),
                (ViewerPreference.PageModeUseNone,            "PageModeUseNone"),
                (ViewerPreference.PageModeUseOC,              "PageModeUseOC"),
                (ViewerPreference.PageModeUseOutlines,        "PageModeUseOutlines"),
                (ViewerPreference.PageModeUseThumbs,          "PageModeUseThumbs"),
                (ViewerPreference.PickTrayByPDFSize,          "PickTrayByPDFSize"),
                (ViewerPreference.PrintScalingAppDefault,    "PrintScalingAppDefault"),
                (ViewerPreference.PrintScalingNone,           "PrintScalingNone"),
                (ViewerPreference.Simplex,                    "Simplex")
            };

            // Build XML representation
            XElement root = new XElement("ViewerPreferences",
                new XAttribute("CombinedValue", prefValue));

            foreach (var (Flag, Name) in knownFlags)
            {
                if ((prefValue & Flag) != 0)
                {
                    root.Add(new XElement("Preference", new XAttribute("Name", Name)));
                }
            }

            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Viewer preferences saved to '{outputXmlPath}'.");
    }
}