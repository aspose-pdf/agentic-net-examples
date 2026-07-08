using System;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "currency_form.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will be placed (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a NumberField (inherits from TextBoxField) on the page
            NumberField currencyField = new NumberField(page, fieldRect)
            {
                PartialName = "AmountField",   // internal name of the field
                AlternateName = "Amount"       // tooltip shown in PDF viewers
            };

            // Prepare a currency value with symbol and thousand separators
            decimal amount = 1234567.89m;
            // Format using invariant culture to ensure consistent separators, then prepend the dollar sign
            string formattedValue = string.Format(CultureInfo.InvariantCulture, "${0:N2}", amount);
            // Assign the formatted string to the field
            currencyField.Value = formattedValue;

            // Add the field to the document's form collection
            doc.Form.Add(currencyField);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted currency field saved to '{outputPath}'.");
    }
}