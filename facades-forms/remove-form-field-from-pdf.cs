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

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements SaveableFacade, so we can use a using block for deterministic disposal
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Remove the unwanted form field
            editor.RemoveField(fieldName);

            // Save the modified PDF to a new file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Removed field '{fieldName}' and saved to '{outputPath}'.");
    }
}