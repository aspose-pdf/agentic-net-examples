using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output34.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to a FormEditor instance and apply facade settings
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Set a uniform font size for all form fields via the facade
            editor.Facade.FontSize = 10F; // float value

            // Decorate every ListBox field with the facade appearance
            editor.DecorateField(FieldType.ListBox);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
