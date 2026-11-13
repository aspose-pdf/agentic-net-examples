using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "filtered_fields.csv";
        const string prefix = "Customer_";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document contains form fields
            if (doc.Form == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found.");
                return;
            }

            // Collect fields whose names start with the specified prefix
            List<Field> matchingFields = new List<Field>();
            foreach (Field field in doc.Form.Fields)
            {
                if (!string.IsNullOrEmpty(field.FullName) &&
                    field.FullName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    matchingFields.Add(field);
                }
            }

            // Export the filtered fields to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                writer.WriteLine("FieldName,FieldValue"); // CSV header

                foreach (Field field in matchingFields)
                {
                    string value = GetFieldValueAsString(field);
                    // Escape double quotes and ensure a non‑null string for CSV output
                    string escapedValue = value?.Replace("\"", "\"\"") ?? string.Empty;
                    writer.WriteLine($"{field.FullName},\"{escapedValue}\"");
                }
            }

            Console.WriteLine($"Exported {matchingFields.Count} fields to '{outputCsv}'.");
        }
    }

    // Helper method to obtain a string representation of various form field types
    private static string GetFieldValueAsString(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value ?? string.Empty;
            case CheckboxField chk:
                return chk.Checked ? "Checked" : "Unchecked";
            case RadioButtonField rbl:
                // RadioButtonField exposes the selected export value via the Value property
                return rbl.Value ?? string.Empty;
            case ListBoxField lst:
                // Map selected indices (1‑based) to their option values
                if (lst.SelectedItems == null || lst.Options == null)
                    return string.Empty;
                var selectedValues = lst.SelectedItems
                    .Where(i => i > 0 && i <= lst.Options.Count)
                    .Select(i => lst.Options[i].Value ?? string.Empty);
                return string.Join(";", selectedValues);
            case ComboBoxField cmb:
                // The selected value is stored in the Value property
                return cmb.Value ?? string.Empty;
            case SignatureField sig:
                return sig.Signature != null ? "Signed" : "Unsigned";
            default:
                return field.Value?.ToString() ?? string.Empty;
        }
    }
}
