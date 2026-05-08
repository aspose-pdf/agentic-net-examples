using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "TextBox1"; // replace with the actual field name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor and bind the PDF document
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // Set up the visual facade with justified alignment
            FormFieldFacade facade = new FormFieldFacade();
            facade.Alignment = FormFieldFacade.AlignJustified;
            editor.Facade = facade;

            // Apply the facade to the specified text field
            editor.DecorateField(fieldName);

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Justified text saved to '{outputPdf}'.");
    }
}