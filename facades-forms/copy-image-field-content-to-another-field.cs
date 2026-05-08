using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string sourcePdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // FormEditor works with AcroForm fields. It creates a new document (outputPdf)
        // that contains all original content plus the copied field.
        using (FormEditor formEditor = new FormEditor(sourcePdf, outputPdf))
        {
            // Copy the inner content of the image field "Logo" to a new image field "HeaderLogo".
            // The third parameter is the page number; -1 means keep the original page.
            formEditor.CopyInnerField("Logo", "HeaderLogo", -1);

            // Persist the changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Field content copied. Output saved to '{outputPdf}'.");
    }
}