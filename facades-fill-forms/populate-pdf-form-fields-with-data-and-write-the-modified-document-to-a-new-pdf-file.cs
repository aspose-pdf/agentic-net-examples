using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF form and the output PDF
        const string inputPdf  = "input_form.pdf";
        const string outputPdf = "filled_form.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use the Form facade to load the PDF, fill fields, and save the result.
        // Form implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (Form form = new Form(inputPdf))
        {
            // Fill text fields – use the full field names as they appear in the PDF.
            // Adjust the field names and values to match your specific form.
            form.FillField("FirstName", "John");
            form.FillField("LastName",  "Smith");
            form.FillField("Email",     "john.smith@example.com");

            // Example of filling a checkbox field (true = checked, false = unchecked)
            form.FillField("SubscribeNewsletter", true);

            // Example of filling a radio button or list box by index (zero‑based index of the option)
            form.FillField("Gender", 1); // selects the second option in the "Gender" group

            // Save the modified document to a new file.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdf}'.");
    }
}