using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputCsv = "form_fields.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Capture start timestamp
        DateTime start = DateTime.UtcNow;
        Console.WriteLine($"Form extraction started at {start:O}");

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Check for form fields
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found.");
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(outputCsv, false))
                    {
                        writer.WriteLine("FieldName,FieldValue");
                        foreach (Field field in doc.Form)
                        {
                            string name = field.FullName;
                            string value = GetFieldValue(field);
                            writer.WriteLine($"{EscapeCsv(name)},{EscapeCsv(value)}");
                        }
                    }
                    Console.WriteLine($"Form data written to {outputCsv}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }

        // Capture end timestamp
        DateTime end = DateTime.UtcNow;
        Console.WriteLine($"Form extraction ended at {end:O}");
        Console.WriteLine($"Duration: {(end - start).TotalSeconds:F2} seconds");
    }

    // Retrieve a field's value as a string
    private static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value;
            case CheckboxField chk:
                return chk.Checked ? "Checked" : "Unchecked";
            case RadioButtonField rad:
                // RadioButtonField stores the selected option index (1‑based).
                if (rad.Selected > 0 && rad.Options != null && rad.Options.Count >= rad.Selected)
                {
                    // Return the option's name (or value if preferred).
                    return rad.Options[rad.Selected - 1].Name ?? string.Empty;
                }
                return string.Empty;
            case ListBoxField list:
                return string.Join(";", list.SelectedItems);
            case ComboBoxField combo:
                return combo.Value;
            case SignatureField sig:
                return sig.Signature != null ? "Signed" : "Unsigned";
            default:
                return string.Empty;
        }
    }

    // Simple CSV escaping
    private static string EscapeCsv(string s)
    {
        if (s == null) return "";
        if (s.Contains("\"") || s.Contains(",") || s.Contains("\n"))
        {
            s = s.Replace("\"", "\"\"");
            return $"\"{s}\"";
        }
        return s;
    }
}
