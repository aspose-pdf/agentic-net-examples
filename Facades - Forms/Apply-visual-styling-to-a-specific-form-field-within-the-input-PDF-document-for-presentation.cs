using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "styled_output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with input and output PDF files
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Configure visual appearance via FormFieldFacade
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;
            editor.Facade.TextColor = System.Drawing.Color.DarkBlue;
            editor.Facade.BorderColor = System.Drawing.Color.Green;
            editor.Facade.BorderWidth = FormFieldFacade.BorderWidthMedium;
            editor.Facade.Alignment = FormFieldFacade.AlignCenter;

            // Apply the styling to the specified form field
            editor.DecorateField(fieldName);

            // Persist changes to the output file
            editor.Save();
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}