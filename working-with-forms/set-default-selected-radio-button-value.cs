using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify the radio button default value, and save.
        using (Document doc = new Document(inputPath))
        {
            // Access the radio button group named 'PaymentMethod'.
            RadioButtonField paymentField = doc.Form["PaymentMethod"] as RadioButtonField;
            if (paymentField != null)
            {
                // Set the default selected option to 'Credit'.
                paymentField.Value = "Credit";
            }
            else
            {
                Console.Error.WriteLine("Radio button field 'PaymentMethod' not found.");
            }

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}