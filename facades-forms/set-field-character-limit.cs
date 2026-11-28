using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "survey.pdf";
        const string outputPath = "limited.pdf";
        const string fieldName = "comment"; // name of the form field to limit

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor works directly on PDF files; it implements IDisposable, so use a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF into the editor.
            editor.BindPdf(inputPath);

            // Set the maximum number of characters allowed in the specified field.
            // The correct FormEditor method is SetFieldLimit(name, maxLength).
            editor.SetFieldLimit(fieldName, 50);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Limited PDF saved to '{outputPath}'.");
    }
}
