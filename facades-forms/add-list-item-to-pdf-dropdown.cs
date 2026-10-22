using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor (Aspose.Pdf.Facades) to modify form fields.
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Add a new list item "Option A" to the dropdown field named "Choices".
            // The AddListItem overload accepts only the field name and the item value.
            editor.AddListItem("Choices", "Option A");

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Dropdown field updated and saved to '{outputPath}'.");
    }
}
