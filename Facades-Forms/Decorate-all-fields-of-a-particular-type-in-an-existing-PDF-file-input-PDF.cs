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

        // FormEditor constructor accepts source and destination file names.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Set visual attributes for the fields to be decorated.
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;
            editor.Facade.TextColor       = System.Drawing.Color.DarkBlue;
            editor.Facade.BorderColor     = System.Drawing.Color.Green;
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

            // Decorate all fields of the chosen type (e.g., text fields).
            editor.DecorateField(FieldType.Text);

            // Persist the changes.
            editor.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}