using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for FieldType enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is a facade that implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Create a FormFieldFacade to define visual attributes.
            editor.Facade = new FormFieldFacade();

            // Set appearance properties. System.Drawing.Color is required by the API.
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;
            editor.Facade.TextColor       = System.Drawing.Color.DarkBlue;
            editor.Facade.BorderColor     = System.Drawing.Color.Gray;
            editor.Facade.Alignment       = FormFieldFacade.AlignCenter;

            // Apply the defined appearance to all text fields in the PDF.
            editor.DecorateField(FieldType.Text);

            // Save the modified PDF to the output file specified in the constructor.
            editor.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}