using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        FormEditor editor = null;
        try
        {
            // Load the PDF and specify the output file.
            editor = new FormEditor(inputPath, outputPath);

            // Use FormFieldFacade to modify field appearance.
            editor.Facade = new FormFieldFacade();

            // Set the custom font name for non‑standard fonts.
            editor.Facade.CustomFont = "Arial Bold";

            // Apply the font (and any other visual settings) to all text fields.
            editor.DecorateField(FieldType.Text);
        }
        finally
        {
            // Ensure the editor releases all resources.
            editor?.Close();
        }

        Console.WriteLine($"All text fields updated with font 'Arial Bold' and saved to '{outputPath}'.");
    }
}