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

        // FormEditor constructor binds the source PDF and sets the destination file.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Configure visual appearance for the fields.
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;   // uniform background
            editor.Facade.BorderColor     = System.Drawing.Color.DarkBlue;    // uniform border color
            editor.Facade.BorderStyle     = FormFieldFacade.BorderStyleSolid; // solid border
            editor.Facade.BorderWidth     = FormFieldFacade.BorderWidthMedium; // medium width

            // Apply the appearance to all list-type fields (list boxes, combo boxes, etc.).
            editor.DecorateField(FieldType.ListBox);

            // Persist the changes.
            editor.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}