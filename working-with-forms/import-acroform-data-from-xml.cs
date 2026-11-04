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
        const string xmlDataPath = "updatedData.xml";
        const string outputPdfPath = "output_with_form_data.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data not found: {xmlDataPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the XML that contains the form data.
            // Expected simple format:
            // <FormData>
            //   <Field name="FieldName">Value</Field>
            //   ...
            // </FormData>
            XDocument xmlDoc = XDocument.Load(xmlDataPath);

            foreach (XElement fieldElem in xmlDoc.Descendants("Field"))
            {
                XAttribute nameAttr = fieldElem.Attribute("name");
                if (nameAttr == null)
                    continue; // skip malformed entries

                string fieldName = nameAttr.Value;
                string fieldValue = fieldElem.Value ?? string.Empty;

                // Check if the PDF actually contains the field
                if (!pdfDoc.Form.HasField(fieldName))
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found in PDF.");
                    continue;
                }

                // Retrieve the field via the indexer – it returns a WidgetAnnotation that we cast to the concrete type
                var field = pdfDoc.Form[fieldName];

                switch (field)
                {
                    case TextBoxField txt:
                        txt.Value = fieldValue;
                        break;
                    case CheckboxField chk:
                        // Treat common truthy strings as checked
                        chk.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                      fieldValue.Equals("1") ||
                                      fieldValue.Equals("yes", StringComparison.OrdinalIgnoreCase);
                        break;
                    case RadioButtonField rad:
                        rad.Value = fieldValue;
                        break;
                    case ListBoxField list:
                        // Clear previous selections and set the new one (1‑based index expected by Aspose)
                        // Find the option that matches the supplied value
                        int index = 0;
                        foreach (var opt in list.Options)
                        {
                            index++;
                            if (opt.Name == fieldValue || opt.Value == fieldValue)
                            {
                                list.SelectedItems = new[] { index };
                                break;
                            }
                        }
                        break;
                    case ComboBoxField combo:
                        // Set the selected value directly
                        combo.Value = fieldValue;
                        break;
                    case ButtonField _:
                        // Buttons do not hold a value; ignore
                        break;
                    default:
                        // Unknown or unsupported field type – ignore safely
                        break;
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
