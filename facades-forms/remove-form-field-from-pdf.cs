using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "ObsoleteField";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor implements SaveableFacade, which is disposable.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF file.
            formEditor.BindPdf(inputPdf);

            // Remove the specified form field.
            formEditor.RemoveField(fieldName);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"{fieldName}\" removed. Output saved to '{outputPdf}'.");
    }
}