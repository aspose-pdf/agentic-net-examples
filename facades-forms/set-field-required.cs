using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Mark the field "Agreement" as required (asterisk will be shown)
        bool success = formEditor.SetFieldAttribute("Agreement", PropertyFlag.Required);
        Console.WriteLine(success ? "Field 'Agreement' set to required." : "Failed to set field attribute.");
    }
}
