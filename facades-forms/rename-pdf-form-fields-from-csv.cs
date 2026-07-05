using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_renamed.pdf";
        const string csvPath       = "field_mapping.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV mapping file not found: {csvPath}");
            return;
        }

        // Load mapping from CSV (format: oldFieldName,newFieldName per line, optional header)
        var renameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadLines(csvPath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line)) continue;

            // If the line contains a header (e.g., "OldName,NewName") and not a comma-separated pair, ignore it
            var parts = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) continue;

            var oldName = parts[0].Trim();
            var newName = parts[1].Trim();

            if (!string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName))
                renameMap[oldName] = newName;
        }

        if (renameMap.Count == 0)
        {
            Console.WriteLine("No valid field mappings found in CSV.");
            return;
        }

        // Use FormEditor to rename fields – it handles both AcroForm and XFA fields
        using (FormEditor formEditor = new FormEditor(inputPdfPath, outputPdfPath))
        {
            foreach (var kvp in renameMap)
            {
                try
                {
                    // Rename each field according to the mapping
                    formEditor.RenameField(kvp.Key, kvp.Value);
                    Console.WriteLine($"Renamed field '{kvp.Key}' to '{kvp.Value}'.");
                }
                catch (Exception ex)
                {
                    // Log but continue with other fields
                    Console.Error.WriteLine($"Failed to rename '{kvp.Key}': {ex.Message}");
                }
            }

            // Persist changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Field renaming completed. Output saved to '{outputPdfPath}'.");
    }
}