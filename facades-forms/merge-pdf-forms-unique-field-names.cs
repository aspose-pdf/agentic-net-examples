using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
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

        // 1. Retrieve all field names using the Form facade (FieldNames property).
        string[] fieldNames;
        using (var form = new Form())
        {
            form.BindPdf(inputPath);
            fieldNames = form.FieldNames; // no GetFieldNames method
        }

        // 2. Rename duplicate field names with FormEditor.
        using (var editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Track occurrences (case‑insensitive).
            var nameCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var originalName in fieldNames)
            {
                if (nameCount.ContainsKey(originalName))
                {
                    // Duplicate – generate a new unique name.
                    nameCount[originalName]++;
                    string newName = $"{originalName}_{nameCount[originalName]}";

                    // Guard against a generated name that already exists.
                    while (nameCount.ContainsKey(newName))
                    {
                        nameCount[originalName]++;
                        newName = $"{originalName}_{nameCount[originalName]}";
                    }

                    editor.RenameField(originalName, newName);
                    nameCount[newName] = 1; // register the new name
                }
                else
                {
                    nameCount[originalName] = 1; // first occurrence
                }
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Duplicate field names renamed and saved to '{outputPath}'.");
    }
}
