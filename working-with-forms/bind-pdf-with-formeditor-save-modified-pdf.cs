using System;
using System.IO;
using Aspose.Pdf.Facades; // FormEditor resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with form fields
        const string outputPdf = "output.pdf";  // PDF after modifications

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block for deterministic cleanup
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF to the editor
            editor.BindPdf(inputPdf);

            // Example modification: set a form field value (replace with actual field name)
            // editor.SetFieldValue("FieldName", "New Value");

            // Save the modified PDF to the output path
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Form-modified PDF saved to '{outputPdf}'.");
    }
}