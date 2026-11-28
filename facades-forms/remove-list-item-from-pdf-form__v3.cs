using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output12.pdf";
        const string fieldName = "CountryList";
        const string itemToRemove = "Canada";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create FormEditor, bind the PDF, remove the list item, and save.
        using (var editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Delete the specified item from the list box. DelListItem silently does nothing if the item is absent.
            editor.DelListItem(fieldName, itemToRemove);

            // Save the modified document.
            editor.Save(outputPath);

            // Close releases the bound document (no UnbindPdf method exists).
            editor.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
