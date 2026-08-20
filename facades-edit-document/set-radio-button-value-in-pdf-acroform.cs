using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF with AcroForm
        const string outputPdf = "output_filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form, fill the radio button, and save.
        using (Form form = new Form(inputPdf))
        {
            // Fill the radio button group "ShippingMethod" with the option "Express".
            // The field name must be the fully qualified name of the radio group.
            form.FillField("ShippingMethod", "Express");

            // Save the updated PDF.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Radio button set to 'Express' and saved to '{outputPdf}'.");
    }
}