using System;
using System.Drawing;               // System.Drawing.Color is required by FormFieldFacade
using Aspose.Pdf.Facades;          // FormEditor, FormFieldFacade, FieldType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing list fields
        const string outputPdf = "decorated_output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with file paths directly; it implements IDisposable.
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Create a facade that holds visual attributes for the fields.
            editor.Facade = new FormFieldFacade();

            // Uniform appearance settings for all list-type fields.
            editor.Facade.BackgroundColor = Color.LightGray;          // background fill
            editor.Facade.BorderColor     = Color.DarkBlue;          // border color
            editor.Facade.BorderStyle     = FormFieldFacade.BorderStyleSolid;
            editor.Facade.BorderWidth     = FormFieldFacade.BorderWidthMedium;

            // Apply the appearance to every ListBox field in the document.
            editor.DecorateField(FieldType.ListBox);

            // If combo boxes should also be styled, uncomment the following line:
            // editor.DecorateField(FieldType.ComboBox);

            // Persist changes to the output file.
            editor.Save();
        }

        Console.WriteLine($"List fields decorated and saved to '{outputPdf}'.");
    }
}