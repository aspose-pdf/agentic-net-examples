using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for form handling

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the form
        const string outputPdf = "output_checked.pdf"; // destination PDF after checking

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form using the Form facade (lifecycle: load)
        using (Form form = new Form(inputPdf))
        {
            // Fill the checkbox field "TermsAccepted" with true (check the box)
            bool filled = form.FillField("TermsAccepted", true);
            if (!filled)
            {
                Console.Error.WriteLine("Checkbox field 'TermsAccepted' not found.");
            }

            // Save the modified document (lifecycle: save)
            form.Save(outputPdf);
        }

        Console.WriteLine($"Checkbox updated and saved to '{outputPdf}'.");
    }
}