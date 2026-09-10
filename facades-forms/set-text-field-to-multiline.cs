using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Convert the "Comments" text field to a multiline field
        bool success = formEditor.Single2Multiple("Comments");
        if (!success)
        {
            Console.Error.WriteLine("Unable to set multiline property for field 'Comments'.");
        }

        // Persist changes to the output PDF
        formEditor.Save();

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}