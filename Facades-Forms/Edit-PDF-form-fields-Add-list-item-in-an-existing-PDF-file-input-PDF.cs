using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Name of the list box field and the item to be added
        const string listBoxFieldName = "MyListBox";
        const string newItem = "New Option";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Initialize the FormEditor facade and bind it to the PDF
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPdfPath);

                // Add a new item to the specified list box field
                formEditor.AddListItem(listBoxFieldName, newItem);

                // Save the modified PDF using the provided save rule
                formEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully added item '{newItem}' to list box '{listBoxFieldName}'.");
            Console.WriteLine($"Modified PDF saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}