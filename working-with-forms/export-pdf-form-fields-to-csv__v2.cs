using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "form_fields.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the document actually contains an AcroForm with fields
                // Fixed: Count is a method, so we need to invoke it.
                if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
                {
                    Console.WriteLine("No form fields found in the document.");
                    return;
                }

                // Create the CSV file (UTF‑8) inside a using block
                using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("FieldName,FieldValue");

                    // Iterate over each form field and write its name/value
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Field name – PartialName is the field's identifier
                        string name = field.PartialName ?? string.Empty;

                        // Resolve the field value based on its concrete type
                        string value = GetFieldValue(field);

                        // Escape commas, quotes, and newlines for CSV compliance
                        string escapedName = EscapeCsv(name);
                        string escapedValue = EscapeCsv(value);

                        writer.WriteLine($"{escapedName},{escapedValue}");
                    }
                }

                Console.WriteLine($"Form fields exported to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Centralised logic for extracting a field's current value
    private static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case CheckboxField cb:
                return cb.Checked ? "True" : "False";

            case TextBoxField tb:
                return tb.Value ?? string.Empty;

            case ComboBoxField combo:
                return combo.Value ?? string.Empty;

            case ListBoxField listBox:
                // SelectedItems returns an array of integer indices.
                // Join them as a semicolon‑separated string.
                if (listBox.SelectedItems != null && listBox.SelectedItems.Length > 0)
                    return string.Join(";", listBox.SelectedItems);
                return string.Empty;

            case RadioButtonOptionField radio:
                return radio.Value ?? string.Empty;

            default:
                try
                {
                    return field.Value?.ToString() ?? string.Empty;
                }
                catch
                {
                    // If the field does not expose a Value property, return empty.
                    return string.Empty;
                }
        }
    }

    // Helper to escape a CSV field according to RFC 4180
    private static string EscapeCsv(string input)
    {
        if (input == null) return string.Empty;

        if (input.Contains('"'))
            input = input.Replace("\"", "\"\"");

        if (input.Contains(',') || input.Contains('\n') || input.Contains('\r'))
            return $"\"{input}\"";

        return input;
    }
}
