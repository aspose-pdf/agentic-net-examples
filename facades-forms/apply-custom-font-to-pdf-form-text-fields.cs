using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom_font.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // FormEditor works with file paths; it will apply changes to the output file.
            using (FormEditor editor = new FormEditor(inputPath, outputPath))
            {
                // Use FormFieldFacade to specify a non‑standard font.
                editor.Facade = new FormFieldFacade();
                editor.Facade.CustomFont = "Arial Bold";

                // Apply the facade settings to all text fields in the document.
                editor.DecorateField(FieldType.Text);
            }
        }

        Console.WriteLine($"PDF saved with custom font to '{outputPath}'.");
    }
}