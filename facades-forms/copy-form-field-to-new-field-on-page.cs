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

        // Initialize FormEditor and bind the source PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Copy the outer definition of "AddressBlock" to a new field "BillingAddress" on page 3
            // CopyInnerField creates a copy with a new name on the specified page.
            formEditor.CopyInnerField("AddressBlock", "BillingAddress", 3);

            // Save the modified document
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}