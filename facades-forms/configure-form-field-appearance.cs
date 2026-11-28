using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output14.pdf";
        const string fieldName = "CustomerName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to FormEditor, configure the facade, decorate the field and save.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPath);

            // Configure appearance on the editor's Facade (System.Drawing.Color is required).
            editor.Facade.FontSize = 12F;
            editor.Facade.BorderColor = Color.Black;
            editor.Facade.BackgroundColor = Color.LightGray;

            // Apply the appearance to the specific field.
            editor.DecorateField(fieldName);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
