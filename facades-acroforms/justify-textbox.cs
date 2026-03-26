using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "justified_form.pdf";
        const string fieldName = "myTextBox";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        FormEditor editor = new FormEditor(inputPath, outputPath);
        editor.Facade = new FormFieldFacade();
        editor.Facade.Alignment = FormFieldFacade.AlignJustified;
        editor.DecorateField(fieldName);

        Console.WriteLine($"Justified textbox saved to '{outputPath}'.");
    }
}
