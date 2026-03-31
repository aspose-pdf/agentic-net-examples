using System;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "decorated.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            FormEditor editor = new FormEditor(inputPath, outputPath);
            editor.Facade = new FormFieldFacade();
            editor.Facade.Alignment = FormFieldFacade.AlignCenter;
            editor.DecorateField(FieldType.Text);
            editor.Save();
            Console.WriteLine($"All text fields decorated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
