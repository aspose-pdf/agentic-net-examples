using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the FormEditor facade
        using var formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // Apply an input mask to the "PhoneNumber" field.
        // FormEditor does not expose a SetFieldCustomAttribute method.
        // The usual way to add custom behaviour is to attach a JavaScript
        // snippet to the field via SetFieldScript. The script below creates a
        // custom attribute called "inputMask" that can be read by other
        // JavaScript in the PDF (or by a PDF viewer that respects it).
        string js = "this.getField('PhoneNumber').setAttribute('inputMask', '(###) ###-####');";
        formEditor.SetFieldScript("PhoneNumber", js);

        // Save the updated PDF
        formEditor.Save(outputPath);
        Console.WriteLine($"PhoneNumber field mask applied and saved to '{outputPath}'.");
    }
}
