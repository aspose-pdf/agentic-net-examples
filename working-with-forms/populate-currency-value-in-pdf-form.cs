using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "filled_currency.pdf";

        // Create a simple one‑page PDF if the template does not exist
        if (!File.Exists(inputPdf))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing for pages)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle where the field will be placed (llx, lly, urx, ury)
            // Rectangle constructor expects double values
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the RichTextBoxField on the page
            RichTextBoxField currencyField = new RichTextBoxField(page, fieldRect);

            // Optional visual styling (border removed because the Border class is not available in the referenced version)
            currencyField.Color = Aspose.Pdf.Color.LightGray;

            // Example numeric value
            decimal amount = 1234567.89m;

            // Format as currency with symbol and thousand separators (US culture)
            string formattedCurrency = amount.ToString("C", CultureInfo.GetCultureInfo("en-US"));
            currencyField.FormattedValue = formattedCurrency;

            // Add the field to the document's form collection (page number is 1‑based)
            pdfDoc.Form.Add(currencyField, 1);

            // Save the updated PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Currency field populated and saved to '{outputPdf}'.");
    }
}
