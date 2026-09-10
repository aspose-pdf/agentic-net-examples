using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Color

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

        // Initialize FormEditor with input and output PDF files
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Create a new facade to define visual attributes
            editor.Facade = new FormFieldFacade();

            // Set the background color of the field to light green
            editor.Facade.BackgroundColor = Color.LightGreen;

            // Apply the visual attributes to the specific field named "Status"
            editor.DecorateField("Status");

            // Persist the changes to the output PDF
            editor.Save();
        }

        Console.WriteLine($"Background color of field \"Status\" set to light green. Saved to '{outputPath}'.");
    }
}