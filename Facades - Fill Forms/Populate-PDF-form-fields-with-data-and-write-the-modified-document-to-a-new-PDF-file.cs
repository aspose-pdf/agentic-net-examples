using System;
using System.IO;
using Aspose.Pdf.Facades;   // Form facade resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing form fields
        const string outputPdf = "filled_output.pdf";  // destination PDF after filling

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF file
        Form form = new Form(inputPdf);

        // Fill text fields (field names are case‑sensitive and must be fully qualified)
        form.FillField("FirstName", "John");
        form.FillField("LastName",  "Doe");

        // Fill a check box field (true = checked, false = unchecked)
        form.FillField("SubscribeNewsletter", true);

        // Fill a combo box or list box by index (0‑based index)
        form.FillField("CountryList", 2); // selects the third item in the list

        // Save the updated PDF to a new file
        form.Save(outputPdf);

        Console.WriteLine($"Form fields populated and saved to '{outputPdf}'.");
    }
}