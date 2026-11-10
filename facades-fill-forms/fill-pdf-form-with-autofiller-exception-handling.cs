using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";
        const string dataFilePath = "data.xml"; // could be .json, .csv, etc.
        const string outputPdfPath = "filled_output.pdf";

        // Verify that required files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF template not found at '{inputPdfPath}'.");
            return;
        }

        if (!File.Exists(dataFilePath))
        {
            Console.Error.WriteLine($"Error: Data file not found at '{dataFilePath}'.");
            return;
        }

        try
        {
            // Load external data into a dictionary (simple XML example)
            var data = LoadData(dataFilePath);

            // Load the PDF document
            Document pdfDoc = new Document(inputPdfPath);

            if (pdfDoc.Form == null || pdfDoc.Form.Fields == null)
                throw new InvalidOperationException("The PDF does not contain any form fields.");

            // Iterate over each form field and set its value if present in the data dictionary
            foreach (var field in pdfDoc.Form.Fields)
            {
                if (field == null) continue;
                string fieldName = field.FullName;
                if (!data.TryGetValue(fieldName, out string value))
                    continue; // No data for this field – skip

                switch (field)
                {
                    case TextBoxField txt:
                        txt.Value = value;
                        break;
                    case CheckboxField chk:
                        chk.Checked = value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                      value.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                                      value == "1";
                        break;
                    case RadioButtonField rad:
                        // Find the option whose name matches the supplied value
                        foreach (var opt in rad.Options)
                        {
                            if (opt.Name.Equals(value, StringComparison.OrdinalIgnoreCase))
                            {
                                rad.Selected = opt.Index; // 1‑based index
                                break;
                            }
                        }
                        break;
                    case ListBoxField list:
                        // Expect a comma‑separated list of 1‑based indices
                        var parts = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var selected = new List<int>();
                        foreach (var part in parts)
                        {
                            if (int.TryParse(part.Trim(), out int idx))
                                selected.Add(idx);
                        }
                        if (selected.Count > 0)
                            list.SelectedItems = selected.ToArray();
                        break;
                    default:
                        // Fallback for other field types
                        field.Value = value;
                        break;
                }
            }

            // Save the filled PDF
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Form successfully filled and saved to '{outputPdfPath}'.");
        }
        // Specific form‑field or logical errors
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine($"Form error: {ex.Message}");
        }
        // File access related errors
        catch (FileNotFoundException ex)
        {
            Console.Error.WriteLine($"File error: {ex.Message}");
        }
        // General I/O errors (e.g., permission issues)
        catch (IOException ex)
        {
            Console.Error.WriteLine($"I/O error: {ex.Message}");
        }
        // Fallback for any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // Simple XML loader: <Root><FieldName>Value</FieldName>...</Root>
    private static Dictionary<string, string> LoadData(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var doc = XDocument.Load(path);
        foreach (var element in doc.Root.Elements())
        {
            dict[element.Name.LocalName] = element.Value;
        }
        return dict;
    }
}
