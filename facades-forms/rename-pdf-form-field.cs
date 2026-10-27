using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldFieldName = "OldName";
        const string newFieldName = "NewName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so no using block is needed.
        FormEditor editor = new FormEditor();

        // Load the PDF document.
        editor.BindPdf(inputPath);

        // Rename the form field throughout the document.
        editor.RenameField(oldFieldName, newFieldName);

        // Save the updated PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Field renamed from '{oldFieldName}' to '{newFieldName}' and saved to '{outputPath}'.");
    }
}