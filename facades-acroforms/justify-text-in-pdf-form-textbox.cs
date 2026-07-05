using System;
using System.IO;
using Aspose.Pdf.Facades; // FormEditor, FormFieldFacade

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // existing PDF with a filled textbox
        const string outputPdf = "output_form.pdf";  // result PDF
        const string fieldName = "TextBox1";         // fully qualified name of the textbox field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor constructor that accepts input and output file paths
        using (Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor(inputPdf, outputPdf))
        {
            // Configure visual attributes via FormFieldFacade
            formEditor.Facade = new Aspose.Pdf.Facades.FormFieldFacade
            {
                // Set text alignment to justified
                Alignment = Aspose.Pdf.Facades.FormFieldFacade.AlignJustified
            };

            // Apply the alignment to the specified field
            formEditor.DecorateField(fieldName);

            // Persist changes to the output file
            formEditor.Save();
        }

        Console.WriteLine($"Justified textbox saved to '{outputPdf}'.");
    }
}