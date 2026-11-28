using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output13.pdf";
        const string fieldName = "CountryList";
        const string itemToDelete = "Mexico";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable – wrap it in a using block
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the PDF document to the editor
            formEditor.BindPdf(inputPath);

            // Delete the specified list item (if it exists)
            // FormEditor provides DelListItem for ListBox/ComboBox fields.
            formEditor.DelListItem(fieldName, itemToDelete);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
