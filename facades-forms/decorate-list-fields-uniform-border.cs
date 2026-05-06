using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Color used by FormFieldFacade
using Aspose.Pdf.Facades;           // FormEditor, FormFieldFacade, FieldType

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "decorated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination files
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Configure visual appearance for the fields
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = Color.LightGray;          // Uniform background
            editor.Facade.BorderColor     = Color.DarkBlue;          // Uniform border color
            editor.Facade.BorderStyle     = FormFieldFacade.BorderStyleSolid;
            editor.Facade.BorderWidth     = FormFieldFacade.BorderWidthMedium;

            // Apply the appearance to all list-type fields (ListBox and ComboBox)
            editor.DecorateField(FieldType.ListBox);
            editor.DecorateField(FieldType.ComboBox);

            // Persist the changes
            editor.Save();
        }

        Console.WriteLine($"All list fields decorated and saved to '{outputPdf}'.");
    }
}