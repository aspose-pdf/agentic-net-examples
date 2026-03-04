using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing a list box field
        const string inputPath = "input.pdf";
        // Output PDF after the list item is removed
        const string outputPath = "output.pdf";
        // Fully qualified name of the list box field
        const string listFieldName = "listboxField";
        // The exact item text to delete from the list
        const string itemToDelete = "item2";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create FormEditor facade, bind the PDF, delete the list item, and save
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document into the facade
                editor.BindPdf(inputPath);

                // Delete the specified item from the list field
                editor.DelListItem(listFieldName, itemToDelete);

                // Save the modified PDF to a new file
                editor.Save(outputPath);
            }

            Console.WriteLine($"Item '{itemToDelete}' removed from field '{listFieldName}'. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}