using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "CustomerName";
        const int maxLength = 50;

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, set the character limit on the specified field, and save
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Apply a 50‑character limit to the 'CustomerName' field
            editor.SetFieldLimit(fieldName, maxLength);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' limited to {maxLength} characters. Saved to '{outputPath}'.");
    }
}