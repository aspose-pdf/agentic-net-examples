using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the original field
        const string outputPdf = "output.pdf";  // PDF that will contain the duplicated field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor works with two files: source and destination.
        // It loads the source PDF, performs form operations, and saves to the destination.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Copy the existing field "Signature" to a new field named "SignatureCopy"
            // on the same page as the original. Page number -1 means "same page".
            formEditor.CopyInnerField("Signature", "SignatureCopy", -1);

            // Persist changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Field duplicated successfully. Output saved to '{outputPdf}'.");
    }
}