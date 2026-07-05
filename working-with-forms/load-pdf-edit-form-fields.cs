using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable; wrap it in a using block for deterministic cleanup
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF into the editor
            formEditor.BindPdf(inputPdf);

            // OPTIONAL: modify form fields, e.g. set a value for a field named "Name"
            // formEditor.SetFieldValue("Name", "John Doe");

            // Save the modified PDF to the output file
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Form-modified PDF saved to '{outputPdf}'.");
    }
}