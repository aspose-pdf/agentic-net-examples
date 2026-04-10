using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the filled textbox
        const string outputPdf = "output.pdf";  // PDF after applying justified alignment
        const string fieldName = "MyTextBox";   // fully qualified name of the textbox field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so use a using block for deterministic cleanup
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPdf);

            // Configure visual attributes via FormFieldFacade
            editor.Facade = new FormFieldFacade
            {
                // Set text alignment to justified
                Alignment = FormFieldFacade.AlignJustified
            };

            // Apply the facade settings to the specific field
            editor.DecorateField(fieldName);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Justified textbox saved to '{outputPdf}'.");
    }
}