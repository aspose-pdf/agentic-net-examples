using System;
using System.IO;
using System.Xml;
using System.Xml;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string xmlPath = "formdata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the document contains an interactive form
                if (doc.Form != null && doc.Form.Count > 0)
                {
                    // Export the form field values to a local XML file using a custom serializer
                    ExportFormDataToXml(doc, xmlPath);
                }
                else
                {
                    Console.WriteLine("The PDF does not contain any form fields.");
                }

                // Save the (unchanged) PDF to a new file
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Form data exported to '{xmlPath}'. PDF saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Serialises the AcroForm fields of a PDF document to a simple XML representation.
    /// This replaces the missing Form.ExportXml method while staying within the core Aspose.Pdf APIs.
    /// </summary>
    private static void ExportFormDataToXml(Document doc, string filePath)
    {
        var settings = new XmlWriterSettings { Indent = true, Encoding = System.Text.Encoding.UTF8 };
        using (var writer = XmlWriter.Create(filePath, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("FormData");

            foreach (Field field in doc.Form.Fields)
            {
                writer.WriteStartElement("Field");
                writer.WriteAttributeString("Name", field.PartialName ?? string.Empty);

                string value = string.Empty;
                switch (field)
                {
                    case TextBoxField txt:
                        value = txt.Value ?? string.Empty;
                        break;
                    case CheckboxField chk:
                        value = chk.Checked ? "true" : "false";
                        break;
                    case RadioButtonField rad:
                        value = rad.Value ?? string.Empty;
                        break;
                    case ListBoxField list:
                        // ListBoxField.SelectedItems returns an int[] of selected indices.
                        // Convert indices to the corresponding display values if needed; here we simply join the indices.
                        var selected = list.SelectedItems?.Select(i => i.ToString()).ToArray();
                        value = selected != null ? string.Join(",", selected) : string.Empty;
                        break;
                    case ComboBoxField combo:
                        value = combo.Value ?? string.Empty;
                        break;
                    case SignatureField _:
                        // Signature fields do not expose a simple string value; leave empty or add custom handling.
                        value = string.Empty;
                        break;
                    default:
                        // For any other field types, attempt to use the generic Value property if available.
                        if (field.Value != null)
                            value = field.Value.ToString();
                        break;
                }

                writer.WriteString(value);
                writer.WriteEndElement(); // Field
            }

            writer.WriteEndElement(); // FormData
            writer.WriteEndDocument();
        }
    }
}
