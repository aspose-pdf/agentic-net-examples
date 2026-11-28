using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output11.pdf";
        const string fieldName  = "CountryList";
        const string newItem    = "USA";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the PDF document to the editor.
            editor.BindPdf(inputPath);

            // Add the new list item to the specified list field.
            editor.AddListItem(fieldName, newItem);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Added \"{newItem}\" to list field \"{fieldName}\" and saved as \"{outputPath}\".");
    }
}