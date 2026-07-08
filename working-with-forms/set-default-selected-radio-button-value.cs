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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Retrieve the radio button field named "PaymentMethod"
            RadioButtonField paymentMethod = form["PaymentMethod"] as RadioButtonField;

            if (paymentMethod != null)
            {
                // Set the default selected value to the option named "Credit"
                paymentMethod.Value = "Credit";

                // Optionally ensure that exactly one option is always selected
                paymentMethod.NoToggleToOff = true;
            }
            else
            {
                Console.Error.WriteLine("Radio button field 'PaymentMethod' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with default selection to '{outputPath}'.");
    }
}