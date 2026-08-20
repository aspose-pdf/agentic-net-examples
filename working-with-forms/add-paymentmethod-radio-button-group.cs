using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "payment_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the radio button group
            Page page = doc.Pages.Add();

            // Create a radio button field (group) associated with the page
            RadioButtonField paymentMethod = new RadioButtonField(page);
            paymentMethod.PartialName = "PaymentMethod";               // Group name
            paymentMethod.NoToggleToOff = false;                      // Exactly one option selected

            // Define and add the "Credit" option
            Aspose.Pdf.Rectangle creditRect = new Aspose.Pdf.Rectangle(100, 600, 115, 615);
            paymentMethod.AddOption("Credit", creditRect);

            // Define and add the "Debit" option
            Aspose.Pdf.Rectangle debitRect = new Aspose.Pdf.Rectangle(100, 580, 115, 595);
            paymentMethod.AddOption("Debit", debitRect);

            // Add the radio button field to the document's form
            doc.Form.Add(paymentMethod);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}
