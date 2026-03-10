using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "styled_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a FormEditor facade and bind the source PDF.
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Configure visual attributes via the FormFieldFacade.
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;
            editor.Facade.TextColor       = System.Drawing.Color.DarkBlue;
            editor.Facade.BorderColor     = System.Drawing.Color.Green;
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

            // Apply the styling to all fields of the specified type (e.g., Text fields).
            editor.DecorateField(FieldType.Text);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}