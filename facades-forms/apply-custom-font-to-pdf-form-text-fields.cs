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

        // FormEditor works as a facade that loads the PDF and writes the result.
        FormEditor editor = new FormEditor(inputPath, outputPath);

        // Use FormFieldFacade to modify visual attributes of form fields.
        editor.Facade = new FormFieldFacade();

        // Specify the custom font name (non‑standard font).
        editor.Facade.CustomFont = "Arial Bold";

        // Apply the facade to all text fields in the document.
        editor.DecorateField(FieldType.Text);

        // Flush changes and close the facade.
        editor.Close();

        Console.WriteLine($"All text fields have been set to Arial Bold and saved to '{outputPath}'.");
    }
}