using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the Form facade with input and output PDF files
        using (Form form = new Form(inputPath, outputPath))
        {
            // Iterate over all form field names
            foreach (string fieldName in form.FieldNames)
            {
                // Rename fields that start with "Old_"
                if (fieldName.StartsWith("Old_"))
                {
                    string newName = "New_" + fieldName.Substring("Old_".Length);
                    form.RenameField(fieldName, newName);
                }
            }

            // Persist the changes to the output PDF
            form.Save();
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }
}