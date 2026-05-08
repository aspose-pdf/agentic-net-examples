using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the field
        const string outputPdf = "output.pdf";         // PDF after the field is removed
        const string fieldName = "ObsoleteField";      // name of the field to delete

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPdf);

            // Remove the specified form field.
            editor.RemoveField(fieldName);

            // Save the modified PDF to the output path.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"{fieldName}\" removed. Result saved to \"{outputPdf}\".");
    }
}