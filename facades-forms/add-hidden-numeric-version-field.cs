using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor and bind the existing PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Add a hidden numeric field named "Version" with initial value "2"
            // Placed at a zero‑size rectangle on page 1 (effectively invisible)
            formEditor.AddField(FieldType.Numeric, "Version", "2", 1, 0, 0, 0, 0);

            // No need to set a hidden flag – the zero‑size rectangle makes the field invisible.

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Hidden field added and saved to '{outputPath}'.");
    }
}
