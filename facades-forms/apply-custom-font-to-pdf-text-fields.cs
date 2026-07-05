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

        // FormEditor loads the PDF (inputPath) and saves the result (outputPath) via its constructor.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Assign a FormFieldFacade to modify visual attributes of form fields.
            editor.Facade = new FormFieldFacade();

            // Specify the custom font name to be used for non‑standard fonts.
            editor.Facade.CustomFont = "Arial Bold";

            // Apply the custom font to all text fields in the document.
            editor.DecorateField(FieldType.Text);
        }

        Console.WriteLine($"All text fields updated with Arial Bold and saved to '{outputPath}'.");
    }
}