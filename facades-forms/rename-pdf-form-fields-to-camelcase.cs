using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    // Convert a field name to camelCase (e.g., "First_Name" -> "firstName")
    static string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        // Split on underscores, spaces and hyphens
        var parts = name.Split(new[] { '_', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return name;

        // First part lowercased, subsequent parts capitalized
        var result = parts[0].ToLowerInvariant();
        for (int i = 1; i < parts.Length; i++)
        {
            var part = parts[i];
            if (part.Length == 0) continue;
            result += char.ToUpperInvariant(part[0]) + part.Substring(1).ToLowerInvariant();
        }
        return result;
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "renamed_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // First, read the existing field names using the Form facade (not FormEditor)
        string[] fieldNames;
        using (var formReader = new Form())
        {
            formReader.BindPdf(inputPath);
            fieldNames = formReader.FieldNames; // FieldNames property returns string[]
        }

        // Build a map of old name -> new camelCase name
        var renameMap = new Dictionary<string, string>();
        foreach (string oldName in fieldNames)
        {
            string newName = ToCamelCase(oldName);
            if (!string.Equals(oldName, newName, StringComparison.Ordinal))
            {
                renameMap[oldName] = newName;
            }
        }

        // Perform renames using FormEditor (one call per field)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            foreach (var kvp in renameMap)
            {
                // RenameField(oldName, newName) is the correct method name
                editor.RenameField(kvp.Key, kvp.Value);
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }
}
