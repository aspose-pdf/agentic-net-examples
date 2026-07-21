using System;
using System.IO;
using System.Drawing;               // System.Drawing.Color is required by FormFieldFacade
using Aspose.Pdf.Facades;          // FormEditor, FormFieldFacade, FieldType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "decorated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with an input PDF and writes the result to the output path.
        using (FormEditor fe = new FormEditor(inputPdf, outputPdf))
        {
            // Configure visual appearance for the fields.
            fe.Facade = new FormFieldFacade();
            fe.Facade.BorderColor   = Color.Green;               // Uniform border color
            fe.Facade.BorderStyle   = FormFieldFacade.BorderStyleSolid; // Solid border
            fe.Facade.BorderWidth   = FormFieldFacade.BorderWidthMedium; // Medium width
            fe.Facade.BackgroundColor = Color.LightYellow;        // Uniform background
            fe.Facade.TextColor     = Color.Black;               // Optional text color

            // Apply the appearance to all list box fields in the document.
            fe.DecorateField(FieldType.ListBox);

            // Persist changes.
            fe.Save();
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPdf}'.");
    }
}