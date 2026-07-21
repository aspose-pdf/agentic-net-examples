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
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the field rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a RichTextBoxField on the page
            RichTextBoxField currencyField = new RichTextBoxField(page, fieldRect)
            {
                Name = "CurrencyField",
                // Set the formatted currency value (symbol + thousand separators)
                FormattedValue = "$1,234.56"
            };

            // Add the field to the document's form collection (not directly to page annotations)
            doc.Form.Add(currencyField);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("currency_form.pdf");
        }

        Console.WriteLine("PDF with formatted currency field saved as 'currency_form.pdf'.");
    }
}
