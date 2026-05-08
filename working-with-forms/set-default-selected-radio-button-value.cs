using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths
        const string outputPath = "radio_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the radio button field
            Page page = doc.Pages.Add();

            // Define rectangles for each option (position and size)
            Aspose.Pdf.Rectangle rectCash   = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            Aspose.Pdf.Rectangle rectCredit = new Aspose.Pdf.Rectangle(100, 660, 120, 680);
            Aspose.Pdf.Rectangle rectCheck  = new Aspose.Pdf.Rectangle(100, 620, 120, 640);

            // Create a radio button field on the page (page‑aware constructor)
            RadioButtonField radio = new RadioButtonField(page);
            radio.PartialName = "PaymentMethod";

            // Add the options (the first option is created by AddOption as well)
            radio.AddOption("Cash",   rectCash);
            radio.AddOption("Credit", rectCredit);
            radio.AddOption("Check",  rectCheck);

            // Set the default selected value to "Credit"
            radio.Value = "Credit"; // matches the export value of the option

            // Add the radio button field to the document's form collection
            doc.Form.Add(radio);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with radio button saved to '{outputPath}'.");
    }
}
