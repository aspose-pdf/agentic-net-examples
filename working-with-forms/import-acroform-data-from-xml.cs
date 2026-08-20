using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the XML data file and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string xmlDataPath    = "updatedData.xml";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(sourcePdfPath);

        // Load the XML data (expected format: <Fields><Field name="FieldName">Value</Field>...</Fields>)
        XDocument xmlDoc = XDocument.Load(xmlDataPath);
        foreach (XElement fieldElement in xmlDoc.Descendants("Field"))
        {
            XAttribute nameAttr = fieldElement.Attribute("name");
            if (nameAttr == null) continue; // skip malformed entries
            string fieldName = nameAttr.Value;
            string fieldValue = fieldElement.Value ?? string.Empty;

            // The Form indexer returns a WidgetAnnotation; cast it to Field before using.
            Field? pdfField = pdfDocument.Form[fieldName] as Field;
            if (pdfField != null)
            {
                pdfField.Value = fieldValue;
            }
            else
            {
                Console.Error.WriteLine($"Warning: field '{fieldName}' not found in the PDF form.");
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"AcroForm data imported and saved to '{outputPdfPath}'.");
    }
}
