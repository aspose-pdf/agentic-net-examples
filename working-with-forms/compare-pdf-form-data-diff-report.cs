using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class FormDataComparer
{
    static void Main()
    {
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";
        const string reportPath = "FormDataDiffReport.txt";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Load the two PDF documents inside using blocks (lifecycle rule)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Extract form fields into dictionaries: field name -> value
            var fields1 = ExtractFormFields(doc1);
            var fields2 = ExtractFormFields(doc2);

            // Build diff report
            var reportLines = new List<string>();
            reportLines.Add($"Form Data Comparison Report");
            reportLines.Add($"Generated: {DateTime.Now}");
            reportLines.Add($"File 1: {pdfPath1}");
            reportLines.Add($"File 2: {pdfPath2}");
            reportLines.Add(string.Empty);

            // Check for fields present in both documents
            foreach (var kvp in fields1)
            {
                string fieldName = kvp.Key;
                string value1 = kvp.Value;

                if (fields2.TryGetValue(fieldName, out string value2))
                {
                    if (!string.Equals(value1, value2, StringComparison.Ordinal))
                    {
                        reportLines.Add($"[MISMATCH] Field: '{fieldName}'");
                        reportLines.Add($"    File1 Value: '{value1}'");
                        reportLines.Add($"    File2 Value: '{value2}'");
                    }
                }
                else
                {
                    reportLines.Add($"[MISSING IN FILE2] Field: '{fieldName}' with value '{value1}'");
                }
            }

            // Check for fields that exist only in the second document
            foreach (var kvp in fields2)
            {
                if (!fields1.ContainsKey(kvp.Key))
                {
                    reportLines.Add($"[MISSING IN FILE1] Field: '{kvp.Key}' with value '{kvp.Value}'");
                }
            }

            // Write report to console
            foreach (var line in reportLines)
                Console.WriteLine(line);

            // Save report to a text file
            File.WriteAllLines(reportPath, reportLines);
            Console.WriteLine($"Diff report saved to '{reportPath}'.");
        }
    }

    // Helper method to extract all form fields from a document
    private static Dictionary<string, string> ExtractFormFields(Document doc)
    {
        var result = new Dictionary<string, string>(StringComparer.Ordinal);

        // The Forms collection is accessed via doc.Form (Aspose.Pdf namespace)
        // Each field implements IFormField; we retrieve its name and value.
        if (doc.Form != null && doc.Form.Fields != null)
        {
            foreach (var field in doc.Form.Fields)
            {
                // Field name – use FullName if available, otherwise Name.
                string name = field.FullName ?? field.Name ?? string.Empty;

                // Resolve the field value in a version‑agnostic way.
                string value = ResolveFieldValue(field);

                result[name] = value;
            }
        }

        return result;
    }

    // Centralised logic to obtain a string representation of a form field's value.
    // This method avoids direct references to concrete field classes that may differ
    // between Aspose.Pdf versions, eliminating compile‑time errors.
    private static string ResolveFieldValue(Field field)
    {
        // 1. Try the generic "Value" property – most field types expose it.
        var valueProp = field.GetType().GetProperty("Value");
        if (valueProp != null)
        {
            var valObj = valueProp.GetValue(field);
            if (valObj != null)
                return valObj.ToString();
        }

        // 2. CheckBox – look for a boolean "Checked" property.
        var checkedProp = field.GetType().GetProperty("Checked");
        if (checkedProp != null && checkedProp.PropertyType == typeof(bool))
        {
            bool isChecked = (bool)checkedProp.GetValue(field);
            return isChecked ? "Checked" : "Unchecked";
        }

        // 3. RadioButtonListField or ListBoxField – attempt to read "SelectedItem".
        var selectedItemProp = field.GetType().GetProperty("SelectedItem");
        if (selectedItemProp != null)
        {
            var selectedItem = selectedItemProp.GetValue(field);
            if (selectedItem != null)
            {
                // SelectedItem usually has a "Value" property.
                var itemValueProp = selectedItem.GetType().GetProperty("Value");
                if (itemValueProp != null)
                {
                    var itemVal = itemValueProp.GetValue(selectedItem);
                    return itemVal?.ToString() ?? string.Empty;
                }
                // Fallback to ToString() if no "Value" property.
                return selectedItem.ToString();
            }
        }

        // 4. ComboBoxField – also may expose a "Value" property (handled above),
        // but some versions expose "SelectedItem" similar to ListBox.
        // If we reach here, return empty string.
        return string.Empty;
    }
}
