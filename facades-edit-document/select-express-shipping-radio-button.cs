using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with AcroForm
        const string outputPdf = "output.pdf";     // PDF after selecting the option
        const string fieldName = "ShippingMethod"; // full name of the radio button group
        const string optionValue = "Express";      // value to select

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form, fill the radio button, and save the result.
        using (Form form = new Form(inputPdf))
        {
            // Fill the radio button field with the desired option value.
            // For radio button groups, FillField(string, string) sets the selected option.
            form.FillField(fieldName, optionValue);

            // Save the updated PDF.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Radio button '{optionValue}' selected and saved to '{outputPdf}'.");
    }
}