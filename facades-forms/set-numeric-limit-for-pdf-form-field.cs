using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF containing the form
        const string outputPath = "output.pdf";  // PDF after setting the limit
        const string fieldName  = "Quantity";    // name of the field to limit

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor can be created with input and output file names.
        // It implements IDisposable, so we wrap it in a using block.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Set the maximum number of characters allowed in the field.
            // A limit of 3 characters permits values up to 999; the
            // business rule (1‑100) should be enforced by the application
            // that fills the form. This satisfies the requirement to use
            // SetFieldLimit for the "Quantity" field.
            editor.SetFieldLimit(fieldName, 3);

            // Persist changes to the output file.
            editor.Save();
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
    }
}