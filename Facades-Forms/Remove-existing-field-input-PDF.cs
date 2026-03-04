using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the field
        const string outputPdf = "output.pdf";         // PDF after the field is removed
        const string fieldName = "myField";            // name of the field to delete

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor handles both loading the source PDF and specifying the output file.
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Remove the specified form field.
            editor.RemoveField(fieldName);

            // Persist changes to the output file.
            editor.Save();
        }

        Console.WriteLine($"Field \"{fieldName}\" removed. Result saved to \"{outputPdf}\".");
    }
}