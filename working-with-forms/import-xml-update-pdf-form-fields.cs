using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // PDF with form fields
        const string xmlPath = "data.xml";           // XML containing a subset of field values
        const string outputPath = "updated.pdf";     // Resulting PDF

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

        // Load the PDF document (lifecycle: create -> load -> save)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML data
            XDocument xmlDoc = XDocument.Load(xmlPath);

            // Guard against a missing root element
            if (xmlDoc.Root == null)
            {
                Console.Error.WriteLine("XML does not contain a root element.");
                return;
            }

            // Iterate over each element in the XML.
            // Assume each element name corresponds to a form field name.
            foreach (XElement element in xmlDoc.Root.Elements())
            {
                string fieldName = element.Name.LocalName;
                string fieldValue = element.Value;

                // Check if the PDF contains a field with this name
                if (pdfDoc.Form.HasField(fieldName))
                {
                    // Retrieve the field as a generic Form Field and set its value.
                    // The indexer returns a WidgetAnnotation; cast it to Field to access the Value property.
                    if (pdfDoc.Form[fieldName] is Field field)
                    {
                        field.Value = fieldValue;
                    }
                }
            }

            // Save the updated PDF (lifecycle: save)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF updated and saved to '{outputPath}'.");
    }
}
