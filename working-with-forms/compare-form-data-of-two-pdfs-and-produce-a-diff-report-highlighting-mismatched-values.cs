using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // required for form field types

class Program
{
    static void Main()
    {
        const string pdf1Path = "form1.pdf";
        const string pdf2Path = "form2.pdf";
        const string diffReportPath = "form_diff_report.json";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("Input PDFs not found.");
            return;
        }

        // Ensure deterministic disposal of Document objects
        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Extract form fields from both documents
            var fields1 = GetFormFields(doc1);
            var fields2 = GetFormFields(doc2);

            // Collect mismatched field entries
            var mismatches = new List<Dictionary<string, string>>();

            // Compare fields present in the first document
            foreach (var kvp in fields1)
            {
                string name = kvp.Key;
                string value1 = kvp.Value;
                if (fields2.TryGetValue(name, out string value2))
                {
                    if (!string.Equals(value1, value2, StringComparison.Ordinal))
                    {
                        mismatches.Add(new Dictionary<string, string>
                        {
                            {"Field", name},
                            {"Document1", value1},
                            {"Document2", value2}
                        });
                    }
                }
                else
                {
                    mismatches.Add(new Dictionary<string, string>
                    {
                        {"Field", name},
                        {"Document1", value1},
                        {"Document2", "(missing)"}
                    });
                }
            }

            // Detect fields that exist only in the second document
            foreach (var kvp in fields2)
            {
                if (!fields1.ContainsKey(kvp.Key))
                {
                    mismatches.Add(new Dictionary<string, string>
                    {
                        {"Field", kvp.Key},
                        {"Document1", "(missing)"},
                        {"Document2", kvp.Value}
                    });
                }
            }

            // Serialize mismatches to a simple JSON structure
            string json = GenerateJsonReport(mismatches);
            File.WriteAllText(diffReportPath, json);
            Console.WriteLine($"Form data diff report saved to '{diffReportPath}'.");
        }
    }

    // Retrieves a dictionary of form field names and their string values
    static Dictionary<string, string> GetFormFields(Document doc)
    {
        var dict = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (Field field in doc.Form) // Field is the base class for all form elements
        {
            string name = field.FullName;
            string value = GetFieldValue(field);
            dict[name] = value;
        }
        return dict;
    }

    // Normalizes the value of a form field to a string representation
    static string GetFieldValue(Field field)
    {
        // For most field types the generic Value property provides a suitable string.
        // Specific handling for TextBoxField is kept for clarity; other specialized
        // field types (CheckBox, RadioButton, ListBox, ComboBox, Signature) are
        // accessed via the generic Value property to avoid referencing types that
        // may not be present in older Aspose.Pdf versions.
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value ?? string.Empty;
            default:
                return field.Value?.ToString() ?? string.Empty;
        }
    }

    // Generates a minimal JSON document describing the mismatches
    static string GenerateJsonReport(List<Dictionary<string, string>> mismatches)
    {
        using (StringWriter sw = new StringWriter())
        {
            sw.Write("{\"Mismatches\":[");
            for (int i = 0; i < mismatches.Count; i++)
            {
                var entry = mismatches[i];
                sw.Write("{");
                int j = 0;
                foreach (var kv in entry)
                {
                    sw.Write($"\"{EscapeJson(kv.Key)}\":\"{EscapeJson(kv.Value)}\"");
                    if (j < entry.Count - 1) sw.Write(",");
                    j++;
                }
                sw.Write("}");
                if (i < mismatches.Count - 1) sw.Write(",");
            }
            sw.Write("]}");
            return sw.ToString();
        }
    }

    // Escapes characters that would break JSON syntax
    static string EscapeJson(string s)
    {
        return s?.Replace("\\", "\\\\").Replace("\"", "\\\"") ?? string.Empty;
    }
}
