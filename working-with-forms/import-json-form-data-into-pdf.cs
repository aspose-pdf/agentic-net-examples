using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the PDF template, JSON data file and the output PDF
        const string pdfTemplatePath = "template.pdf";
        const string jsonDataPath    = "data.json";
        const string outputPdfPath   = "filled.pdf";

        // Verify that required files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Read and deserialize the JSON into a dictionary of field name / value pairs
        string jsonContent = File.ReadAllText(jsonDataPath);
        Dictionary<string, JsonElement> fieldData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonContent);

        // Open the PDF document
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Iterate over each entry from the JSON
            foreach (KeyValuePair<string, JsonElement> entry in fieldData)
            {
                string fieldName  = entry.Key;
                string fieldValue = entry.Value.GetString();

                // Retrieve the widget annotation that represents the form field
                WidgetAnnotation widget = pdfDoc.Form[fieldName] as WidgetAnnotation;
                if (widget == null)
                    continue; // field not found or not a widget

                // Determine the concrete field type at runtime and populate accordingly
                string typeName = widget.GetType().Name;

                switch (typeName)
                {
                    case "TextBoxField":
                        // Text box – assign the string value directly
                        ((TextBoxField)widget).Value = fieldValue;
                        break;

                    case "CheckBoxField":
                        // Check box – interpret "true"/"1" as checked
                        dynamic checkBox = widget;
                        checkBox.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) || fieldValue == "1";
                        break;

                    case "RadioButtonOptionField":
                        // Radio button – try to set the selected option/value
                        dynamic radio = widget;
                        if (HasProperty(radio, "Selected"))
                        {
                            radio.Selected = fieldValue;
                        }
                        else if (HasProperty(radio, "Checked"))
                        {
                            radio.Checked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) || fieldValue == "1";
                        }
                        break;

                    case "ListBoxField":
                        // List box – set the first matching index (fallback to first item)
                        dynamic listBox = widget;
                        // Attempt to find the index of the item that matches the supplied value
                        int index = FindListBoxItemIndex(listBox, fieldValue);
                        listBox.SelectedItems = new int[] { index };
                        break;

                    case "ComboBoxField":
                        // Combo box – set the selected value
                        dynamic combo = widget;
                        combo.Selected = fieldValue;
                        break;

                    // Additional field types (e.g., SignatureField) can be handled here as needed
                    default:
                        // Unknown or unsupported field type – ignore
                        break;
                }
            }

            // Save the populated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported successfully to '{outputPdfPath}'.");
    }

    // Helper: checks whether a dynamic object has a given property
    private static bool HasProperty(object obj, string propertyName)
    {
        return obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance) != null;
    }

    // Helper: attempts to locate the index of a list‑box item that matches the supplied value.
    // If not found, returns 0 (first item) to avoid exceptions.
    private static int FindListBoxItemIndex(dynamic listBox, string value)
    {
        try
        {
            // ListBoxField exposes a "Items" collection of strings.
            var items = listBox.Items as IList<string>;
            if (items != null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Equals(value, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
            }
        }
        catch
        {
            // Swallow any reflection‑related issues – fallback to 0.
        }
        return 0;
    }
}
