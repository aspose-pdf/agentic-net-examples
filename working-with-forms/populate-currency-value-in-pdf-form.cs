using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "currency_form.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the field rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a RichTextBoxField on the page
            RichTextBoxField currencyField = new RichTextBoxField(page, rect);

            // Populate the field with a formatted currency string
            // Includes the currency symbol and thousand separators
            currencyField.FormattedValue = "$1,234,567.89";

            // Optional: give the field a name for later reference
            currencyField.PartialName = "CurrencyField";

            // Add the field to the document's interactive form
            doc.Form.Add(currencyField);

            // Save the PDF (Document.Save without SaveOptions always writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}