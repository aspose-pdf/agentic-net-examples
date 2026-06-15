using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Choices";
        const string itemName   = "Option B";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable – use using for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // Delete the specified list item from the field
            formEditor.DelListItem(fieldName, itemName);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Deleted \"{itemName}\" from list field \"{fieldName}\". Saved to \"{outputPath}\".");
    }
}