using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "formdata.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Serialize the current form fields to an XML file
                ExportFormDataToXml(pdfDoc, outputXmlPath);
                Console.WriteLine($"Form data successfully exported to '{outputXmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }

    /// <summary>
    /// Exports the values of all form fields in the given PDF document to an XML file.
    /// The XML structure is:
    /// <FormData>
    ///   <Field name="fieldName" value="fieldValue" />
    ///   ...
    /// </FormData>
    /// </summary>
    private static void ExportFormDataToXml(Document pdfDocument, string xmlFilePath)
    {
        // Ensure the document actually contains a form
        if (pdfDocument?.Form?.Fields == null)
        {
            // Create an empty XML file to indicate no form data
            new XDocument(new XElement("FormData")).Save(xmlFilePath);
            return;
        }

        var root = new XElement("FormData");
        foreach (Field field in pdfDocument.Form.Fields)
        {
            // Some fields may have complex values (e.g., list boxes). Use ToString() for a simple representation.
            string fieldName = field?.Name ?? field?.FullName ?? string.Empty;
            string fieldValue = field?.Value?.ToString() ?? string.Empty;

            var fieldElement = new XElement("Field",
                new XAttribute("name", fieldName),
                new XAttribute("value", fieldValue));
            root.Add(fieldElement);
        }

        var doc = new XDocument(root);
        doc.Save(xmlFilePath);
    }
}
