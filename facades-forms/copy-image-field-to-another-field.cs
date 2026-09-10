using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDFs.
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Copy the inner content of the image field "Logo" to a new field "HeaderLogo"
        // on the same page (-1 keeps the original page).
        formEditor.CopyInnerField("Logo", "HeaderLogo", -1);

        // Persist changes.
        formEditor.Save();

        // Release resources.
        formEditor.Close();

        Console.WriteLine($"Image field copied successfully to '{outputPdf}'.");
    }
}