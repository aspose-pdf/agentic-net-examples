using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // First, collect the names of all fields that start with "Temp_"
        List<string> fieldsToRemove = new List<string>();
        using (Form formFacade = new Form(inputPath))
        {
            foreach (string fieldName in formFacade.FieldNames)
            {
                if (fieldName.StartsWith("Temp_", StringComparison.Ordinal))
                {
                    fieldsToRemove.Add(fieldName);
                }
            }
        }

        // If there are no matching fields, just copy the file
        if (fieldsToRemove.Count == 0)
        {
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("No temporary fields found. File copied unchanged.");
            return;
        }

        // Load the document and remove the collected fields using FormEditor
        using (Document doc = new Document(inputPath))
        using (FormEditor editor = new FormEditor(doc))
        {
            foreach (string fieldName in fieldsToRemove)
            {
                editor.RemoveField(fieldName);
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Temporary fields removed. Clean PDF saved to '{outputPath}'.");
    }
}