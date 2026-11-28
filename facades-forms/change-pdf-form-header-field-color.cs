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
        const string outputPath = "ColorUpdated.pdf";
        const string fieldName = "Header";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so we use a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Configure the appearance that will be applied to the field.
            editor.Facade.BorderColor = System.Drawing.Color.Blue;               // fully qualified to avoid ambiguity
            editor.Facade.BackgroundColor = System.Drawing.Color.Blue;           // fully qualified to avoid ambiguity
            // Optional: set border width and style.
            editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // float constant
            editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid; // int constant

            // Apply the decoration to the specific field.
            editor.DecorateField(fieldName);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' color updated and saved to '{outputPath}'.");
    }
}
