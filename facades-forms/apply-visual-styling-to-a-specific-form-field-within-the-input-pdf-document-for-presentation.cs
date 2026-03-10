using System;
using System.IO;
using System.Drawing;               // For System.Drawing.Color used by FormFieldFacade
using Aspose.Pdf.Facades;          // FormEditor and FormFieldFacade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "styled_output.pdf";
        const string fieldName  = "myTextField";   // Fully qualified field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor binds the source PDF and the target PDF in one step
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Configure visual appearance via FormFieldFacade
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = Color.LightYellow;
            editor.Facade.TextColor       = Color.DarkBlue;
            editor.Facade.BorderColor     = Color.Green;
            editor.Facade.BorderWidth     = FormFieldFacade.BorderWidthMedium;
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

            // Apply the styling to the specified field
            editor.DecorateField(fieldName);

            // Persist changes
            editor.Save();
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}