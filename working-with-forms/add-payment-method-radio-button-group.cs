using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "payment_method_form.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Instantiate the radio button field with the page only (no rectangle in ctor)
            RadioButtonField paymentMethod = new RadioButtonField(page);
            // Set the field name (PartialName) and its rectangle
            paymentMethod.PartialName = "PaymentMethod";
            paymentMethod.Rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Add options to the radio button group
            paymentMethod.AddOption("Credit");
            paymentMethod.AddOption("Debit");

            // Add the radio button field to the document's form collection
            doc.Form.Add(paymentMethod);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button group saved to '{outputPath}'.");
    }
}