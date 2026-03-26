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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Add a text field named "NewTextField" on page 1.
        // Coordinates are in points: lower‑left (100, 200), upper‑right (250, 230)
        bool fieldAdded = formEditor.AddField(FieldType.Text, "NewTextField", 1, 100f, 200f, 250f, 230f);

        if (!fieldAdded)
        {
            Console.Error.WriteLine("Failed to add the text field.");
        }

        // Persist changes to the output PDF
        formEditor.Save();
        Console.WriteLine($"Text field added and saved to '{outputPath}'.");
    }
}
