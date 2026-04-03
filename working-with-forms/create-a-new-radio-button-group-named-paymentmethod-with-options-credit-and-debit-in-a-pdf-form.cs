using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page where the radio button will be placed
            Page page = doc.Pages.Add();

            // Create the radio button field (group) on the page using the page‑only constructor
            RadioButtonField paymentMethod = new RadioButtonField(page);

            // Set the group name
            paymentMethod.Name = "PaymentMethod";

            // Add the "Credit" option with its own rectangle (left, bottom, right, top)
            paymentMethod.AddOption("Credit", new Rectangle(100, 560, 120, 580));

            // Add the "Debit" option with its own rectangle (left, bottom, right, top)
            paymentMethod.AddOption("Debit", new Rectangle(100, 520, 120, 540));

            // Add the radio button field to the document's form collection
            doc.Form.Add(paymentMethod);

            // Save the PDF with the radio button group
            doc.Save("PaymentMethodForm.pdf");
        }
    }
}
