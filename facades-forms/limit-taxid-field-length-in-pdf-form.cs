using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "invoice_form.pdf";
        const string outputPath = "taxid_limited.pdf";
        const string fieldName  = "TaxID";
        const int    maxLength  = 8;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable; instantiate directly.
        FormEditor formEditor = new FormEditor();

        // Load the PDF into the editor.
        formEditor.BindPdf(inputPath);

        // Set a character limit of 8 for the "TaxID" field.
        formEditor.SetFieldLimit(fieldName, maxLength);

        // Save the modified PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"PDF saved with field limit to '{outputPath}'.");
    }
}