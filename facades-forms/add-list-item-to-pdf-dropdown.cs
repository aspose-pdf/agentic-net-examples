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
            // Add a new option "Option A" to the combo box (dropdown) named "Choices"
            // For combo boxes the overload expects an array: { label, exportValue }
            formEditor.AddListItem("Choices", new string[] { "Option A", "Option A" });

            // Persist changes to the output file
            formEditor.Save();
        }

        Console.WriteLine($"Dropdown field updated and saved to '{outputPath}'.");
    }
}