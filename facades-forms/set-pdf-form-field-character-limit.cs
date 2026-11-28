using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor is in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "zipCode";
        const int    maxLength  = 5;

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block
        using (FormEditor editor = new FormEditor())
        {
            // Bind the PDF to the editor
            editor.BindPdf(inputPath);

            // Set the character limit for the specified form field
            editor.SetFieldLimit(fieldName, maxLength);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" limited to {maxLength} characters and saved to '{outputPath}'.");
    }
}