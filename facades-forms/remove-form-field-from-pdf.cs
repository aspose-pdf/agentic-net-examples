using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "ObsoleteField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor, bind the PDF, remove the field, and save the result.
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF document.
            editor.RemoveField(fieldName);      // Delete the specified form field.
            editor.Save(outputPath);            // Persist changes to a new file.
        }

        Console.WriteLine($"Removed field '{fieldName}' and saved to '{outputPath}'.");
    }
}