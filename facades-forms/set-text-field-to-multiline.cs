using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the form
        const string outputPdf = "output.pdf";  // PDF with the multiline field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works as a facade for editing AcroForm fields.
        // The constructor takes the source PDF and the destination PDF.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Change the "Comments" text field from single‑line to multiline.
            // Returns true if the operation succeeds.
            bool success = formEditor.Single2Multiple("Comments");
            if (!success)
            {
                Console.Error.WriteLine("Failed to set multiline property for field 'Comments'.");
                return;
            }

            // Persist changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Multiline field saved to '{outputPdf}'.");
    }
}