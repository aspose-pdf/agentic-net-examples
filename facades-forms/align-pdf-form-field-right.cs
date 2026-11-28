using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output32.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; use a using block
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Configure the facade – alignment to the right
            editor.Facade.Alignment = FormFieldFacade.AlignRight; // int constant defined by the API
            // Visual appearance – fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
            editor.Facade.BorderColor = System.Drawing.Color.Black;
            editor.Facade.BackgroundColor = System.Drawing.Color.White;
            editor.Facade.TextColor = System.Drawing.Color.Black;

            // Apply the facade to the existing field named "Comments"
            editor.DecorateField("Comments");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
