using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string xmlOutput = "viewer_prefs.xml"; // destination XML

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF and retrieve viewer preferences using PdfContentEditor (Facade API)
        Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor();
        editor.BindPdf(pdfPath);
        int prefValue = editor.GetViewerPreference();

        // Build XML representation of the flags
        XElement root = new XElement("ViewerPreferences",
            new XAttribute("CombinedValue", prefValue));

        // Reflect over ViewerPreference constants to decode individual flags
        var prefFields = typeof(Aspose.Pdf.Facades.ViewerPreference)
                         .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                         .Where(f => f.IsLiteral && !f.IsInitOnly);

        foreach (var field in prefFields)
        {
            int flag = (int)field.GetRawConstantValue();
            bool isSet = (prefValue & flag) != 0;
            root.Add(new XElement("Preference",
                        new XAttribute("Name", field.Name),
                        new XAttribute("Enabled", isSet)));
        }

        // Save the XML file
        XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        doc.Save(xmlOutput);

        Console.WriteLine($"Viewer preferences exported to '{xmlOutput}'.");
    }
}