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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        FormFieldFacade facade = new FormFieldFacade();
        facade.BackgroundColor = System.Drawing.Color.LightGreen;
        editor.Facade = facade;

        editor.DecorateField("Status");
        editor.Save(outputPath);

        Console.WriteLine($"Field 'Status' background set to light green. Saved to '{outputPath}'.");
    }
}
