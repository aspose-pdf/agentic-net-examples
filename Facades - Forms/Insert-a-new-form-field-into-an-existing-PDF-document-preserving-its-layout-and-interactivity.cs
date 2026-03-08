using System;
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

        // Load the source PDF and prepare to edit its form fields.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Add a new text field named "NewTextField" on page 1.
            // Rectangle coordinates: lower‑left (100, 500), upper‑right (300, 530).
            bool added = formEditor.AddField(FieldType.Text, "NewTextField", 1, 100, 500, 300, 530);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the form field.");
            }

            // Persist the changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}