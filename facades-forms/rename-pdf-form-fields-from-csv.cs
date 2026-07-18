using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_renamed.pdf";
        const string csvMapPath    = "field_mapping.csv";

        // Ensure the CSV file exists
        if (!File.Exists(csvMapPath))
        {
            Console.Error.WriteLine($"Mapping file not found: {csvMapPath}");
            return;
        }

        // FormEditor handles both opening the source PDF and writing the result
        using (FormEditor formEditor = new FormEditor(inputPdfPath, outputPdfPath))
        {
            // Read each line of the CSV: expected format "oldFieldName,newFieldName"
            foreach (string line in File.ReadLines(csvMapPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(',');
                if (parts.Length < 2)
                {
                    Console.Error.WriteLine($"Invalid mapping line (ignored): {line}");
                    continue;
                }

                string oldName = parts[0].Trim();
                string newName = parts[1].Trim();

                // Rename the field using the FormEditor API
                formEditor.RenameField(oldName, newName);
            }

            // Persist the changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Field renaming completed. Output saved to '{outputPdfPath}'.");
    }
}