using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the form
        const string outputPdf = "output.pdf";  // PDF with the multiline field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor is a SaveableFacade – wrap it in a using block for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPdf);

            // Change the "Comments" text field from single‑line to multiline
            // Single2Multiple returns true on success; ignore the return value here
            formEditor.Single2Multiple("Comments");

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Multiline field set and saved to '{outputPdf}'.");
    }
}