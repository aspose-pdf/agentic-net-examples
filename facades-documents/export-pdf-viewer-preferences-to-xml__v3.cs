using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

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

        // Load the PDF document using the high‑level Aspose.Pdf API.
        Document pdfDoc = new Document(inputPdfPath);

        // ViewerPreferences were introduced in later versions of Aspose.Pdf.
        // To keep the code compatible with older versions we access the property
        // via dynamic and handle the possible RuntimeBinderException.
        dynamic dynDoc = pdfDoc;
        dynamic prefs;
        try
        {
            prefs = dynDoc.ViewerPreferences;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
            return;
        }

        // Build an XML document that lists each preference and its current value.
        // All members are accessed dynamically – if a particular property does not exist
        // the runtime will throw, so we guard each access with a helper.
        XDocument xmlDoc = new XDocument(
            new XElement("ViewerPreferences",
                CreateElement("HideToolbar", GetProperty(prefs, "HideToolbar")),
                CreateElement("HideMenubar", GetProperty(prefs, "HideMenubar")),
                CreateElement("HideWindowUI", GetProperty(prefs, "HideWindowUI")),
                CreateElement("FitWindow", GetProperty(prefs, "FitWindow")),
                CreateElement("CenterWindow", GetProperty(prefs, "CenterWindow")),
                CreateElement("DisplayDocTitle", GetProperty(prefs, "DisplayDocTitle")),
                CreateElement("NonFullScreenPageMode", GetProperty(prefs, "NonFullScreenPageMode")?.ToString() ?? "None"),
                CreateElement("Direction", GetProperty(prefs, "Direction")?.ToString() ?? "None"),
                CreateElement("PrintScaling", GetProperty(prefs, "PrintScaling")?.ToString() ?? "None"),
                CreateElement("Duplex", GetProperty(prefs, "Duplex")?.ToString() ?? "None"),
                CreateElement("PickTrayByPDFSize", GetProperty(prefs, "PickTrayByPDFSize")),
                CreateElement("PrintPageRange", GetProperty(prefs, "PrintPageRange")),
                CreateElement("PageLayout", GetProperty(prefs, "PageLayout")?.ToString() ?? "None"),
                CreateElement("PageMode", GetProperty(prefs, "PageMode")?.ToString() ?? "None")
            )
        );

        // Save the XML document.
        xmlDoc.Save(outputXmlPath);
        Console.WriteLine($"Viewer preferences exported to '{outputXmlPath}'.");
    }

    // Helper that safely reads a property from a dynamic object.
    private static object GetProperty(dynamic obj, string name)
    {
        try
        {
            return ((object)obj).GetType().GetProperty(name)?.GetValue(obj, null) ??
                   ((IDictionary<string, object>)obj)[name]; // fallback for ExpandoObject‑like objects
        }
        catch
        {
            // Property not present – return null so the XML can use a default value.
            return null;
        }
    }

    // Helper to create an XElement with a string representation of the value (or empty string).
    private static XElement CreateElement(string elementName, object value)
    {
        return new XElement(elementName, value?.ToString() ?? string.Empty);
    }
}
