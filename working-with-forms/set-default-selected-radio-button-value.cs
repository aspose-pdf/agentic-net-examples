using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
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

            // Set the default selected value to "Credit"
            // The Value property accepts the export value of the option.
            paymentMethod.Value = "Credit";

            // Alternatively, you could set the Selected index if you know it:
            // paymentMethod.Selected = 2; // example index (1‑based)

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with default payment method set to 'Credit' at '{outputPath}'.");
    }
}