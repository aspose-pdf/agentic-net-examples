using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "formdata.xml";
        const string customRoot = "MyCustomRoot";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Build the XML manually because Aspose.Pdf.Form does not expose ExportToXml with a custom root.
                XDocument xDoc = new XDocument(new XElement(customRoot));

                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        string fieldName = field.Name ?? field.FullName ?? "UnnamedField";
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        // Each field is represented as a <Field Name="..." Value="..."/> element.
                        XElement fieldElement = new XElement("Field",
                            new XAttribute("Name", fieldName),
                            new XAttribute("Value", fieldValue));

                        xDoc.Root.Add(fieldElement);
                    }
                }

                xDoc.Save(outputPath);
            }

            Console.WriteLine($"Form data exported to '{outputPath}' with root element '{customRoot}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
