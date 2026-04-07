using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";      // PDF with form fields
        const string xmlPath = "data.xml";          // XML containing a subset of field values
        const string outputPath = "filled.pdf";     // Resulting PDF

        // Verify input files exist
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

        // Load the PDF document (lifecycle rule: use using block)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the XML data (simple flat structure: <FieldName>value</FieldName>)
            XDocument xmlDoc = XDocument.Load(xmlPath);

            // Iterate over each child element of the root element
            foreach (XElement element in xmlDoc.Root.Elements())
            {
                string fieldName = element.Name.LocalName; // XML element name matches PDF field name
                string fieldValue = element.Value;         // Text content of the element

                // Update only if the PDF contains a matching field
                if (pdfDoc.Form.HasField(fieldName))
                {
                    // The Form collection returns a WidgetAnnotation; cast to Field to access Value
                    if (pdfDoc.Form[fieldName] is Field pdfField)
                    {
                        pdfField.Value = fieldValue;
                    }
                }
            }

            // Save the updated PDF (lifecycle rule: use Save method)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
