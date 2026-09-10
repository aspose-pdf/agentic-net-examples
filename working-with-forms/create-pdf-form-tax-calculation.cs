using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TaxForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            Aspose.Pdf.Rectangle rectSubtotal = new Aspose.Pdf.Rectangle(100, 700, 250, 720);
            Aspose.Pdf.Rectangle rectTaxRate = new Aspose.Pdf.Rectangle(100, 650, 250, 670);
            Aspose.Pdf.Rectangle rectTaxAmount = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // ----- Subtotal NumberField -----
            NumberField subtotalField = new NumberField(page, rectSubtotal);
            subtotalField.PartialName = "Subtotal";
            subtotalField.Color = Aspose.Pdf.Color.LightGray;
            subtotalField.AllowedChars = "0123456789.";
            // Border must be set after the field is instantiated
            subtotalField.Border = new Border(subtotalField) { Width = 1 };
            page.Paragraphs.Add(subtotalField);

            // ----- Tax Rate NumberField (percentage) -----
            NumberField taxRateField = new NumberField(page, rectTaxRate);
            taxRateField.PartialName = "TaxRate";
            taxRateField.Color = Aspose.Pdf.Color.LightGray;
            taxRateField.AllowedChars = "0123456789.";
            taxRateField.Border = new Border(taxRateField) { Width = 1 };
            page.Paragraphs.Add(taxRateField);

            // ----- Tax Amount NumberField (calculated, read‑only) -----
            NumberField taxAmountField = new NumberField(page, rectTaxAmount);
            taxAmountField.PartialName = "TaxAmount";
            taxAmountField.Color = Aspose.Pdf.Color.LightGray;
            taxAmountField.ReadOnly = true;
            taxAmountField.Border = new Border(taxAmountField) { Width = 1 };
            page.Paragraphs.Add(taxAmountField);

            // JavaScript calculation for TaxAmount field
            string jsCalc = @"
                var subtotal = this.getField('Subtotal').value;
                var rate = this.getField('TaxRate').value;
                if (subtotal == '' || rate == '') {
                    event.value = '';
                } else {
                    event.value = (subtotal * rate / 100).toFixed(2);
                }
            ";
            taxAmountField.Actions.OnCalculate = new JavascriptAction(jsCalc);

            // Enable automatic recalculation (default is true, but set explicitly for clarity)
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with tax calculation saved to '{outputPath}'.");
    }
}
