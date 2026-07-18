using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string xmlPath = "formDefinition.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML that defines the form fields and their default values
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create a new PDF document and add a blank page to host the form fields
        using (Document pdfDoc = new Document())
        {
            pdfDoc.Pages.Add();

            // If the PDF uses XFA, assign the XFA data from the XML
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Example XML structure:
            // <Fields>
            //   <Field name="FirstName" value="John Doe" />
            //   <Field name="Age" value="30" />
            // </Fields>
            XmlNodeList? fieldNodes = xmlDoc.SelectNodes("//Field");
            if (fieldNodes != null)
            {
                foreach (XmlNode node in fieldNodes)
                {
                    if (node.Attributes == null) continue;

                    string? fieldName = node.Attributes["name"]?.Value;
                    string? defaultValue = node.Attributes["value"]?.Value;

                    if (!string.IsNullOrEmpty(fieldName) && defaultValue != null)
                    {
                        // Retrieve the field; the indexer returns a WidgetAnnotation, so cast to Field
                        Field? field = pdfDoc.Form[fieldName] as Field;
                        if (field != null)
                        {
                            field.Value = defaultValue;
                        }
                    }
                }
            }

            // Save the resulting PDF with the populated form fields
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with form fields saved to '{outputPdf}'.");
    }
}
