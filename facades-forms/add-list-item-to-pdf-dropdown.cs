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

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Add a new option "Option A" to the dropdown (combo box) named "Choices"
            formEditor.AddListItem("Choices", "Option A");

            // Persist changes
            formEditor.Save();
        }

        Console.WriteLine($"List item added and saved to '{outputPath}'.");
    }
}