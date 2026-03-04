using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Path to the source PDF
        const string outputPdf = "output.pdf";         // Path where the modified PDF will be saved
        const string fieldName = "listboxField";       // Name of the list field in the PDF form
        const string itemToDelete = "item2";           // Exact text of the list item to remove

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // FormEditor works directly with file paths: the constructor binds the input PDF
            // and prepares the output file. No explicit Save() call is required.
            FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

            // Delete the specified item from the list field.
            formEditor.DelListItem(fieldName, itemToDelete);

            Console.WriteLine($"Item \"{itemToDelete}\" removed from field \"{fieldName}\".");
            Console.WriteLine($"Modified PDF saved as \"{outputPdf}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}