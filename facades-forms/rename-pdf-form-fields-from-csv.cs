using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string csvPath   = "mapping.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"Mapping CSV not found: {csvPath}");
            return;
        }

        // FormEditor works directly with source and destination PDF files.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Each CSV line: oldFieldName,newFieldName
            foreach (string line in File.ReadLines(csvPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] parts = line.Split(',');
                if (parts.Length < 2)
                    continue; // malformed line

                string oldName = parts[0].Trim();
                string newName = parts[1].Trim();

                // Rename the field using the FormEditor API.
                formEditor.RenameField(oldName, newName);
            }

            // Persist the changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Field renaming completed. Output saved to '{outputPdf}'.");
    }
}