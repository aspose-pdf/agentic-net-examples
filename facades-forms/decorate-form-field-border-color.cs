using System;
using System.IO;
using System.Drawing; // System.Drawing.Color for facade properties
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output31.pdf";
        const string fieldName = "ZipCode";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor works directly on PDF files and implements IDisposable.
            using (FormEditor editor = new FormEditor())
            {
                // Bind the existing PDF document.
                editor.BindPdf(inputPath);

                // Configure appearance via the editor's Facade.
                editor.Facade.BorderColor = Color.Blue; // System.Drawing.Color
                // (Optional) you can also set other appearance properties, e.g.:
                // editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin;
                // editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid;

                // Apply the appearance to the specific field.
                editor.DecorateField(fieldName);

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Form field '{fieldName}' decorated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
