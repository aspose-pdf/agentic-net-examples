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

        // Load rename mapping from CSV (format: oldFieldName,newFieldName)
        var renameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(csvPath))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
                continue;

            var oldName = parts[0].Trim();
            var newName = parts[1].Trim();

            if (!string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName))
                renameMap[oldName] = newName;
        }

        // Use FormEditor (Aspose.Pdf.Facades) to rename form fields
        FormEditor editor = new FormEditor();
        editor.BindPdf(pdfPath);

        foreach (var kvp in renameMap)
        {
            // RenameField silently skips non‑existent fields
            editor.RenameField(kvp.Key, kvp.Value);
        }

        editor.Save(outputPath);
        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }
}