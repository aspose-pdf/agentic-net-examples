using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form using the Form facade
        using (Form form = new Form(inputPdf))
        {
            // Fill a text field
            form.FillField("FirstName", "John");

            // Fill a checkbox field
            form.FillField("Subscribe", true);

            // Fill a list box or combo box by index (example)
            form.FillField("CountryList", 2);

            // Save the updated PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
    }
}