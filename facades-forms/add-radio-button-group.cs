using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "payment_method.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three pages; the third page will hold the radio button group
            doc.Pages.Add();
            doc.Pages.Add();
            Page page3 = doc.Pages.Add();

            // Create a radio button field on page 3
            RadioButtonField radioField = new RadioButtonField(page3);
            radioField.PartialName = "PaymentMethod";

            // Define rectangles for each option (position and size)
            Aspose.Pdf.Rectangle rectCredit = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            Aspose.Pdf.Rectangle rectPayPal = new Aspose.Pdf.Rectangle(100, 460, 120, 480);

            // Add the two options to the radio button group
            radioField.AddOption("Credit", rectCredit);
            radioField.AddOption("PayPal", rectPayPal);

            // Add the radio button field to the document's form collection
            doc.Form.Add(radioField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with radio button group saved to '" + outputPath + "'.");
    }
}