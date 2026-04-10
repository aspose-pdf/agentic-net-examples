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

        // FormEditor works with a source PDF and writes changes to a destination PDF.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Copy the existing field "Signature" to a new field "SignatureCopy" on the same page.
            // pageNum = -1 means the new field will be placed on the same page as the original.
            formEditor.CopyInnerField("Signature", "SignatureCopy", -1);

            // Persist the modifications.
            formEditor.Save();
        }

        Console.WriteLine($"Field 'Signature' duplicated as 'SignatureCopy' in '{outputPdf}'.");
    }
}