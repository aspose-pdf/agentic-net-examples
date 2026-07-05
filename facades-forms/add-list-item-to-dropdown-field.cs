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
            // Add a new option to the dropdown (combo box) field named "Choices"
            // For combo boxes the overload with a string array (label, export value) is required
            formEditor.AddListItem("Choices", new string[] { "Option A", "Option A" });

            // Save the modified PDF
            formEditor.Save();
        }

        Console.WriteLine($"Dropdown field updated and saved to '{outputPath}'.");
    }
}