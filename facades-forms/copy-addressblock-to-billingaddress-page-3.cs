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

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Copy the outer definition of the field "AddressBlock" to a new field
            // named "BillingAddress" on page 3 (pages are 1‑based).
            formEditor.CopyInnerField("AddressBlock", "BillingAddress", 3);

            // Save the modified document
            formEditor.Save();
        }

        Console.WriteLine($"Field copied successfully. Output saved to '{outputPdf}'.");
    }
}