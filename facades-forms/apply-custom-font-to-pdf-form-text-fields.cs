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

        // FormEditor handles loading and saving of the PDF.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Use FormFieldFacade to modify visual attributes of form fields.
            editor.Facade = new FormFieldFacade();

            // Set the custom font name (must be installed on the system or embedded later).
            editor.Facade.CustomFont = "Arial Bold";

            // Apply the font to all text fields in the document.
            editor.DecorateField(FieldType.Text);
        }

        Console.WriteLine($"All text fields have been updated with 'Arial Bold' and saved to '{outputPath}'.");
    }
}