using System;
using System.IO;
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

        // Bind the PDF document to a FormEditor instance
        Aspose.Pdf.Facades.FormEditor editor = new Aspose.Pdf.Facades.FormEditor();
        editor.BindPdf(inputPath);

        // Configure visual attributes for the field
        Aspose.Pdf.Facades.FormFieldFacade facade = new Aspose.Pdf.Facades.FormFieldFacade();
        facade.BackgroundColor = Color.LightGray;          // light gray background
        facade.CustomFont = "Helvetica-Oblique";           // italic font style
        facade.TextColor = Color.Black;                    // optional text color
        facade.FontSize = 12;                              // optional font size

        editor.Facade = facade;

        // Apply the styling to the field named "Footer"
        editor.DecorateField("Footer");

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}