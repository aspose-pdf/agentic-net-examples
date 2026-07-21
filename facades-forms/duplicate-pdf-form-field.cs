using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing the original "Signature" field
        const string outputPdf = "output.pdf";         // PDF that will contain the duplicated field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with two files: source and destination.
        // It copies the whole document and then allows form manipulation.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Copy the existing field "Signature" to a new field named "SignatureCopy"
            // on the same page (pageNum = -1 keeps the original page).
            formEditor.CopyInnerField("Signature", "SignatureCopy", -1);

            // Persist the changes to the destination file.
            formEditor.Save();
        }

        Console.WriteLine($"Field duplicated successfully. Output saved to '{outputPdf}'.");
    }
}