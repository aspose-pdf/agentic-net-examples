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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (use a using block for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the radio button field named "PaymentMethod"
            RadioButtonField paymentMethod = doc.Form["PaymentMethod"] as RadioButtonField;

            if (paymentMethod != null)
            {
                // Aspose.Pdf allows two ways to set the default selected option:
                // 1. By export value (the value that is stored in the PDF when the form is submitted)
                // 2. By index of the option in the field's Options collection.
                // Here we use the export value "Credit" which must match one of the option values.
                paymentMethod.Value = "Credit";

                // If you prefer to set by index, uncomment the following line (indexes are zero‑based):
                // paymentMethod.Selected = paymentMethod.Options.IndexOf("Credit");
            }
            else
            {
                Console.Error.WriteLine("Radio button field 'PaymentMethod' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
