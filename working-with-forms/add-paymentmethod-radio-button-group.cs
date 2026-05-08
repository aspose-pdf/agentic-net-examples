using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "PaymentMethodForm.pdf";

        // Create a new PDF document and add a page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the two radio button options.
            // Rectangle(left, bottom, right, top)
            Rectangle creditRect = new Rectangle(100, 700, 120, 720);
            Rectangle debitRect  = new Rectangle(100, 660, 120, 680);

            // Create a radio button field bound to the page.
            RadioButtonField paymentMethod = new RadioButtonField(page);

            // Set the field name (group name) – this is the name used to identify the group.
            paymentMethod.PartialName = "PaymentMethod";
            paymentMethod.Name = "PaymentMethod";

            // Add the two options with their visual bounds.
            paymentMethod.AddOption("Credit", creditRect);
            paymentMethod.AddOption("Debit",  debitRect);

            // Add the radio button field to the document's form collection.
            doc.Form.Add(paymentMethod);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}