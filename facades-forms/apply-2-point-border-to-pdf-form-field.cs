using System;
using System.IO;
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

        // Load the PDF using FormEditor (Facades API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Set custom border thickness (2 points) for the field named "Signature"
            editor.Facade.BorderWidth = 2f; // 2 points
            editor.DecorateField("Signature"); // Apply visual changes to the specific field

            // (Optional) Example of using SetFieldAttribute for another flag
            // editor.SetFieldAttribute("Signature", PropertyFlag.Required);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}