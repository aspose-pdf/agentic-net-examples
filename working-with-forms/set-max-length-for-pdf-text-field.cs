using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_limited.pdf";
        const string fieldName  = "CustomerName";
        const int    maxLength  = 50;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so no using block is needed.
        FormEditor formEditor = new FormEditor();

        // Load the PDF document into the editor.
        formEditor.BindPdf(inputPath);

        // Set a character limit of 50 on the specified form field.
        formEditor.SetFieldLimit(fieldName, maxLength);

        // Save the modified PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Field '{fieldName}' limited to {maxLength} characters. Saved to '{outputPath}'.");
    }
}