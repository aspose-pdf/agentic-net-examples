using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF that contains interactive form fields
        using (Document doc = new Document(inputPath))
        {
            // Access the Form object associated with the document
            Form form = doc.Form;

            // Verify that form fields have been parsed correctly
            Console.WriteLine($"Total form fields: {form.Count}");

            // Iterate through all fields and display their full names and current values
            foreach (Field field in form.Fields)
            {
                Console.WriteLine($"Field Name: {field.FullName}, Value: {field.Value}");
            }

            // Example manipulation: set a new value for a specific field if it exists
            const string targetFieldName = "CustomerName";
            if (form.HasField(targetFieldName))
            {
                // Retrieve the field by its name using the indexer and cast to Field
                Field targetField = form[targetFieldName] as Field;
                if (targetField != null)
                {
                    targetField.Value = "John Doe";
                    Console.WriteLine($"Set '{targetFieldName}' to '{targetField.Value}'.");
                }
                else
                {
                    Console.WriteLine($"Field '{targetFieldName}' exists but could not be cast to a form Field.");
                }
            }
            else
            {
                Console.WriteLine($"Field '{targetFieldName}' not found.");
            }

            // NOTE: In recent versions of Aspose.Pdf the Form class no longer exposes a Calculate() method.
            // If you need to recalculate dependent fields, they are automatically updated when the document is saved.
            // Therefore the explicit call has been removed.

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
