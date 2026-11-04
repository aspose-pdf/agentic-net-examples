using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class FormDataTransformer
{
    static void Main()
    {
        const string sourcePdfPath   = "source_form.pdf";
        const string targetPdfPath   = "target_form.pdf";
        const string outputPdfPath   = "merged_output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        // 1. Extract form data from the source PDF
        var sourceData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // Iterate over all form fields in the source document
            foreach (Field field in srcDoc.Form.Fields)
            {
                string fieldName = field.PartialName;
                string fieldValue = string.Empty;

                // Retrieve the value based on the concrete field type
                switch (field)
                {
                    case TextBoxField txt:
                        fieldValue = txt.Value?.ToString() ?? string.Empty;
                        break;
                    case CheckboxField chk:
                        fieldValue = chk.Checked ? "True" : "False";
                        break;
                    case RadioButtonField rad:
                        // Selected is 1‑based; we store the index as string
                        fieldValue = rad.Selected > 0 ? rad.Selected.ToString() : string.Empty;
                        break;
                    // Add other field types as needed
                }

                sourceData[fieldName] = fieldValue;
            }
        }

        // 2. Transform the extracted data to a new schema
        var nameMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "FirstName", "GivenName" },
            { "LastName",  "FamilyName" },
            { "Email",     "ContactEmail" }
            // add more mappings as required
        };

        var transformedData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var kvp in sourceData)
        {
            string newName = nameMapping.ContainsKey(kvp.Key) ? nameMapping[kvp.Key] : kvp.Key;
            string newValue = kvp.Value.Trim().ToUpperInvariant();
            transformedData[newName] = newValue;
        }

        // 3. Import the transformed data into the target PDF
        using (Document tgtDoc = new Document(targetPdfPath))
        {
            foreach (var kvp in transformedData)
            {
                // Locate a field with the transformed name in the target PDF
                Field targetField = tgtDoc.Form[kvp.Key] as Field;

                if (targetField != null)
                {
                    // Set the field's value according to its concrete type
                    switch (targetField)
                    {
                        case TextBoxField txt:
                            txt.Value = kvp.Value;
                            break;
                        case CheckboxField chk:
                            chk.Checked = kvp.Value.Equals("TRUE", StringComparison.OrdinalIgnoreCase);
                            break;
                        case RadioButtonField rad:
                            // Attempt to match the stored value with an option index
                            if (int.TryParse(kvp.Value, out int sel) && sel > 0 && sel <= rad.Options.Count)
                                rad.Selected = sel;
                            break;
                        // Extend with other field types as required
                    }
                }
                else
                {
                    Console.WriteLine($"Warning: Target PDF does not contain field '{kvp.Key}'.");
                }
            }

            // 4. Save the updated target PDF
            tgtDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data transferred and saved to '{outputPdfPath}'.");
    }
}
