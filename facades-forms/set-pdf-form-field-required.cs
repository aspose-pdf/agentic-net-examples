using System;
using System.IO;
using Aspose.Pdf;               // PropertyFlag enum
using Aspose.Pdf.Facades;      // FormEditor class

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Agreement";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF for form editing; specify source and destination files
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Mark the field as required (displays the asterisk indicator)
            bool success = editor.SetFieldAttribute(fieldName, PropertyFlag.Required);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set Required attribute on field '{fieldName}'.");
            }

            // Persist changes
            editor.Save();
        }

        Console.WriteLine($"Field '{fieldName}' set to required. Output saved to '{outputPath}'.");
    }
}