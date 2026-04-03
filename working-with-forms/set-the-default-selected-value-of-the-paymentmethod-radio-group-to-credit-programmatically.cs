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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Retrieve the radio button field named "PaymentMethod"
            RadioButtonField paymentRadio = form["PaymentMethod"] as RadioButtonField;

            if (paymentRadio != null)
            {
                // Set the selected value to "Credit"
                // This sets the field's value; the corresponding option will be selected.
                paymentRadio.Value = "Credit";

                // Optional: ensure the Selected index matches the option name.
                // (Numbering of items starts from 1)
                for (int i = 0; i < paymentRadio.Options.Count; i++)
                {
                    if (paymentRadio.Options[i].Name.Equals("Credit", StringComparison.OrdinalIgnoreCase))
                    {
                        paymentRadio.Selected = i + 1; // set index (1‑based)
                        break;
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("Radio button field 'PaymentMethod' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}