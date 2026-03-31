using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        FormEditor editor = new FormEditor(inputPath, outputPath);
        editor.Facade = new FormFieldFacade();
        // Set custom appearance for the field
        editor.Facade.BorderColor = Color.DarkBlue;
        editor.Facade.BackgroundColor = Color.LightYellow;
        editor.Facade.CustomFont = "Arial"; // non‑standard font name
        editor.Facade.FontSize = 12;
        editor.Facade.TextColor = Color.Black;
        editor.Facade.Alignment = FormFieldFacade.AlignCenter;

        // Apply the appearance to the specific field "CustomerName"
        editor.DecorateField("CustomerName");
        editor.Save();

        Console.WriteLine($"Field 'CustomerName' decorated and saved to {outputPath}");
    }
}