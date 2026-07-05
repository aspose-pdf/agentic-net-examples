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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a radio button field (group) on the page
            RadioButtonField paymentMethod = new RadioButtonField(page)
            {
                // Set the group name (field name)
                PartialName = "PaymentMethod",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Select Payment Method"
            };

            // Add the first option "Credit"
            paymentMethod.AddOption("Credit", new Rectangle(100, 700, 120, 720));

            // Add the second option "Debit" positioned below the first
            paymentMethod.AddOption("Debit", new Rectangle(100, 660, 120, 680));

            // Add the radio button field to the document's form collection
            doc.Form.Add(paymentMethod);

            // Save the PDF to a file
            doc.Save("PaymentMethodForm.pdf");
        }

        Console.WriteLine("PDF with radio button group 'PaymentMethod' created successfully.");
    }
}