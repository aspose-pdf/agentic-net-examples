using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (containing the "Logo" image field)
        // and the destination PDF where the new "HeaderLogo" field will be created.
        const string sourcePdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists.
        if (!System.IO.File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // FormEditor works with two files: the input PDF and the output PDF.
        // The constructor overload (string srcFile, string dstFile) creates a
        // facade bound to the source document and prepares the destination.
        using (FormEditor formEditor = new FormEditor(sourcePdf, outputPdf))
        {
            // Copy the inner content (appearance) of the existing image field
            // named "Logo" to a new image field named "HeaderLogo".
            // The page number -1 tells Aspose.Pdf to place the new field on the
            // same page as the original field.
            formEditor.CopyInnerField("Logo", "HeaderLogo", -1);

            // Persist the changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Image field copied successfully to '{outputPdf}'.");
    }
}