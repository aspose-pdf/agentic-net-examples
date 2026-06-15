using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit form fields using FormEditor
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Set visual attributes for the fields
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;
            editor.Facade.TextColor = System.Drawing.Color.DarkBlue;
            editor.Facade.BorderColor = System.Drawing.Color.Gray;
            editor.Facade.Alignment = FormFieldFacade.AlignCenter;

            // Apply the decoration to all text fields
            editor.DecorateField(FieldType.Text);

            // Persist the changes
            editor.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}