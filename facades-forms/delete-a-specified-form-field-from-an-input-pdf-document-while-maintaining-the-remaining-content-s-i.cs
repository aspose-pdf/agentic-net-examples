using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the form field
        const string outputPdf = "output.pdf";  // PDF after the field is removed
        const string fieldName = "FieldToDelete"; // exact name of the field to delete

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor handles loading the source PDF and writing the result.
        // It implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Remove the specified form field from the document.
            formEditor.RemoveField(fieldName);

            // Persist the changes to the output file.
            // Save(string) writes the modified PDF to the given path.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"{fieldName}\" removed. Output saved to \"{outputPdf}\".");
    }
}