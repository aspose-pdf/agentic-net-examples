using System;
using System.IO;
using System.Drawing; // System.Drawing.Color is required for FormFieldFacade
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with FormEditor and configure the field appearance via the Facade
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Set the desired appearance on the Facade
            editor.Facade.Alignment = FormFieldFacade.AlignJustified; // int constant, not an enum
            editor.Facade.BorderColor = Color.Black;                 // System.Drawing.Color
            editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // float constant (or 1.0F)

            // Apply the appearance to the specific field "Text2"
            editor.DecorateField("Text2");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aligned PDF saved to '{outputPath}'.");
    }
}
