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
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "form_fields.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document actually contains a form.
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Build an XML representation of the form fields.
            var xml = new XDocument(
                new XElement("FormFields",
                    pdfDoc.Form.Fields.Select(field =>
                        new XElement("Field",
                            new XAttribute("Name", field.Name ?? field.FullName ?? string.Empty),
                            new XAttribute("Value", field.Value?.ToString() ?? string.Empty)
                        )
                    )
                )
            );

            // Write the XML to the output file via a FileStream.
            using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            {
                xml.Save(xmlStream);
            }

            Console.WriteLine($"Form fields exported to XML: {outputXmlPath}");
        }
    }
}
