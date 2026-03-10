using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF containing the list field
        const string outputPath = "output.pdf";  // PDF after the item is removed
        const string fieldName  = "listboxField"; // name of the list field
        const string itemName   = "item2";        // list item to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor binds the input and output files.
        // The output file will be written when the editor is disposed.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Delete the specified item from the list field.
            editor.DelListItem(fieldName, itemName);
            // No explicit Save() call is required; disposal writes the output file.
        }

        Console.WriteLine($"Item \"{itemName}\" removed from field \"{fieldName}\". Output saved to \"{outputPath}\".");
    }
}