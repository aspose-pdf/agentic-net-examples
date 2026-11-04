using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "updated.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load XML and create a lookup of element name -> text value
        XDocument xmlDoc = XDocument.Load(xmlPath);
        var xmlValues = xmlDoc.Root
                              .Elements()
                              .ToDictionary(e => e.Name.LocalName, e => e.Value);

        // Load the PDF, update matching form fields, and save
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over all form fields in the PDF using the Forms API
            foreach (Field field in pdfDoc.Form.Fields)
            {
                string fieldName = field.PartialName;
                if (xmlValues.TryGetValue(fieldName, out string newValue))
                {
                    // Update the field with the value from XML
                    field.Value = newValue;
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF updated and saved to '{outputPath}'.");
    }
}
