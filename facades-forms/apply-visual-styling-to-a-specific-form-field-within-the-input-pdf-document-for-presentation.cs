using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // FormFieldFacade uses System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "styled_output.pdf";
        const string fieldName = "myTextField"; // fully qualified field name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor binds the source PDF and sets the target output file.
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Configure visual appearance via FormFieldFacade.
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = Color.LightYellow;
            editor.Facade.TextColor       = Color.DarkBlue;
            editor.Facade.BorderColor     = Color.Green;
            editor.Facade.BorderWidth     = FormFieldFacade.BorderWidthMedium;
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

            // Apply the styling to the specified field.
            editor.DecorateField(fieldName);

            // Persist changes to the output PDF.
            editor.Save();
        }

        Console.WriteLine($"Styled PDF saved to '{outputPdf}'.");
    }
}