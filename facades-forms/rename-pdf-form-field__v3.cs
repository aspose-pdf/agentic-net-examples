using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output6.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a FormEditor instance (does not implement IDisposable).
        FormEditor formEditor = new FormEditor();

        // Bind the PDF document to the editor.
        formEditor.BindPdf(inputPath);

        // Rename the form field from CustomerName to ClientName.
        formEditor.RenameField("CustomerName", "ClientName");

        // Save the updated PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Field renamed and saved to '{outputPath}'.");
    }
}