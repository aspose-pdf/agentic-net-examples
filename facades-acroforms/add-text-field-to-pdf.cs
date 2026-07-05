using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FieldType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source PDF and destination PDF
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Add a text field named "NewTextField" on page 1.
        // Coordinates: lower‑left (100, 500), upper‑right (200, 520)
        bool success = formEditor.AddField(FieldType.Text, "NewTextField", 1, 100f, 500f, 200f, 520f);
        if (!success)
        {
            Console.Error.WriteLine("Failed to add the text field.");
        }

        // Persist changes to the output file
        formEditor.Save();

        // Release resources
        formEditor.Close();

        Console.WriteLine($"Text field added and saved to '{outputPath}'.");
    }
}