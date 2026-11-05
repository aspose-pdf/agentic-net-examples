using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "validated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Collect names of required fields that are empty.
            List<string> missingFields = new List<string>();

            // Ensure the document actually contains a form and fields.
            if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Any())
            {
                // Iterate over each field.
                foreach (Field field in doc.Form.Fields)
                {
                    // Determine if the field is marked as required via its Required property.
                    bool isRequired = field.Required;
                    if (!isRequired)
                        continue; // skip non‑required fields

                    bool isEmpty = false;

                    // Evaluate emptiness based on concrete field type.
                    switch (field)
                    {
                        case TextBoxField txt:
                            isEmpty = string.IsNullOrWhiteSpace(txt.Value);
                            break;
                        case CheckboxField chk:
                            // Unchecked is considered missing when required.
                            isEmpty = !chk.Checked;
                            break;
                        case RadioButtonField rad:
                            // No selected option means empty.
                            isEmpty = string.IsNullOrWhiteSpace(rad.Value);
                            break;
                        default:
                            // Fallback for other field types – treat null/whitespace as empty.
                            isEmpty = string.IsNullOrWhiteSpace(field.Value?.ToString());
                            break;
                    }

                    if (isEmpty)
                        missingFields.Add(field.FullName);
                }
            }

            // If any required fields are missing, abort the save operation.
            if (missingFields.Count > 0)
            {
                Console.Error.WriteLine("Cannot save PDF. The following required fields are empty:");
                foreach (string name in missingFields)
                    Console.Error.WriteLine($" - {name}");
                return;
            }

            // All required fields are filled; proceed to save the document.
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
    }
}
