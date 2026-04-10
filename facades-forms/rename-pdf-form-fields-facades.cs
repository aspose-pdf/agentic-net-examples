using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // <-- required for form field classes

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_renamed.pdf";
        const string csvPath       = "field_mapping.csv"; // CSV format: oldName,newName per line

        // Ensure the source PDF exists – if not, create a minimal sample PDF with a form field.
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdf(inputPdfPath);
            Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
        }

        // Load mapping from CSV into a dictionary
        var renameMap = LoadMapping(csvPath);
        if (renameMap.Count == 0)
        {
            Console.Error.WriteLine("No field mappings found – ensure the CSV file exists and contains entries.");
            return;
        }

        // Initialize FormEditor and bind the source PDF (use the non‑obsolete overload)
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdfPath);

            // Apply each rename operation
            foreach (var kvp in renameMap)
            {
                string oldName = kvp.Key;
                string newName = kvp.Value;

                // Rename the field using the Facades API
                formEditor.RenameField(oldName, newName);
            }

            // Persist changes to the output file using the non‑obsolete Save overload
            formEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Field renaming completed. Output saved to '{outputPdfPath}'.");
    }

    // Creates a very simple PDF containing a single text box form field named "OldField".
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add a TextBox field named "OldField"
            // The Form collection is created lazily; we can add directly.
            TextBoxField txt = new TextBoxField(page, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "OldField",
                Value = "Sample value"
            };
            doc.Form.Add(txt, 1);

            // Save the document
            doc.Save(path);
        }
    }

    // Reads a CSV file where each line contains: oldFieldName,newFieldName
    // Returns a dictionary mapping old names to new names.
    private static Dictionary<string, string> LoadMapping(string csvFilePath)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!File.Exists(csvFilePath))
        {
            Console.Error.WriteLine($"Mapping file not found: {csvFilePath}");
            return map;
        }

        foreach (var line in File.ReadLines(csvFilePath))
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Split on first comma only to allow commas in field names if needed
            var parts = line.Split(new[] { ',' }, 2);
            if (parts.Length != 2)
                continue; // malformed line – ignore

            string oldName = parts[0].Trim();
            string newName = parts[1].Trim();

            if (!string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName))
                map[oldName] = newName;
        }

        return map;
    }
}
