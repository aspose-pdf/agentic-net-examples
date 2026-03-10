using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with a list field
        const string outputPdf = "output.pdf";  // PDF after the item is removed
        const string fieldName = "listboxField"; // name of the list field
        const string itemName  = "item2";        // list item to delete

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor constructor accepts input and output file paths.
        // It inherits SaveableFacade, so it can be used in a using block.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Delete the specified item from the list field.
            formEditor.DelListItem(fieldName, itemName);
        }

        Console.WriteLine($"Deleted item \"{itemName}\" from list field \"{fieldName}\".");
        Console.WriteLine($"Result saved to \"{outputPdf}\".");
    }
}