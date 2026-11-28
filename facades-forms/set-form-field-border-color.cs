using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output15.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create FormEditor and bind the PDF document.
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Set appearance on the editor's Facade. BorderColor expects System.Drawing.Color.
        editor.Facade.BorderColor = System.Drawing.Color.Red;
        // Additional appearance settings can be configured here if needed, e.g.:
        // editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin;
        // editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid;

        // Apply the configured appearance to the specific field.
        editor.DecorateField("EmailAddress");

        // Save the updated PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
