using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq; // Needed for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect field names and their values
            var fieldValues = new Dictionary<string, object>();

            // Iterate over every form field in the document
            foreach (Field field in doc.Form)
            {
                object value = null;

                // Handle known field types explicitly
                switch (field)
                {
                    case TextBoxField txt:
                        value = txt.Value;
                        break;
                    case CheckboxField chk:
                        // For checkboxes the logical value is the Checked flag
                        value = chk.Checked;
                        break;
                    case RadioButtonField radGroup:
                        // RadioButtonField represents the whole group. The selected option
                        // is indicated by the integer index stored in Selected.
                        if (radGroup.Options != null && radGroup.Options.Count() > 0 &&
                            radGroup.Selected >= 0 && radGroup.Selected < radGroup.Options.Count())
                        {
                            value = radGroup.Options[radGroup.Selected];
                        }
                        else
                        {
                            value = null;
                        }
                        break;
                    case ListBoxField list:
                        // ListBox may have multiple selections
                        if (list.SelectedItems != null && list.SelectedItems.Count() > 0)
                        {
                            value = list.SelectedItems.Count() == 1
                                ? (object)list.SelectedItems[0]
                                : list.SelectedItems;
                        }
                        break;
                    case ComboBoxField combo:
                        value = combo.Value;
                        break;
                    default:
                        // Fallback to the generic Value property if present
                        value = field.Value;
                        break;
                }

                // Use FullName as the key; store empty string for null values
                fieldValues[field.FullName] = value ?? "";
            }

            // Serialize the dictionary to a formatted JSON string
            string json = JsonSerializer.Serialize(fieldValues, new JsonSerializerOptions { WriteIndented = true });

            Console.WriteLine(json);
        }
    }
}
