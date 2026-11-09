using System;
using System.IO;
using System.Xml.Serialization;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputXml = "viewer_preferences.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document. ViewerPreferences is a member of Document in newer
        // Aspose.Pdf versions. To keep the code compatible with older versions we
        // access it via dynamic and handle the possible RuntimeBinderException.
        Document pdfDocument = new Document(inputPath);
        object viewerPrefs = null;
        try
        {
            // "dynamic" bypasses compile‑time checks for the ViewerPreferences property.
            dynamic dynDoc = pdfDocument;
            viewerPrefs = dynDoc.ViewerPreferences; // may throw at runtime if not present
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
        }

        if (viewerPrefs != null)
        {
            // Serialize the actual runtime type of the ViewerPreferences object.
            XmlSerializer serializer = new XmlSerializer(viewerPrefs.GetType());
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, viewerPrefs);
            }
            Console.WriteLine($"Viewer preferences have been saved to '{outputXml}'.");
        }
        else
        {
            // Create an empty XML file to indicate that no preferences were retrieved.
            File.WriteAllText(outputXml, "<ViewerPreferences />");
            Console.WriteLine($"No ViewerPreferences available; empty placeholder written to '{outputXml}'.");
        }
    }
}
