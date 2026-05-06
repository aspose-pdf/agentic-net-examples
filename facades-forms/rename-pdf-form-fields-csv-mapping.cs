using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string csvMappingPath = "field_mapping.csv";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(csvMappingPath))
        {
            Console.Error.WriteLine($"CSV mapping file not found: {csvMappingPath}");
            return;
        }

        // Load field name mappings from CSV (format: oldName,newName per line)
        var fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(csvMappingPath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
                continue; // skip malformed lines

            var oldName = parts[0].Trim();
            var newName = parts[1].Trim();

            if (!string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName))
                fieldMap[oldName] = newName;
        }

        // Rename fields using FormEditor (facade API)
        using (FormEditor formEditor = new FormEditor(inputPdfPath, outputPdfPath))
        {
            foreach (var kvp in fieldMap)
            {
                formEditor.RenameField(kvp.Key, kvp.Value);
            }

            // Persist changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Field renaming completed. Output saved to '{outputPdfPath}'.");
    }
}