using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output29.pdf";
        const string fieldName = "DepartmentList";
        const string itemToRemove = "Finance";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is the correct facade for editing form structure.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Remove the specified list item from the list field.
            // DelListItem removes a single entry; if the item does not exist the call is ignored.
            editor.DelListItem(fieldName, itemToRemove);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
