using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output073.pdf";
        const string fieldName = "CustomerName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does not implement IDisposable, so we instantiate it directly.
            FormEditor editor = new FormEditor();

            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Configure the facade that will be applied to the field.
            editor.Facade = new FormFieldFacade
            {
                // FormFieldFacade.BorderColor expects System.Drawing.Color, not Aspose.Pdf.Color.
                BorderColor = System.Drawing.Color.Blue,
                FontSize    = 12F
            };

            // Apply the decoration to the existing field.
            editor.DecorateField(fieldName);

            // Save the modified PDF.
            editor.Save(outputPath);

            // Release resources.
            editor.Close();

            Console.WriteLine($"PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
