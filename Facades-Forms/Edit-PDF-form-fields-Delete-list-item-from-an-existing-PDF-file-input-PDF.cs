using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF containing the list box field
        const string inputPdfPath = "input.pdf";
        // Output PDF after the list item is removed
        const string outputPdfPath = "output.pdf";
        // Fully qualified name of the list box field
        const string listFieldName = "MyListBox";
        // Value of the item to delete from the list
        const string itemToDelete = "Item 3";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the FormEditor facade and bind the source PDF
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPdfPath);

                // Delete the specified item from the list box field
                formEditor.DelListItem(listFieldName, itemToDelete);

                // Save the modified PDF to the output file
                formEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"List item \"{itemToDelete}\" removed from field \"{listFieldName}\".");
            Console.WriteLine($"Modified PDF saved as \"{outputPdfPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}