using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_renamed.pdf";
        const string csvMappingPath = "field_mapping.csv";

        // ------------------------------------------------------------
        // 1. Create a sample PDF with a form field (self‑contained demo)
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            Page page = seed.Pages.Add();
            // Create a simple text box field named "OldField"
            var rect = new Rectangle(100, 600, 300, 620);
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "OldField",
                Value = "Sample"
            };
            seed.Form.Add(txtField);
            seed.Save(inputPdfPath);
        }

        // ------------------------------------------------------------
        // 2. Create a CSV mapping file (oldName,newName)
        // ------------------------------------------------------------
        File.WriteAllLines(csvMappingPath, new[] { "OldField,NewField" });

        // ------------------------------------------------------------
        // 3. Load mapping from CSV (format: oldFieldName,newFieldName per line, optional header)
        // ------------------------------------------------------------
        var renameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(csvMappingPath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Skip a possible header line (e.g., "OldName,NewName")
            if (line.TrimStart().StartsWith("Old", StringComparison.OrdinalIgnoreCase) && line.Contains(","))
                continue;

            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length == 2)
            {
                var oldName = parts[0].Trim();
                var newName = parts[1].Trim();
                if (!string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName))
                {
                    renameMap[oldName] = newName;
                }
            }
        }

        // Ensure there is something to rename
        if (renameMap.Count == 0)
        {
            Console.WriteLine("No field mappings found in the CSV file.");
            return;
        }

        // ------------------------------------------------------------
        // 4. Use FormEditor (facade) to rename fields and save the result
        //    – Load the PDF into a Document and pass it to FormEditor (non‑obsolete constructor).
        // ------------------------------------------------------------
        using (Document srcDoc = new Document(inputPdfPath))
        using (FormEditor formEditor = new FormEditor(srcDoc))
        {
            foreach (var kvp in renameMap)
            {
                // Rename each field according to the mapping
                formEditor.RenameField(kvp.Key, kvp.Value);
            }

            // Persist changes to the output PDF using the non‑obsolete overload
            formEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Field renaming completed. Output saved to '{outputPdfPath}'.");
    }
}
