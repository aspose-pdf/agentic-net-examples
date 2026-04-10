using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with the form
        const string outputPdf = "output.pdf";  // PDF after checking the box

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form, fill the checkbox, and save the result.
        using (Form form = new Form(inputPdf))
        {
            // Fill the checkbox field named "TermsAccepted" with true (checked).
            bool filled = form.FillField("TermsAccepted", true);
            if (!filled)
            {
                Console.Error.WriteLine("Checkbox field 'TermsAccepted' not found.");
            }

            // Save the modified PDF.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Checkbox updated and saved to '{outputPdf}'.");
    }
}