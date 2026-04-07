using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (contains the form fields to copy)
        // and the destination PDF (receives the copied fields).
        const string sourcePdf = "source.pdf";
        const string destinationPdf = "destination.pdf";
        const string outputPdf = "output.pdf";

        // Verify that both files exist before proceeding.
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }

        // Open the source PDF with the Form facade to enumerate its field names.
        using (Form srcForm = new Form(sourcePdf))
        {
            // Bind the destination PDF to a FormEditor instance.
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(destinationPdf);

                // Copy each form field from the source PDF to the destination PDF.
                foreach (string fieldName in srcForm.FieldNames)
                {
                    // CopyOuterField copies the field with its original page and coordinates.
                    editor.CopyOuterField(sourcePdf, fieldName);
                }

                // Save the modified destination PDF to the output path.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Form fields copied successfully to '{outputPdf}'.");
    }
}