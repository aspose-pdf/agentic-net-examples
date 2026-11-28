using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice_template.pdf";
        const string outputPath = "required_date.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, instantiate directly.
        FormEditor formEditor = new FormEditor();

        // Load the PDF document.
        formEditor.BindPdf(inputPath);

        // Set the Required attribute on the form field named "InvoiceDate".
        formEditor.SetFieldAttribute("InvoiceDate", PropertyFlag.Required);

        // Save the updated PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"PDF saved with required field: '{outputPath}'.");
    }
}