using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Address";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Set the horizontal alignment of the "Address" field to center
            bool success = editor.SetFieldAlignment(fieldName, FormFieldFacade.AlignCenter);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set alignment for field '{fieldName}'.");
            }

            // Persist changes to the output file
            editor.Save();
        }

        Console.WriteLine($"Field '{fieldName}' alignment set to center. Output saved to '{outputPath}'.");
    }
}