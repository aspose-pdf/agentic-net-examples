using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Aspose.Pdf; // Document class

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "viewerPreferences.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPdf);

            // Try to obtain the ViewerPreferences property via reflection.
            // This avoids compile‑time errors when the property is not present in the referenced Aspose.Pdf version.
            PropertyInfo viewerPrefProp = pdfDoc.GetType().GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
            if (viewerPrefProp == null)
            {
                Console.WriteLine("The current Aspose.Pdf library version does not expose a ViewerPreferences property.");
                return;
            }

            object viewerPreferences = viewerPrefProp.GetValue(pdfDoc);
            if (viewerPreferences == null)
            {
                Console.WriteLine("ViewerPreferences property exists but returned null.");
                return;
            }

            // Serialize the retrieved preferences object to XML.
            // The actual runtime type is used for the XmlSerializer constructor.
            XmlSerializer serializer = new XmlSerializer(viewerPreferences.GetType());
            using (FileStream fs = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, viewerPreferences);
            }

            Console.WriteLine($"Viewer preferences saved to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
