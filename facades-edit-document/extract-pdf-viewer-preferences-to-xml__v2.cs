using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        // Bind the PDF and retrieve the viewer preference flags
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);
        int prefValue = editor.GetViewerPreference();

        // Build an XML document describing the preferences
        XDocument xmlDoc = new XDocument(
            new XElement("ViewerPreferences",
                new XAttribute("CombinedValue", prefValue),
                new XElement("SetFlags", GetSetFlagElements(prefValue))
            )
        );

        // Save the XML to the specified file
        xmlDoc.Save(outputXml);
        Console.WriteLine($"Viewer preferences have been saved to '{outputXml}'.");
    }

    // Generates XML elements for each flag that is set in the combined preference value
    static IEnumerable<XElement> GetSetFlagElements(int combinedValue)
    {
        var prefType = typeof(ViewerPreference);
        var flagFields = prefType.GetFields(
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Static);

        foreach (var field in flagFields)
        {
            if (field.FieldType != typeof(int))
                continue;

            int flag = (int)field.GetValue(null);
            if ((combinedValue & flag) != 0)
            {
                yield return new XElement("Flag", field.Name);
            }
        }
    }
}