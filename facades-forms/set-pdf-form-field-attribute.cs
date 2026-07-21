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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // NOTE: FormEditor.SetFieldAttribute supports only predefined PropertyFlag values
        // (NoExport, ReadOnly, Required). Setting an arbitrary custom attribute such as
        // "data-id" is not supported by this API. As a workaround you can set one of the
        // available flags; here we set the field as required as an example.
        formEditor.SetFieldAttribute("OrderNumber", PropertyFlag.Required);

        // Save the modified PDF
        formEditor.Save();

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}