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

            // Find the radio button field named "PaymentMethod"
            RadioButtonField paymentMethod = form["PaymentMethod"] as RadioButtonField;
            if (paymentMethod == null)
            {
                Console.Error.WriteLine("Radio button field 'PaymentMethod' not found.");
                return;
            }

            // Set the default selected value to "Credit"
            // You can set either the Value (export value) or the Selected index.
            // Here we use the export value, which matches the option name.
            paymentMethod.Value = "Credit";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with default selection to '{outputPath}'.");
    }
}