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
        const string outputPath = "filled.pdf";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Import field values from the transformed XML.
            // The core Aspose.Pdf API does not provide a direct ImportXml method on Document.Form,
            // so we parse the XML ourselves and assign values to the matching form fields.
            ImportFormDataFromXml(pdfDoc, xmlPath);

            // Save the updated PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported data saved to '{outputPath}'.");
    }

    /// <summary>
    /// Reads a simple XML representation of form data and populates the corresponding fields
    /// in the provided PDF document.
    /// Expected XML format (compatible with Aspose export):
    /// <form>
    ///   <field name="FieldName1">Value1</field>
    ///   <field name="FieldName2">Value2</field>
    ///   ...
    /// </form>
    /// </summary>
    private static void ImportFormDataFromXml(Document pdfDoc, string xmlPath)
    {
        if (pdfDoc?.Form?.Fields == null)
            return;

        XDocument xDoc = XDocument.Load(xmlPath);
        // The XML may contain a root element named "form" or "FormData" – we handle both.
        var fieldElements = xDoc.Descendants("field");
        foreach (var elem in fieldElements)
        {
            string fieldName = (string)elem.Attribute("name");
            if (string.IsNullOrEmpty(fieldName))
                continue;

            string fieldValue = elem.Value ?? string.Empty;
            // Find the field in the PDF (case‑sensitive match on Name).
            var pdfField = pdfDoc.Form.Fields.FirstOrDefault(f => f.Name == fieldName);
            if (pdfField != null)
            {
                // Most field types expose a Value property that accepts an object.
                pdfField.Value = fieldValue;
            }
        }
    }
}
