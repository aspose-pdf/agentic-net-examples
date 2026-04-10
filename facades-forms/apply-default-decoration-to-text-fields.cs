using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "decorated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Create a facade that defines the visual appearance for fields
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow; // field background
            editor.Facade.TextColor       = System.Drawing.Color.DarkBlue;   // text color
            editor.Facade.BorderColor     = System.Drawing.Color.Gray;       // border color
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;    // text alignment

            // Apply the facade to all text fields in the document
            editor.DecorateField(FieldType.Text);

            // Persist the changes to the output PDF
            editor.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}