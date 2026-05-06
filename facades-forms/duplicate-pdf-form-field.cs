using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the source PDF document
        using (Document srcDoc = new Document(inputPdf))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(srcDoc))
            {
                // Copy the existing field "Signature" to a new field "SignatureCopy"
                // Page number -1 indicates the same page as the original field.
                formEditor.CopyInnerField("Signature", "SignatureCopy", -1);

                // Save the modified document to the output path
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Field duplicated successfully: '{outputPdf}'.");
    }
}