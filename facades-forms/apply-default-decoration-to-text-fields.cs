using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "decorated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor handles loading the PDF and saving the result.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Configure default visual attributes for text fields.
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;
            editor.Facade.TextColor       = System.Drawing.Color.DarkBlue;
            editor.Facade.BorderColor     = System.Drawing.Color.Gray;
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

            // Apply the decoration to all text fields in the document.
            editor.DecorateField(FieldType.Text);

            // Persist the changes.
            editor.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}