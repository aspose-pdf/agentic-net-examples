using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF with a form
        const string outputPdf = "styled_output.pdf"; // destination PDF
        const string fieldName = "myTextField";        // fully‑qualified name of the field to style

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works directly with file paths; it does not implement IDisposable.
        FormEditor editor = new FormEditor(inputPdf, outputPdf);

        // Configure visual attributes via FormFieldFacade.
        editor.Facade = new FormFieldFacade();

        // Apply styling (avoid System.Drawing.Color as per global rule).
        editor.Facade.Alignment    = FormFieldFacade.AlignCenter;   // center the text
        editor.Facade.BorderWidth  = FormFieldFacade.BorderWidthMedium; // medium border
        // Additional styling can be set here (e.g., BackgroundColor, TextColor) if needed.

        // Apply the styling to the specific field.
        editor.DecorateField(fieldName);

        // Persist the changes.
        editor.Save();

        Console.WriteLine($"Styled PDF saved to '{outputPdf}'.");
    }
}