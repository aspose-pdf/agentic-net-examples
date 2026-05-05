using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF with the form
        const string outputPdf = "output.pdf";  // PDF after checking the box

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Form constructor with input and output paths initializes the facade.
        // The using block ensures proper disposal of resources.
        using (Form form = new Form(inputPdf, outputPdf))
        {
            // Fill the checkbox field "TermsAccepted" with true (checked).
            // FillField returns true if the field is found and updated.
            bool filled = form.FillField("TermsAccepted", true);
            if (!filled)
            {
                Console.Error.WriteLine("Field 'TermsAccepted' not found or could not be filled.");
            }

            // Save writes the changes to the output file specified in the constructor.
            form.Save();
        }

        Console.WriteLine($"Checkbox 'TermsAccepted' set to true. Saved to '{outputPdf}'.");
    }
}