using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Xml;

public class RetrieveViewerPreferences
{
    public static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "viewer_preferences.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF file not found: " + inputPdf);
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);
            int preferenceValue = editor.GetViewerPreference();

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("ViewerPreferences");
            xmlDoc.AppendChild(root);

            XmlElement valueElement = xmlDoc.CreateElement("PreferenceValue");
            valueElement.InnerText = preferenceValue.ToString();
            root.AppendChild(valueElement);

            // Add individual flags that are set
            if ((preferenceValue & ViewerPreference.HideMenubar) != 0)
            {
                XmlElement flag = xmlDoc.CreateElement("HideMenubar");
                flag.InnerText = "true";
                root.AppendChild(flag);
            }

            if ((preferenceValue & ViewerPreference.HideToolbar) != 0)
            {
                XmlElement flag = xmlDoc.CreateElement("HideToolbar");
                flag.InnerText = "true";
                root.AppendChild(flag);
            }

            if ((preferenceValue & ViewerPreference.PageModeUseNone) != 0)
            {
                XmlElement flag = xmlDoc.CreateElement("PageModeUseNone");
                flag.InnerText = "true";
                root.AppendChild(flag);
            }

            xmlDoc.Save(outputXml);
            Console.WriteLine("Viewer preferences exported to " + outputXml);
        }
    }
}
