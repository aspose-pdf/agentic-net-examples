using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input_form.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(inputPdf))
        {
            // Fill text fields
            form.FillField("FirstName", "John");
            form.FillField("LastName", "Doe");

            // Fill a checkbox (true = checked, false = unchecked)
            form.FillField("SubscribeNewsletter", true);

            // Fill a radio button by index (0‑based index of the option)
            form.FillField("Gender", 1); // selects the second option

            // Fill a list box with multiple selections
            form.FillField("Interests", new string[] { "Music", "Travel" });

            // Save the updated PDF to a new file
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
    }
}