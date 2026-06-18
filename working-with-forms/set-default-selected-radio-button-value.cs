using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Retrieve the radio button field by its full name
            RadioButtonField paymentMethod = form["PaymentMethod"] as RadioButtonField;
            if (paymentMethod == null)
            {
                Console.Error.WriteLine("Radio button field 'PaymentMethod' not found.");
                return;
            }

            // Set the default selected value to the option named "Credit"
            // The Value property accepts the export value (option name) directly.
            paymentMethod.Value = "Credit";

            // Alternatively, you could set the Selected index if you know it:
            // paymentMethod.Selected = 2; // example index (1‑based)

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with default payment method set to 'Credit' at '{outputPdf}'.");
    }
}