using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string configXml = "viewer_preferences.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and retrieve its viewer preferences
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        int prefValue = editor.GetViewerPreference();

        // Build an XML document describing the preferences
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root = xmlDoc.CreateElement("ViewerPreferences");
        xmlDoc.AppendChild(root);
        root.SetAttribute("CombinedValue", prefValue.ToString());

        // Helper to add a flag element when the corresponding bit is set
        void AddFlag(string name, int flag)
        {
            if ((prefValue & flag) != 0)
            {
                XmlElement flagElem = xmlDoc.CreateElement("Flag");
                flagElem.SetAttribute("Name", name);
                flagElem.SetAttribute("Enabled", "true");
                root.AppendChild(flagElem);
            }
        }

        // Add individual flags (add more as needed)
        AddFlag("HideMenubar", ViewerPreference.HideMenubar);
        AddFlag("HideToolbar", ViewerPreference.HideToolbar);
        AddFlag("HideWindowUI", ViewerPreference.HideWindowUI);
        AddFlag("FitWindow", ViewerPreference.FitWindow);
        AddFlag("CenterWindow", ViewerPreference.CenterWindow);
        AddFlag("DisplayDocTitle", ViewerPreference.DisplayDocTitle);
        AddFlag("PageModeUseNone", ViewerPreference.PageModeUseNone);
        AddFlag("PageModeUseOutlines", ViewerPreference.PageModeUseOutlines);
        AddFlag("PageModeUseThumbs", ViewerPreference.PageModeUseThumbs);
        AddFlag("PageModeFullScreen", ViewerPreference.PageModeFullScreen);
        AddFlag("PageLayoutSinglePage", ViewerPreference.PageLayoutSinglePage);
        AddFlag("PageLayoutOneColumn", ViewerPreference.PageLayoutOneColumn);
        AddFlag("DirectionL2R", ViewerPreference.DirectionL2R);
        AddFlag("DirectionR2L", ViewerPreference.DirectionR2L);
        // ...additional flags can be added here...

        // Save the XML configuration file
        xmlDoc.Save(configXml);
        Console.WriteLine($"Viewer preferences have been saved to '{configXml}'.");
    }
}