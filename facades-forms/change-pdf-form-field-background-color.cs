using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "SummaryBg.pdf";
        const string fieldName = "Summary";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create FormEditor with source and destination PDFs.
        var editor = new FormEditor(inputPath, outputPath);

        // Configure the appearance that will be applied to fields.
        // Light yellow background (RGB 0.9, 0.9, 0.5) => (230, 230, 128).
        editor.Facade = new FormFieldFacade
        {
            BackgroundColor = Color.FromArgb(255, 230, 230, 128),
            BorderColor = Color.Black,
            BorderWidth = 1,
            BorderStyle = FormFieldFacade.BorderStyleSolid
        };

        // Apply the appearance to the existing "Summary" field.
        editor.DecorateField(fieldName);

        // Save the modified PDF.
        editor.Save();

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}
