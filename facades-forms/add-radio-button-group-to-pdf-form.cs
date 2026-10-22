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

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Page 3 where the radio button group will be placed
        Page page = pdfDocument.Pages[3];

        // Create a radio button field (group) named "PaymentMethod"
        RadioButtonField paymentMethod = new RadioButtonField(page)
        {
            PartialName = "PaymentMethod",
            // Set a default selected value if desired
            Value = "Credit"
        };

        // Add the individual options to the group using AddOption (export value + rectangle)
        paymentMethod.AddOption("Credit", new Rectangle(100, 700, 120, 720));   // Credit option
        paymentMethod.AddOption("PayPal", new Rectangle(150, 700, 170, 720)); // PayPal option

        // Add the radio button field to the PDF form
        pdfDocument.Form.Add(paymentMethod);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}
