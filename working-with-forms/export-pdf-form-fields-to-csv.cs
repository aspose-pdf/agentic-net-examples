using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "form_fields.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // Prepare a StringBuilder for CSV content
                StringBuilder csvBuilder = new StringBuilder();
                // Header row
                csvBuilder.AppendLine("FieldName,FieldType,Value");

                // Iterate over all form fields in the document
                foreach (Field field in doc.Form.Fields)
                {
                    string fieldName = field.FullName ?? string.Empty;
                    string fieldType = field.GetType().Name;
                    string fieldValue = string.Empty;

                    // Strong‑typed handling for TextBoxField (stable across versions)
                    if (field is TextBoxField textBox)
                    {
                        fieldValue = textBox.Value ?? string.Empty;
                    }
                    else
                    {
                        // Fallback handling using runtime type name and dynamic access.
                        // This avoids compile‑time dependencies on field classes that may be absent
                        // or have renamed members in different Aspose.Pdf versions.
                        string typeName = field.GetType().Name;
                        dynamic dynField = field;

                        switch (typeName)
                        {
                            case "CheckBoxField":
                                try
                                {
                                    bool isChecked = (bool)dynField.Checked;
                                    fieldValue = isChecked ? "Checked" : "Unchecked";
                                }
                                catch
                                {
                                    fieldValue = "UnsupportedCheckBox";
                                }
                                break;

                            case "RadioButtonOptionField":
                                // Some versions expose a 'Checked' property, others a 'Selected' property.
                                try
                                {
                                    bool isSelected = (bool)dynField.Checked;
                                    fieldValue = isSelected ? "Selected" : "NotSelected";
                                }
                                catch
                                {
                                    try
                                    {
                                        bool isSelected = (bool)dynField.Selected;
                                        fieldValue = isSelected ? "Selected" : "NotSelected";
                                    }
                                    catch
                                    {
                                        fieldValue = "UnsupportedRadioButton";
                                    }
                                }
                                break;

                            case "ListBoxField":
                                // SelectedItems may be an IList<int> or IEnumerable<int> depending on version.
                                try
                                {
                                    var selected = dynField.SelectedItems as System.Collections.IEnumerable;
                                    if (selected != null)
                                    {
                                        var items = new List<string>();
                                        foreach (var it in selected)
                                        {
                                            items.Add(it?.ToString() ?? string.Empty);
                                        }
                                        fieldValue = string.Join("|", items);
                                    }
                                }
                                catch
                                {
                                    fieldValue = "UnsupportedListBox";
                                }
                                break;

                            case "ComboBoxField":
                                // Some versions expose 'SelectedItem', others 'Value'.
                                try
                                {
                                    fieldValue = dynField.SelectedItem?.ToString() ?? string.Empty;
                                }
                                catch
                                {
                                    try
                                    {
                                        fieldValue = dynField.Value?.ToString() ?? string.Empty;
                                    }
                                    catch
                                    {
                                        fieldValue = "UnsupportedComboBox";
                                    }
                                }
                                break;

                            case "SignatureField":
                                fieldValue = "SignatureField";
                                break;

                            default:
                                fieldValue = "UnsupportedFieldType";
                                break;
                        }
                    }

                    // Escape commas and quotes for CSV compliance
                    string escapedValue = fieldValue.Replace("\"", "\"\"");
                    if (escapedValue.Contains(",") || escapedValue.Contains("\""))
                    {
                        escapedValue = $"\"{escapedValue}\"";
                    }

                    // Append CSV line
                    csvBuilder.AppendLine($"{fieldName},{fieldType},{escapedValue}");
                }

                // Write CSV to file
                File.WriteAllText(outputCsvPath, csvBuilder.ToString());
                Console.WriteLine($"Form fields exported to '{outputCsvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
