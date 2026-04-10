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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        // Initialize FormEditor on the loaded document
        using (FormEditor editor = new FormEditor(doc))
        {
            // Set custom border thickness (2 points) for the field
            editor.Facade.BorderWidth = 2f;

            // Apply the visual changes to the specific field named "Signature"
            editor.DecorateField("Signature");

            // Example usage of SetFieldAttribute (make the field read‑only)
            editor.SetFieldAttribute("Signature", PropertyFlag.ReadOnly);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}