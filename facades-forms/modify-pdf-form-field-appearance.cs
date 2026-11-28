using System;
using System.IO;
using System.Drawing; // for Color
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "FontChanged.pdf";
        const string fieldName  = "Notes"; // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does NOT implement IDisposable, so we use a simple instance.
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Define the appearance that we want to apply to the field.
        editor.Facade = new FormFieldFacade
        {
            FontSize        = 14,                     // 14‑point font
            BackgroundColor = Color.LightGray,        // light gray background
            BorderColor     = Color.Blue,             // blue border
            BorderWidth     = 1.0f                    // 1 point border width
        };

        // Apply the facade settings to the existing field.
        editor.DecorateField(fieldName);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
