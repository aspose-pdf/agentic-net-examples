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
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "target_filled.pdf";
        const string xmlDataPath   = "formData.xml";

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        try
        {
            // Export form data from the source PDF to an XML file
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                if (sourceDoc.Form == null || sourceDoc.Form.Fields.Count() == 0)
                {
                    Console.WriteLine("Source PDF contains no form fields to export.");
                }
                else
                {
                    ExportFormDataToXml(sourceDoc, xmlDataPath);
                    Console.WriteLine($"Form data exported to XML: {xmlDataPath}");
                }
            }

            // Import the XML form data into the target PDF and save the result
            using (Document targetDoc = new Document(targetPdfPath))
            {
                if (targetDoc.Form == null || targetDoc.Form.Fields.Count() == 0)
                {
                    Console.WriteLine("Target PDF contains no form fields to import into.");
                }
                else
                {
                    ImportFormDataFromXml(targetDoc, xmlDataPath);
                    Console.WriteLine("Form data imported into target PDF.");
                }

                // Save the updated PDF
                targetDoc.Save(outputPdfPath);
                Console.WriteLine($"Updated PDF saved to: {outputPdfPath}");
            }

            // Clean up temporary XML file
            if (File.Exists(xmlDataPath))
            {
                File.Delete(xmlDataPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Serialises all form fields of a PDF document to a simple XML structure.
    /// </summary>
    private static void ExportFormDataToXml(Document doc, string xmlPath)
    {
        var root = new XElement("FormData");
        foreach (Field field in doc.Form.Fields)
        {
            // Use FullName if Name is null (some fields expose FullName only)
            string fieldName = field.Name ?? field.FullName ?? string.Empty;
            string fieldValue = field.Value?.ToString() ?? string.Empty;
            var fieldElement = new XElement("Field",
                new XAttribute("Name", fieldName),
                new XAttribute("Value", fieldValue));
            root.Add(fieldElement);
        }
        var docXml = new XDocument(root);
        docXml.Save(xmlPath);
    }

    /// <summary>
    /// Deserialises form field values from the XML created by ExportFormDataToXml and applies them to the PDF document.
    /// </summary>
    private static void ImportFormDataFromXml(Document doc, string xmlPath)
    {
        if (!File.Exists(xmlPath))
            throw new FileNotFoundException("XML data file not found.", xmlPath);

        var docXml = XDocument.Load(xmlPath);
        foreach (XElement fieldElement in docXml.Root.Elements("Field"))
        {
            string name = fieldElement.Attribute("Name")?.Value;
            string value = fieldElement.Attribute("Value")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(name))
                continue;

            // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
            Field? pdfField = doc.Form[name] as Field;
            if (pdfField != null)
            {
                pdfField.Value = value;
            }
        }
    }
}
