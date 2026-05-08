using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "currency_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the field rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a NumberField (inherits TextBoxField) on the page
            NumberField currencyField = new NumberField(page, rect);

            // Allow digits, comma, period and currency symbol
            currencyField.AllowedChars = "0123456789.,$";

            // Set the formatted currency value (symbol + thousand separators)
            currencyField.Value = "$1,234,567.89";

            // Optional: set a tooltip for the field
            currencyField.AlternateName = "Total Amount";

            // Add the field to the document's interactive form
            doc.Form.Add(currencyField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted currency field saved to '{outputPath}'.");
    }
}