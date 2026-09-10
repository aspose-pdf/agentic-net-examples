using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF containing the form
        const string outputPath = "output.pdf";  // PDF after the item is removed
        const string fieldName  = "Choices";     // name of the list field
        const string itemName   = "Option B";    // list item to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a facade for editing AcroForm fields.
        // Bind the source PDF, delete the list item, and save the result.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Delete the specified item from the list field.
            editor.DelListItem(fieldName, itemName);

            // Write the modified PDF to the output file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Item \"{itemName}\" removed from field \"{fieldName}\". Output saved to \"{outputPath}\".");
    }
}