using System;
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "currency_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a NumberField (inherits from TextBoxField) at the specified location
            NumberField currencyField = new NumberField(page, fieldRect)
            {
                // Optional: give the field a name
                Name = "AmountField",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Enter amount"
            };

            // Example numeric value
            decimal amount = 1234567.89m;

            // Format the value as currency with symbol and thousand separators
            // Using en-US culture for "$" symbol and comma separator
            string formatted = amount.ToString("C", CultureInfo.GetCultureInfo("en-US"));

            // Populate the field with the formatted string
            currencyField.Value = formatted;

            // Add the field to the document's Form collection (specify the page index, 1‑based)
            doc.Form.Add(currencyField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted currency field saved to '{outputPath}'.");
    }
}