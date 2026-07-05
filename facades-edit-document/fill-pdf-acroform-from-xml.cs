using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";
        const string inputXmlPath = "data.xml";
        const string outputPdfPath = "filled.pdf";

        // Verify that the required files exist before proceeding.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF template not found: '{inputPdfPath}'.");
            return;
        }

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML data file not found: '{inputXmlPath}'.");
            return;
        }

        // Load the XML document once.
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(inputXmlPath);

        // Initialize the Form facade with the source PDF.
        using (Form form = new Form(inputPdfPath))
        {
            // Iterate over all AcroForm field names.
            foreach (string fieldName in form.FieldNames)
            {
                // Locate the XML node that matches the field name.
                XmlNode? node = xmlDoc.SelectSingleNode($"//{fieldName}");
                if (node != null)
                {
                    // Guard against a possible null InnerText.
                    string value = node.InnerText ?? string.Empty;
                    form.FillField(fieldName, value);
                }
            }

            // Save the updated PDF to the desired output file.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}
