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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the radio button on
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page
            Page page = doc.Pages[1];

            // Define rectangles for the individual radio button options
            Aspose.Pdf.Rectangle creditRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            Aspose.Pdf.Rectangle debitRect  = new Aspose.Pdf.Rectangle(100, 660, 120, 680);

            // Create the radio button group (no rectangle in ctor)
            RadioButtonField paymentRadio = new RadioButtonField(page);
            paymentRadio.PartialName = "PaymentMethod";

            // Add the two options with their visual rectangles
            paymentRadio.AddOption("Credit", creditRect);
            paymentRadio.AddOption("Debit",  debitRect);

            // Optionally set a default selected option (1‑based index)
            paymentRadio.Selected = 1; // selects "Credit" by default

            // Register the field with the document's form collection
            doc.Form.Add(paymentRadio);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Radio button group 'PaymentMethod' added and saved to '{outputPath}'.");
    }
}
