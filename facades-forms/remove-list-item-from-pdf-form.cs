using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is a disposable facade – use a using block for deterministic cleanup
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document into the facade
            formEditor.BindPdf(inputPath);

            // Delete the list item "Option B" from the list field named "Choices"
            // (the field is located on page 4, but the API works with the field name only)
            formEditor.DelListItem("Choices", "Option B");

            // Save the modified PDF to the output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"List item removed. Saved to '{outputPath}'.");
    }
}