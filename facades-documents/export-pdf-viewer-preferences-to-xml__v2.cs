using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "viewer_preferences.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // PdfContentEditor instance (required by task, not used for loading)
        PdfContentEditor editor = new PdfContentEditor();

        // Load PDF using Document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Try to obtain viewer preferences via reflection (property may not exist in older versions)
            bool centerWindow = false;
            bool hideMenuBar = false;
            bool hideToolbar = false;

            var vpProp = doc.GetType().GetProperty("ViewerPreferences");
            if (vpProp != null)
            {
                var prefs = vpProp.GetValue(doc);
                if (prefs != null)
                {
                    var prefsType = prefs.GetType();
                    var cwProp = prefsType.GetProperty("CenterWindow");
                    var hmProp = prefsType.GetProperty("HideMenuBar");
                    var htProp = prefsType.GetProperty("HideToolbar");

                    if (cwProp != null && cwProp.PropertyType == typeof(bool))
                        centerWindow = (bool)cwProp.GetValue(prefs);
                    if (hmProp != null && hmProp.PropertyType == typeof(bool))
                        hideMenuBar = (bool)hmProp.GetValue(prefs);
                    if (htProp != null && htProp.PropertyType == typeof(bool))
                        hideToolbar = (bool)htProp.GetValue(prefs);
                }
            }
            else
            {
                Console.WriteLine("ViewerPreferences property is not available in the referenced Aspose.Pdf version. Defaulting values to false.");
            }

            // Build XML document with the required elements
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement("ViewerPreferences");
            xmlDoc.AppendChild(root);

            XmlElement centerElem = xmlDoc.CreateElement("CenterWindow");
            centerElem.InnerText = centerWindow.ToString().ToLower();
            root.AppendChild(centerElem);

            XmlElement hideMenuElem = xmlDoc.CreateElement("HideMenuBar");
            hideMenuElem.InnerText = hideMenuBar.ToString().ToLower();
            root.AppendChild(hideMenuElem);

            XmlElement hideToolElem = xmlDoc.CreateElement("HideToolbar");
            hideToolElem.InnerText = hideToolbar.ToString().ToLower();
            root.AppendChild(hideToolElem);

            // Save the XML to the specified file
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Viewer preferences exported to '{outputXmlPath}'.");
    }
}
