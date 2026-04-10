using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use FormEditor (a Facades class) to modify form fields.
        // The constructor receives the source PDF and the destination PDF.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Assign a FormFieldFacade to control visual attributes of fields.
            formEditor.Facade = new FormFieldFacade();

            // Set the custom font name for non‑standard fonts.
            // "Arial Bold" must be installed on the system or embedded separately.
            formEditor.Facade.CustomFont = "Arial Bold";

            // Apply the visual changes to all text fields in the document.
            formEditor.DecorateField(FieldType.Text);
            // No explicit Save call is required; the output file is written when the
            // FormEditor is disposed (end of the using block).
        }

        Console.WriteLine($"All text fields have been updated with 'Arial Bold' and saved to '{outputPath}'.");
    }
}