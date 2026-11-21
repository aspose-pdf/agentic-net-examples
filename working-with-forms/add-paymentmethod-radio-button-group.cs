using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "payment_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Create a radio button field (group) on the page
            RadioButtonField paymentMethod = new RadioButtonField(page);
            paymentMethod.PartialName = "PaymentMethod"; // group name

            // Define rectangles for each option (left, bottom, right, top)
            var creditRect = new Aspose.Pdf.Rectangle(100, 700, 115, 715);
            var debitRect  = new Aspose.Pdf.Rectangle(150, 700, 165, 715);

            // Add the two options to the group
            paymentMethod.AddOption("Credit", creditRect);
            paymentMethod.AddOption("Debit",  debitRect);

            // Add the field to the document's form collection (page index is 1‑based)
            doc.Form.Add(paymentMethod, 1);

            // Set a default selected option (1‑based index; 1 = first option added)
            paymentMethod.Selected = 1; // selects "Credit"

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}
