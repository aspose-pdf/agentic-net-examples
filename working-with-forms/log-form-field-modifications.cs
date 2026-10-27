using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logPath = "field_modifications.log";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        // Open the log file inside its own using block
        using (StreamWriter logWriter = new StreamWriter(logPath, false))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                doc.Save(outputPdf); // Save unchanged document
                return;
            }

            // Iterate over each form field
            foreach (Field field in doc.Form.Fields)
            {
                string fieldName = field.FullName ?? field.PartialName ?? "(unnamed)";
                string oldValue = GetFieldValue(field);
                string newValue = oldValue; // default to unchanged

                // Apply modifications based on field type
                if (field is TextBoxField txt)
                {
                    newValue = "Updated Text";
                    txt.Value = newValue;
                }
                else
                {
                    // Use runtime type inspection for field types that may not be present in all Aspose.Pdf versions
                    string typeName = field.GetType().Name;

                    if (typeName == "CheckBoxField")
                    {
                        dynamic chk = field;
                        chk.Checked = !chk.Checked; // toggle
                        newValue = chk.Checked.ToString();
                    }
                    else if (typeName == "RadioButtonListField" || typeName == "RadioButtonOptionField")
                    {
                        dynamic rbl = field;
                        if (rbl.Options != null && rbl.Options.Count > 0)
                        {
                            rbl.Selected = rbl.Options[0];
                            newValue = rbl.Selected ?? string.Empty;
                        }
                    }
                    else if (typeName == "ListBoxField")
                    {
                        dynamic lst = field;
                        if (lst.Options != null && lst.Options.Count > 0)
                        {
                            // Assign a new array containing the first option
                            lst.SelectedItems = new string[] { lst.Options[0] };
                            newValue = string.Join(", ", lst.SelectedItems);
                        }
                    }
                    else if (typeName == "ComboBoxField")
                    {
                        dynamic cmb = field;
                        if (cmb.Options != null && cmb.Options.Count > 0)
                        {
                            cmb.Selected = cmb.Options[0];
                            newValue = cmb.Selected ?? string.Empty;
                        }
                    }
                    // Add other field types as needed using the same pattern
                }

                // Log the modification (only if value changed)
                if (!string.Equals(oldValue, newValue, StringComparison.Ordinal))
                {
                    logWriter.WriteLine($"{DateTime.UtcNow:u} | Field: {fieldName} | Old Value: \"{oldValue}\" | New Value: \"{newValue}\"");
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Processing complete. Modified PDF saved to '{outputPdf}'. Log written to '{logPath}'.");
    }

    // Helper method to retrieve a field's current value as string
    private static string GetFieldValue(Field field)
    {
        if (field is TextBoxField txt)
            return txt.Value ?? string.Empty;

        string typeName = field.GetType().Name;
        if (typeName == "CheckBoxField")
        {
            dynamic chk = field;
            return chk.Checked.ToString();
        }
        if (typeName == "RadioButtonListField" || typeName == "RadioButtonOptionField")
        {
            dynamic rbl = field;
            return rbl.Selected ?? string.Empty;
        }
        if (typeName == "ListBoxField")
        {
            dynamic lst = field;
            var items = lst.SelectedItems as string[];
            return items != null ? string.Join(", ", items) : string.Empty;
        }
        if (typeName == "ComboBoxField")
        {
            dynamic cmb = field;
            return cmb.Selected ?? string.Empty;
        }
        return string.Empty;
    }
}
