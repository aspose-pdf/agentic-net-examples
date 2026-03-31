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
            // Initialize FormEditor with input and output files
            FormEditor editor = new FormEditor(inputPath, outputPath);
            // Assign a FormFieldFacade to control field appearance
            editor.Facade = new FormFieldFacade();
            // Set the custom font for text fields
            editor.Facade.CustomFont = "Arial Bold";
            // Apply the appearance to all text fields in the document
            editor.DecorateField(FieldType.Text);
        }

        Console.WriteLine($"Custom font applied and saved to '{outputPath}'.");
    }
}