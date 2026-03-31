using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with input and output PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        // Add a new option "Option A" to the dropdown field named "Choices"
        formEditor.AddListItem("Choices", "Option A");
        // Save the modified PDF
        formEditor.Save();

        Console.WriteLine($"Dropdown field updated and saved to '{outputPath}'.");
    }
}