using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "payment_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a radio button field on the page
            RadioButtonField paymentRadio = new RadioButtonField(page);
            // Set the group name
            paymentRadio.PartialName = "PaymentMethod";

            // Define rectangles for each option (coordinates in points)
            Aspose.Pdf.Rectangle rectCredit = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            Aspose.Pdf.Rectangle rectDebit  = new Aspose.Pdf.Rectangle(100, 660, 120, 680);

            // Add the two options to the group
            paymentRadio.AddOption("Credit", rectCredit);
            paymentRadio.AddOption("Debit",  rectDebit);

            // Ensure exactly one option is selected at a time
            paymentRadio.NoToggleToOff = true;

            // Add the radio button field to the document's form collection
            doc.Form.Add(paymentRadio);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}