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
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where the XML files will be written
        const string outputFolder = "FormXml";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Export only if the document contains a form with at least one field
                    if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Count() > 0)
                    {
                        // Build the XML file name based on the PDF name
                        string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                        string xmlPath = Path.Combine(outputFolder, xmlFileName);

                        // Export the form data to XML using a custom serializer
                        ExportFormToXml(doc, xmlPath);

                        Console.WriteLine($"Exported: {xmlPath}");
                    }
                    else
                    {
                        Console.WriteLine($"No form fields in: {pdfPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch export of form data completed.");
    }

    /// <summary>
    /// Serialises the AcroForm fields of a PDF document to a simple XML structure.
    /// The XML format is:
    ///   <FormData>
    ///       <Field name=\"fieldName\">fieldValue</Field>
    ///       ...
    ///   </FormData>
    /// </summary>
    /// <param name="doc">The loaded PDF document.</param>
    /// <param name="xmlPath">The full path where the XML file will be written.</param>
    private static void ExportFormToXml(Document doc, string xmlPath)
    {
        var settings = new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 };
        using (XmlWriter writer = XmlWriter.Create(xmlPath, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("FormData");

            foreach (Field field in doc.Form.Fields)
            {
                string fieldName = field.Name ?? field.FullName ?? string.Empty;
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                writer.WriteStartElement("Field");
                writer.WriteAttributeString("name", fieldName);
                writer.WriteString(fieldValue);
                writer.WriteEndElement(); // Field
            }

            writer.WriteEndElement(); // FormData
            writer.WriteEndDocument();
        }
    }
}
