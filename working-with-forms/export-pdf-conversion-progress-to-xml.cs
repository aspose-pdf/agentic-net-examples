using System;
using System.IO;
using System.Linq;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF form and the output XML file
        const string pdfPath = "form.pdf";
        const string xmlPath = "progress.xml";

        // Name of the form field that holds the progress bar value
        const string fieldName = "ProgressBar";

        // Verify the PDF file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Ensure the document actually contains a form
                if (doc.Form == null || doc.Form.Fields == null)
                {
                    Console.Error.WriteLine("The PDF does not contain any form fields.");
                    return;
                }

                // Check that the expected form field is present using the Fields collection
                bool fieldExists = doc.Form.Fields.Any(f => f.PartialName == fieldName);
                if (!fieldExists)
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' not found in the PDF.");
                    return;
                }

                // Retrieve the field value – the Form indexer returns a WidgetAnnotation, so cast to Field
                Field? field = doc.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' is not a supported field type.");
                    return;
                }
                string value = field.Value?.ToString() ?? string.Empty;

                // Create an XML file that records the progress bar value
                using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("FormProgress");

                    // Store the field name for reference
                    writer.WriteElementString("FieldName", fieldName);

                    // Store the extracted value
                    writer.WriteElementString("Value", value);

                    writer.WriteEndElement(); // FormProgress
                    writer.WriteEndDocument();
                }

                Console.WriteLine($"Progress bar value exported to '{xmlPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
