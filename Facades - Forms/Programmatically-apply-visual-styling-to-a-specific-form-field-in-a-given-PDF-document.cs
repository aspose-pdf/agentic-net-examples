using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "styled_output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor editor = new FormEditor(inputPdf, outputPdf);

        // Configure visual attributes via FormFieldFacade (avoid System.Drawing.Color)
        editor.Facade = new FormFieldFacade();
        editor.Facade.Alignment   = FormFieldFacade.AlignCenter;      // center text
        editor.Facade.BorderWidth = FormFieldFacade.BorderWidthMedium; // medium border

        // Apply the configured styling to the specified form field
        editor.DecorateField(fieldName);

        // Persist the changes to the output PDF
        editor.Save();

        Console.WriteLine($"Styled field '{fieldName}' saved to '{outputPdf}'.");
    }
}