using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Name of the list field and the item to be removed
        const string fieldName = "listboxField";
        const string itemToDelete = "item2";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor works with form fields; the constructor that takes input and output paths
        // creates the editor, binds the PDF, and prepares the output file.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Delete the specified item from the list field.
            // This operation preserves all other content in the PDF.
            formEditor.DelListItem(fieldName, itemToDelete);
            // No explicit Save call is required; disposing the FormEditor writes the output file.
        }

        Console.WriteLine($"Item \"{itemToDelete}\" removed from field \"{fieldName}\". Output saved to \"{outputPath}\".");
    }
}