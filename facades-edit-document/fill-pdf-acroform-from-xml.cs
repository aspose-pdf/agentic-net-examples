using System;
using System.IO;
using System.Xml;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // PDF with AcroForm fields
        const string inputXmlPath  = "data.xml";       // XML source data
        const string outputPdfPath = "filled.pdf";     // Resulting PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {inputXmlPath}");
            return;
        }

        // Load XML document once
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(inputXmlPath);

        // Initialize the Form facade on the source PDF
        using (Form form = new Form(inputPdfPath))
        {
            // Iterate over all AcroForm field names
            foreach (string fieldName in form.FieldNames)
            {
                // Build an XPath that selects an element whose name matches the field name
                // Adjust the XPath as needed for your XML structure
                string xpath = $"//*[local-name() = '{fieldName}']";

                XmlNode node = xmlDoc.SelectSingleNode(xpath);
                if (node != null)
                {
                    string value = node.InnerText ?? string.Empty;
                    // Fill the field with the extracted value
                    form.FillField(fieldName, value);
                }
                else
                {
                    Console.WriteLine($"No XML value found for field '{fieldName}'.");
                }
            }

            // Save the filled PDF to the desired output file
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}