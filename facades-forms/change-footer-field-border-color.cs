using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "FooterBorder.pdf";
        const string fieldName = "Footer";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF with FormEditor and decorate the specified field
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                // Configure the appearance on the editor's Facade
                editor.Facade.BorderColor = System.Drawing.Color.Gray;               // System.Drawing.Color
                editor.Facade.BorderWidth = 1f;                                      // float, 1 point
                editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid;       // int constant

                // Apply the appearance to the target field
                editor.DecorateField(fieldName);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
