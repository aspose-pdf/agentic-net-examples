using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "feedback"; // name of the form field to limit

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so a plain instance is sufficient.
        FormEditor editor = new FormEditor();

        // Load the PDF into the editor.
        editor.BindPdf(inputPath);

        // Set the maximum character limit for the specified field to 100.
        editor.SetFieldLimit(fieldName, 100);

        // Save the updated PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Field '{fieldName}' limit set to 100 characters. Saved to '{outputPath}'.");
    }
}