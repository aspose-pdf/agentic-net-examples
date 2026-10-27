using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xmlPath   = "data.xml";
        const string outputPdf = "updated.pdf";

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

        // Load XML data
        XDocument xmlDoc = XDocument.Load(xmlPath);
        // Expect a simple structure: <Root><FieldName>Value</FieldName>...</Root>
        var xmlElements = xmlDoc.Root?.Elements();

        if (xmlElements == null)
        {
            Console.Error.WriteLine("XML does not contain any elements.");
            return;
        }

        // Load PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Ensure the PDF has a form
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.WriteLine("PDF does not contain any form fields.");
            }
            else
            {
                // Iterate over XML elements and update matching PDF form fields
                foreach (var elem in xmlElements)
                {
                    string fieldName = elem.Name.LocalName;
                    string fieldValue = elem.Value ?? string.Empty;

                    // Retrieve the field; if it does not exist, the indexer returns null
                    var field = pdfDoc.Form[fieldName];
                    if (field != null)
                    {
                        // Most common field types expose a Value property
                        // Use a safe cast to handle different field types
                        if (field is TextBoxField textBox)
                        {
                            textBox.Value = fieldValue;
                        }
                        else if (field is CheckboxField checkBox)
                        {
                            // Treat "true"/"1" as checked
                            checkBox.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                                fieldValue.Equals("1");
                        }
                        else if (field is RadioButtonOptionField radio)
                        {
                            // Set the selected option if it matches the value
                            radio.Value = fieldValue;
                        }
                        else if (field is ListBoxField listBox)
                        {
                            // Select the matching item
                            listBox.Value = fieldValue;
                        }
                        else
                        {
                            // Fallback: attempt to set the generic Value property via reflection
                            var prop = field.GetType().GetProperty("Value");
                            if (prop != null && prop.CanWrite)
                            {
                                prop.SetValue(field, fieldValue);
                            }
                        }
                    }
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF updated and saved to '{outputPdf}'.");
    }
}
