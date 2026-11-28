using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output091.pdf";
        const string fieldName = "CountryList";
        const string itemToRemove = "Canada";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; use a using block for deterministic disposal
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPath);

            // Remove the item "Canada" from the list box.
            // DelListItem silently does nothing if the item is not present.
            editor.DelListItem(fieldName, itemToRemove);

            // Save the modified PDF to the specified output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
