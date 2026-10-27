using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";        // source PDF with AcroForm
        const string xmlPath = "updatedData.xml";  // XML containing form field values
        const string outputPath = "output.pdf";    // PDF after data import

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

        try
        {
            // Load the PDF document (contains the AcroForm)
            Document doc = new Document(pdfPath);

            // Load the XML that holds the form data.
            // Expected structure:
            // <FormData>
            //   <Field name="FieldName">FieldValue</Field>
            //   ...
            // </FormData>
            XDocument xmlDoc = XDocument.Load(xmlPath);
            if (xmlDoc.Root == null)
            {
                Console.Error.WriteLine("XML does not contain a root element.");
                return;
            }

            // Iterate over each <Field> element and push the value into the matching PDF field.
            foreach (XElement fieldElement in xmlDoc.Root.Elements("Field"))
            {
                XAttribute nameAttr = fieldElement.Attribute("name");
                if (nameAttr == null)
                {
                    // skip malformed entries
                    continue;
                }

                string fieldName = nameAttr.Value;
                string fieldValue = fieldElement.Value ?? string.Empty;

                // Locate the field in the PDF form collection. The Form indexer returns a WidgetAnnotation,
                // so we need to cast it to Aspose.Pdf.Forms.Field before accessing the Value property.
                Field? pdfField = doc.Form[fieldName] as Field;
                if (pdfField != null)
                {
                    pdfField.Value = fieldValue;
                }
                else
                {
                    Console.Error.WriteLine($"Warning: field '{fieldName}' not found in PDF.");
                }
            }

            // Save the updated PDF.
            doc.Save(outputPath);
            Console.WriteLine($"AcroForm data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
