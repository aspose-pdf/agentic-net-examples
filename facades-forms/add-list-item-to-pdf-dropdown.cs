using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Choices";
        const string newItem = "Option A";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is the correct facade class for editing form fields.
        // It implements IDisposable, so we use a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPath);

            // Add a new list item to the dropdown (combo box) field.
            // The field is identified by its name ("Choices").
            // The method works regardless of the page the field resides on.
            formEditor.AddListItem(fieldName, newItem);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Added \"{newItem}\" to dropdown \"{fieldName}\" on page 4 and saved to '{outputPath}'.");
    }
}
