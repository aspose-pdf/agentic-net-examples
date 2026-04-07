using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Rectangle type

class Program
{
    static void Main()
    {
        // Path to the output PDF
        const string outputPath = "CurrencyField.pdf";

        // Currency value to display
        decimal amount = 1234567.89m;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a RichTextBoxField – it supports the FormattedValue property
            // The constructor takes only (Page, Rectangle). The field name is set via PartialName.
            RichTextBoxField currencyField = new RichTextBoxField(page, fieldRect);
            currencyField.PartialName = "CurrencyField";

            // Build the formatted currency string:
            //   - "$" as the symbol (BeforeText)
            //   - Thousand separators and two decimal places
            string formatted = "$" + amount.ToString("N2"); // e.g. $1,234,567.89

            // Assign the formatted string to the field
            currencyField.FormattedValue = formatted;

            // Add the field to the document's form collection
            doc.Form.Add(currencyField);

            // Save the PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted currency field saved to '{outputPath}'.");
    }
}
