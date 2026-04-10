using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Initialize FormEditor with source and destination PDF files
        FormEditor editor = new FormEditor(inputPdf, outputPdf);

        // Configure visual appearance for the fields
        editor.Facade = new FormFieldFacade();
        editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow;   // uniform background
        editor.Facade.BorderColor     = System.Drawing.Color.DarkGray;      // uniform border color
        editor.Facade.BorderStyle     = FormFieldFacade.BorderStyleSolid;   // solid border
        editor.Facade.BorderWidth     = FormFieldFacade.BorderWidthMedium;  // medium width

        // Apply the appearance to all list-type fields (list boxes and combo boxes)
        editor.DecorateField(FieldType.ListBox);
        editor.DecorateField(FieldType.ComboBox);

        // Persist the changes to the output PDF
        editor.Save();
    }
}