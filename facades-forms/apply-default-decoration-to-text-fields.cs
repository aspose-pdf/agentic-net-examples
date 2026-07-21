using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDFs
        FormEditor editor = new FormEditor(inputPath, outputPath);

        // Set visual attributes for text fields via FormFieldFacade
        editor.Facade = new FormFieldFacade();
        editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;
        editor.Facade.TextColor       = System.Drawing.Color.DarkBlue;
        editor.Facade.BorderColor     = System.Drawing.Color.Gray;
        editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

        // Apply the decoration to all fields of type Text
        editor.DecorateField(FieldType.Text);

        // Persist changes
        editor.Save();

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}