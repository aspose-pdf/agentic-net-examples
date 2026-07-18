using System;
using System.Collections.Generic;
using System.IO;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect names of required fields that are empty
            List<string> missingFields = new List<string>();

            // Iterate over the form fields (Aspose.Pdf.Forms.Field objects)
            foreach (Field field in doc.Form.Fields)
            {
                // Consider only fields that are marked as required
                if (field.Required)
                {
                    // The Value property holds the entered data for the field.
                    string fieldValue = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        // Prefer the short Name; fall back to FullName if Name is null.
                        string name = field.Name ?? field.FullName ?? "<unnamed>";
                        missingFields.Add(name);
                    }
                }
            }

            // If any required fields are missing, abort the save operation
            if (missingFields.Count > 0)
            {
                Console.WriteLine("Cannot save PDF. The following required fields are empty:");
                foreach (string name in missingFields)
                {
                    Console.WriteLine($" - {name}");
                }
                return;
            }

            // All required fields are filled; proceed to save the document
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
    }
}
