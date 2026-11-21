using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";
        const string logPath = "field_modifications.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Open the log file for appending; each modification will be written here
            using (StreamWriter log = new StreamWriter(logPath, append: true))
            {
                // Iterate over all form fields in the document
                foreach (Field field in doc.Form)
                {
                    // Capture the original value of the field
                    string oldValue = GetFieldValue(field);
                    string newValue = oldValue;
                    bool modified = false;

                    // Example modification logic for different field types
                    if (field is TextBoxField txt)
                    {
                        txt.Value = "Sample Text";
                        newValue = txt.Value ?? string.Empty;
                        modified = true;
                    }
                    else if (field is CheckboxField chk)
                    {
                        chk.Checked = true;
                        newValue = chk.Checked.ToString();
                        modified = true;
                    }
                    else if (field is RadioButtonField radGroup)
                    {
                        // Select the first option if any are defined (Options collection is 1‑based)
                        if (radGroup.Options.Count > 0)
                        {
                            radGroup.Selected = 1; // 1‑based index
                            newValue = radGroup.Value ?? string.Empty;
                            modified = true;
                        }
                    }
                    else if (field is ListBoxField list)
                    {
                        if (list.Options.Count > 0)
                        {
                            // SelectedItems is an int[] of 1‑based indices
                            list.SelectedItems = new[] { 1 };
                            newValue = string.Join(", ", list.SelectedItems);
                            modified = true;
                        }
                    }
                    else if (field is ComboBoxField combo)
                    {
                        if (combo.Options.Count > 0)
                        {
                            combo.Selected = 1; // 1‑based index
                            newValue = combo.Value ?? string.Empty;
                            modified = true;
                        }
                    }

                    // If the field was changed, write an audit entry
                    if (modified)
                    {
                        string logEntry = $"{DateTime.UtcNow:O}\tFieldName: {field.FullName}\tOldValue: {oldValue}\tNewValue: {newValue}";
                        log.WriteLine(logEntry);
                    }
                }
            }

            // Save the modified PDF; Save() is called inside the using block
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Modified PDF saved to '{outputPath}'. Log written to '{logPath}'.");
    }

    // Helper method to obtain a string representation of a field's current value
    static string GetFieldValue(Field field)
    {
        switch (field)
        {
            case TextBoxField txt:
                return txt.Value ?? string.Empty;
            case CheckboxField chk:
                return chk.Checked.ToString();
            case RadioButtonField rad:
                return rad.Value ?? string.Empty;
            case ListBoxField list:
                // ListBoxField does not expose selected text directly; use SelectedItems indices
                return string.Join(", ", list.SelectedItems);
            case ComboBoxField combo:
                return combo.Value ?? string.Empty;
            default:
                return string.Empty;
        }
    }
}
