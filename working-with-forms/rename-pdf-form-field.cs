using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "renamed_fields.pdf";
        const string oldFieldName = "CustomerID";
        const string newFieldName = "Cust_ID";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Verify that the field to be renamed exists
            if (!form.HasField(oldFieldName))
            {
                Console.Error.WriteLine($"Field '{oldFieldName}' not found.");
                return;
            }

            // Retrieve the existing field (cast from WidgetAnnotation to Field)
            Field oldField = doc.Form[oldFieldName] as Field;
            if (oldField == null)
            {
                Console.Error.WriteLine($"Field '{oldFieldName}' is not a form field.");
                return;
            }

            // Add a copy of the field with the new name on the same page
            // Field.PageIndex is zero‑based; Add expects a 1‑based page number
            int pageNumber = oldField.PageIndex + 1;
            Field newField = form.Add(oldField, newFieldName, pageNumber);

            // Optionally copy additional properties (e.g., MappingName) if needed
            newField.MappingName = oldField.MappingName;
            newField.AlternateName = oldField.AlternateName;
            newField.Value = oldField.Value;

            // Delete the original field
            form.Delete(oldFieldName);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field renamed from '{oldFieldName}' to '{newFieldName}'. Saved as '{outputPath}'.");
    }
}
