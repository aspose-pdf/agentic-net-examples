using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputPdf = "filled_form.pdf";
        const string configPath = "defaults.cfg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Load default field values from a simple key=value file
        Dictionary<string, string> defaults = LoadDefaults(configPath);

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Fill each form field with its default value using the Document.Form API
            foreach (KeyValuePair<string, string> kvp in defaults)
            {
                // Check if the field exists in the PDF form
                if (doc.Form.HasField(kvp.Key))
                {
                    // The indexer returns a WidgetAnnotation; cast it to Field (or a concrete field type)
                    Field field = doc.Form[kvp.Key] as Field;
                    if (field != null)
                    {
                        // All field types expose a Value property
                        field.Value = kvp.Value;
                    }
                    else
                    {
                        // This should not happen for standard form fields, but we guard against it
                        Console.Error.WriteLine($"Warning: field '{kvp.Key}' exists but is not a standard form field.");
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Warning: field '{kvp.Key}' not found in the PDF form.");
                }
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdf}'.");
    }

    // Reads a configuration file where each line is "FieldName=Value"
    static Dictionary<string, string> LoadDefaults(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string line in File.ReadAllLines(path))
        {
            // Ignore empty lines and comments
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            int sep = line.IndexOf('=');
            if (sep <= 0)
                continue; // malformed line

            string key = line.Substring(0, sep).Trim();
            string value = line.Substring(sep + 1).Trim();
            dict[key] = value;
        }
        return dict;
    }
}
