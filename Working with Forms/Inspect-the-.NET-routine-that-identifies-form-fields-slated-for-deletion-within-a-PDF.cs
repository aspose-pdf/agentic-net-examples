using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // Access the form object
                Form form = doc.Form;

                // Collect names of fields that should be removed.
                // Example criterion: field name starts with "DeleteMe"
                List<string> fieldsToDelete = new List<string>();
                foreach (Field field in form.Fields)
                {
                    if (!string.IsNullOrEmpty(field.FullName) &&
                        field.FullName.StartsWith("DeleteMe", StringComparison.OrdinalIgnoreCase))
                    {
                        fieldsToDelete.Add(field.FullName);
                    }
                }

                // Delete the identified fields using Form.Delete(string)
                foreach (string fieldName in fieldsToDelete)
                {
                    form.Delete(fieldName);
                    Console.WriteLine($"Deleted field: {fieldName}");
                }

                // Save the modified PDF (lifecycle rule: use Save)
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}