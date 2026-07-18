using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "payment_method_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page where the radio button options will be placed
            Page page = doc.Pages.Add();

            // Create a radio button field attached to the page
            RadioButtonField paymentRadio = new RadioButtonField(page);
            paymentRadio.PartialName = "PaymentMethod"; // set the field name

            // Define rectangles for each option (coordinates: llx, lly, urx, ury)
            Rectangle creditRect = new Rectangle(100, 700, 120, 720);
            Rectangle debitRect  = new Rectangle(100, 650, 120, 670);

            // Add the two options to the radio button group with their positions
            paymentRadio.AddOption("Credit", creditRect);
            paymentRadio.AddOption("Debit",  debitRect);

            // Optionally set the default selected option (1‑based index)
            paymentRadio.Selected = 1; // selects "Credit" by default

            // Add the radio button field to the document's form collection
            doc.Form.Add(paymentRadio);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}
