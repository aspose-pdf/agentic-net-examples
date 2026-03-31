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

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Set a standard attribute (NoExport) for the field "OrderNumber"
        // Note: FormEditor.SetFieldAttribute supports only predefined PropertyFlag values.
        bool result = formEditor.SetFieldAttribute("OrderNumber", PropertyFlag.NoExport);
        Console.WriteLine(result ? "Attribute applied successfully." : "Failed to apply attribute.");

        // Save the modified PDF
        formEditor.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}