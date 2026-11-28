using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output17.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable – use a using block
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Configure the appearance that will be applied to the fields via the Facade
            editor.Facade.FontSize = 14F;
            editor.Facade.BorderColor = System.Drawing.Color.Black;          // fully qualified System.Drawing.Color
            editor.Facade.BackgroundColor = System.Drawing.Color.LightGray; // fully qualified System.Drawing.Color
            editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // float constant
            editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid; // int constant

            // Apply the configured appearance to every text field in the document
            editor.DecorateField(Aspose.Pdf.Facades.FieldType.Text);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}