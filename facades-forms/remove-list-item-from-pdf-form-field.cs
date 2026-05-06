using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Delete the list item "Option B" from the list field "Choices"
                formEditor.DelListItem("Choices", "Option B");

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"List item removed. Saved to '{outputPath}'.");
    }
}