using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputXmlPath = "viewer_prefs.xml"; // configuration file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Create the PdfContentEditor facade and bind the PDF
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            editor.BindPdf(inputPdfPath);

            // Retrieve the viewer preferences as an integer flag set
            int prefValue = editor.GetViewerPreference();

            // Serialize the preferences to an XML file
            using (XmlWriter writer = XmlWriter.Create(outputXmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("ViewerPreferences");

                // Store the raw integer value
                writer.WriteElementString("PreferenceValue", prefValue.ToString());

                // Optionally, store individual flags that are set
                writer.WriteStartElement("Flags");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.CenterWindow, "CenterWindow");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.DirectionL2R, "DirectionL2R");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.DirectionR2L, "DirectionR2L");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.DisplayDocTitle, "DisplayDocTitle");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.DuplexFlipLongEdge, "DuplexFlipLongEdge");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.DuplexFlipShortEdge, "DuplexFlipShortEdge");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.FitWindow, "FitWindow");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.HideMenubar, "HideMenubar");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.HideToolbar, "HideToolbar");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.HideWindowUI, "HideWindowUI");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.NonFullScreenPageModeUseNone, "NonFullScreenPageModeUseNone");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.NonFullScreenPageModeUseOC, "NonFullScreenPageModeUseOC");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.NonFullScreenPageModeUseOutlines, "NonFullScreenPageModeUseOutlines");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.NonFullScreenPageModeUseThumbs, "NonFullScreenPageModeUseThumbs");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageLayoutOneColumn, "PageLayoutOneColumn");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageLayoutSinglePage, "PageLayoutSinglePage");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageLayoutTwoColumnLeft, "PageLayoutTwoColumnLeft");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageLayoutTwoColumnRight, "PageLayoutTwoColumnRight");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageModeFullScreen, "PageModeFullScreen");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageModeUseAttachment, "PageModeUseAttachment");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageModeUseNone, "PageModeUseNone");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageModeUseOC, "PageModeUseOC");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageModeUseOutlines, "PageModeUseOutlines");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PageModeUseThumbs, "PageModeUseThumbs");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PickTrayByPDFSize, "PickTrayByPDFSize");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PrintScalingAppDefault, "PrintScalingAppDefault");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.PrintScalingNone, "PrintScalingNone");
                WriteFlagIfSet(writer, prefValue, ViewerPreference.Simplex, "Simplex");
                writer.WriteEndElement(); // Flags

                writer.WriteEndElement(); // ViewerPreferences
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Viewer preferences saved to '{outputXmlPath}'.");
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }
    }

    // Helper to write a flag element only if the corresponding bit is set
    private static void WriteFlagIfSet(XmlWriter writer, int prefValue, int flag, string name)
    {
        if ((prefValue & flag) != 0)
        {
            writer.WriteElementString(name, "true");
        }
    }
}