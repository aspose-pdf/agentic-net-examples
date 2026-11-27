using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for Field and concrete form field types

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // Source PDF containing form fields
        const string outputXmlPath = "form_fields.xml"; // Destination XML file

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create the root element for the XML
            XElement root = new XElement("FormFields");

            // Guard against PDFs without an AcroForm or without fields
            var form = pdfDoc.Form;
            if (form != null && form.Fields != null && form.Fields.Length > 0)
            {
                // Iterate over all form fields in the document
                foreach (Field field in form.Fields)
                {
                    // FullName uniquely identifies the field; Value may be null
                    string fullName = field.FullName ?? string.Empty;
                    string value    = field.Value?.ToString() ?? string.Empty;

                    // Build an XML element for the current field
                    XElement fieldElement = new XElement("Field",
                        new XAttribute("FullName", fullName),
                        new XAttribute("Value", value));

                    root.Add(fieldElement);
                }
            }

            // Assemble the final XML document
            XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);

            // Save the XML to the specified path
            xmlDoc.Save(outputXmlPath);
        }

        Console.WriteLine($"Form field data exported to '{outputXmlPath}'.");
    }
}
