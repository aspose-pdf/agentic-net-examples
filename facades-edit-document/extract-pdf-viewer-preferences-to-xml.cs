using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string configPath = "viewerPrefs.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF and retrieve its viewer preferences
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);
        int prefValue = editor.GetViewerPreference();

        // Serialize the preferences to an XML configuration file
        using (XmlWriter writer = XmlWriter.Create(configPath, new XmlWriterSettings { Indent = true }))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("ViewerPreferences");

            // Store the raw integer value
            writer.WriteElementString("PreferenceValue", prefValue.ToString());

            // Store individual flags for readability
            WriteFlag(writer, "HideMenubar",               Aspose.Pdf.Facades.ViewerPreference.HideMenubar,               prefValue);
            WriteFlag(writer, "HideToolbar",               Aspose.Pdf.Facades.ViewerPreference.HideToolbar,               prefValue);
            WriteFlag(writer, "HideWindowUI",              Aspose.Pdf.Facades.ViewerPreference.HideWindowUI,              prefValue);
            WriteFlag(writer, "FitWindow",                 Aspose.Pdf.Facades.ViewerPreference.FitWindow,                 prefValue);
            WriteFlag(writer, "CenterWindow",              Aspose.Pdf.Facades.ViewerPreference.CenterWindow,              prefValue);
            WriteFlag(writer, "DisplayDocTitle",           Aspose.Pdf.Facades.ViewerPreference.DisplayDocTitle,           prefValue);
            WriteFlag(writer, "PageModeUseNone",           Aspose.Pdf.Facades.ViewerPreference.PageModeUseNone,           prefValue);
            WriteFlag(writer, "PageModeUseOutlines",       Aspose.Pdf.Facades.ViewerPreference.PageModeUseOutlines,       prefValue);
            WriteFlag(writer, "PageModeUseThumbs",         Aspose.Pdf.Facades.ViewerPreference.PageModeUseThumbs,         prefValue);
            WriteFlag(writer, "PageModeFullScreen",        Aspose.Pdf.Facades.ViewerPreference.PageModeFullScreen,        prefValue);
            WriteFlag(writer, "PageLayoutSinglePage",      Aspose.Pdf.Facades.ViewerPreference.PageLayoutSinglePage,      prefValue);
            WriteFlag(writer, "PageLayoutOneColumn",       Aspose.Pdf.Facades.ViewerPreference.PageLayoutOneColumn,       prefValue);
            WriteFlag(writer, "PageLayoutTwoColumnLeft",   Aspose.Pdf.Facades.ViewerPreference.PageLayoutTwoColumnLeft,   prefValue);
            WriteFlag(writer, "PageLayoutTwoColumnRight",  Aspose.Pdf.Facades.ViewerPreference.PageLayoutTwoColumnRight,  prefValue);
            // Add additional flags as needed

            writer.WriteEndElement(); // ViewerPreferences
            writer.WriteEndDocument();
        }

        Console.WriteLine($"Viewer preferences saved to '{configPath}'.");
    }

    // Helper to write each flag as an XML element
    static void WriteFlag(XmlWriter writer, string name, int flag, int combinedValue)
    {
        bool isEnabled = (combinedValue & flag) != 0;
        writer.WriteStartElement("Preference");
        writer.WriteAttributeString("Name", name);
        writer.WriteAttributeString("Enabled", isEnabled.ToString().ToLower());
        writer.WriteEndElement();
    }
}