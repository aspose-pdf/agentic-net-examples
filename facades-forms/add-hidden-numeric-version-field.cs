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

        // Add a hidden numeric field named "Version" with value "2" on page 1
        // Position coordinates: lower‑left (10,10), upper‑right (50,30)
        // Note: Aspose.Pdf.Facades.FieldType.Numeric creates a numeric field.
        formEditor.AddField(FieldType.Numeric, "Version", "2", 1, 10f, 10f, 50f, 30f);

        // Save the modified PDF
        formEditor.Save();

        Console.WriteLine($"PDF saved with hidden version field to '{outputPath}'.");
    }
}