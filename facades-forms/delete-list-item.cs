using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Choices";
        const string itemToDelete = "Option B";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        // Delete the specified option from the list field
        formEditor.DelListItem(fieldName, itemToDelete);
        // Release resources
        formEditor.Close();

        Console.WriteLine($"Item \"{itemToDelete}\" removed from field \"{fieldName}\". Saved to {outputPath}");
    }
}
