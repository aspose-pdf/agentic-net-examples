using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "mapping.csv";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV not found: {csvPath}");
            return;
        }

        // Load mapping dictionary from CSV (oldName,newName per line)
        var renameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split only on the first comma to allow commas in new names
            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
                continue;

            var oldName = parts[0].Trim();
            var newName = parts[1].Trim();

            if (!string.IsNullOrEmpty(oldName))
                renameMap[oldName] = newName;
        }

        // Use FormEditor (Facades API) to bind, rename, and save
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(pdfPath);

            foreach (var kvp in renameMap)
            {
                try
                {
                    formEditor.RenameField(kvp.Key, kvp.Value);
                }
                catch (Exception ex)
                {
                    // Log but continue processing other fields
                    Console.Error.WriteLine($"Failed to rename '{kvp.Key}' to '{kvp.Value}': {ex.Message}");
                }
            }

            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Renamed fields saved to '{outputPath}'.");
    }
}
