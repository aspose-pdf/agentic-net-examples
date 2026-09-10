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

        // FormEditor works with a source PDF and a destination PDF.
        // Here we copy the existing field "Signature" to a new field
        // named "SignatureCopy" on the same page (pageNum = -1).
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // CopyInnerField(fieldName, newFieldName, pageNum)
            // pageNum = -1 → keep the field on the original page.
            editor.CopyInnerField("Signature", "SignatureCopy", -1);
            editor.Save(); // Persist changes to outputPdf
        }

        Console.WriteLine($"Field duplicated successfully: {outputPdf}");
    }
}