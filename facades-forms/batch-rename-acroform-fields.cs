using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Use Form (Facade) to work with AcroForm fields.
        // Constructor binds the source PDF and sets the destination file.
        using (Form form = new Form(inputPath, outputPath))
        {
            // Iterate over all field names in the document.
            foreach (string fieldName in form.FieldNames)
            {
                // Identify fields that start with the "Old_" prefix.
                if (fieldName.StartsWith("Old_"))
                {
                    // Build the new field name by replacing the prefix.
                    string newFieldName = "New_" + fieldName.Substring(4);
                    // Rename the field using the Facade method.
                    form.RenameField(fieldName, newFieldName);
                }
            }

            // Persist the changes to the output PDF.
            form.Save();
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }
}