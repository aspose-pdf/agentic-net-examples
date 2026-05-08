using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve all field names from the PDF
        List<string> fieldsToRemove = new List<string>();
        using (Form form = new Form(inputPath))
        {
            foreach (string fieldName in form.FieldNames)
            {
                if (fieldName.StartsWith("Temp_", StringComparison.Ordinal))
                {
                    fieldsToRemove.Add(fieldName);
                }
            }
        }

        // Remove the identified fields and save the result
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            foreach (string fieldName in fieldsToRemove)
            {
                editor.RemoveField(fieldName);
            }
            // Disposing the FormEditor writes the output file.
        }

        Console.WriteLine($"Temporary fields removed. Output saved to '{outputPath}'.");
    }
}