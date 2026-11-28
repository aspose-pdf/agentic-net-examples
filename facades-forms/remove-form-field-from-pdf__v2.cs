using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "radioOption";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor does not implement IDisposable, so no using block.
        FormEditor editor = new FormEditor();

        // Bind the PDF file to the editor.
        editor.BindPdf(inputPdf);

        // Remove the form field named "radioOption".
        editor.RemoveField(fieldName);

        // Save the modified PDF.
        editor.Save(outputPdf);

        Console.WriteLine($"Field '{fieldName}' removed and PDF saved to '{outputPdf}'.");
    }
}