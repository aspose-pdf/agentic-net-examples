using System;
using System.IO;
using Aspose.Pdf.Facades;          // FormEditor, FormFieldFacade, FieldType
using System.Drawing;             // System.Drawing.Color required by FormFieldFacade

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

        // FormEditor handles opening the source PDF and writing the decorated PDF.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Configure visual attributes for the fields via FormFieldFacade.
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = Color.LightYellow; // field background
            editor.Facade.TextColor       = Color.DarkBlue;    // text color
            editor.Facade.BorderColor     = Color.Gray;        // border color
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter; // text alignment

            // Apply the decoration to all text fields in the document.
            editor.DecorateField(FieldType.Text);

            // Persist the changes to the output file.
            editor.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}