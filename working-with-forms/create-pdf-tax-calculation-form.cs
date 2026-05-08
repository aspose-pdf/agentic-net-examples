using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TaxForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // ---------- Subtotal field ----------
            // Rectangle: left, bottom, right, top
            NumberField subtotal = new NumberField(page, new Aspose.Pdf.Rectangle(100, 700, 250, 730));
            subtotal.PartialName = "Subtotal";          // field name
            subtotal.Value = "0";                       // default value
            subtotal.TextHorizontalAlignment = HorizontalAlignment.Right;
            doc.Form.Add(subtotal);                     // add to the form

            // ---------- Tax Rate field ----------
            NumberField taxRate = new NumberField(page, new Aspose.Pdf.Rectangle(100, 650, 250, 680));
            taxRate.PartialName = "TaxRate";
            taxRate.Value = "0";
            taxRate.TextHorizontalAlignment = HorizontalAlignment.Right;
            doc.Form.Add(taxRate);

            // ---------- Tax Amount (calculated) field ----------
            NumberField taxAmount = new NumberField(page, new Aspose.Pdf.Rectangle(100, 600, 250, 630));
            taxAmount.PartialName = "TaxAmount";
            taxAmount.ReadOnly = true;                  // user cannot edit directly
            taxAmount.TextHorizontalAlignment = HorizontalAlignment.Right;

            // JavaScript to calculate TaxAmount = Subtotal * TaxRate / 100
            string js = @"
                var sub = this.getField('Subtotal').value;
                var rate = this.getField('TaxRate').value;
                var tax = (parseFloat(sub) * parseFloat(rate) / 100).toFixed(2);
                this.getField('TaxAmount').value = tax;
            ";
            // Use a valid action property (OnCalculate) instead of the non‑existent JavaScript property
            taxAmount.Actions.OnCalculate = new JavascriptAction(js);
            doc.Form.Add(taxAmount);

            // Ensure automatic recalculation (default is true)
            doc.Form.AutoRecalculate = true;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tax calculation form saved to '{outputPath}'.");
    }
}
