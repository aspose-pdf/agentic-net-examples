using System;
using System.IO;
using Aspose.Pdf;
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

        // Edit the form using FormEditor (facade API)
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // Remove the item "Option B" from the list field named "Choices"
            formEditor.DelListItem("Choices", "Option B");

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"List item removed and saved to '{outputPath}'.");
    }
}