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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the radio button group named "PaymentMethod"
            RadioButtonField paymentMethod = doc.Form["PaymentMethod"] as RadioButtonField;

            if (paymentMethod != null)
            {
                // Set the current value so the selection is visible when the PDF opens
                // The Value property expects the export value of the option (e.g., "Credit").
                paymentMethod.Value = "Credit";

                // Optionally, set the Selected index (1‑based) if you know the position of "Credit"
                // paymentMethod.Selected = 1; // uncomment and adjust if needed
            }
            else
            {
                Console.Error.WriteLine("Radio button group 'PaymentMethod' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
