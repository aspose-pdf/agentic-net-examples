using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with a form
        const string outputPdf = "decorated.pdf";      // result PDF
        const string fieldName = "TargetField";        // fully‑qualified name of the field to decorate

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so use a using block for deterministic cleanup
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Create a facade that holds visual attributes for the field.
            // No color properties are set to avoid System.Drawing usage; defaults will be applied.
            editor.Facade = new FormFieldFacade();

            // Apply the visual attributes to the specified field.
            editor.DecorateField(fieldName);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field '{fieldName}' decorated and saved to '{outputPdf}'.");
    }
}