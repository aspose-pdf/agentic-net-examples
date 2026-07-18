using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "TaxForm.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Subtotal field (numeric input)
            NumberField subtotal = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 600, 200, 620))
            {
                PartialName = "Subtotal",
                Value = "0"
            };
            doc.Form.Add(subtotal, 1);

            // Tax rate field (percentage)
            NumberField taxRate = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 560, 200, 580))
            {
                PartialName = "TaxRate",
                Value = "0"
            };
            doc.Form.Add(taxRate, 1);

            // Tax amount field (calculated, read‑only)
            NumberField taxAmount = new NumberField(doc, new Aspose.Pdf.Rectangle(100, 520, 200, 540))
            {
                PartialName = "TaxAmount",
                ReadOnly = true
            };
            // JavaScript to calculate tax = Subtotal * TaxRate / 100
            JavascriptAction calcJs = new JavascriptAction(
                "event.value = this.getField('Subtotal').value * this.getField('TaxRate').value / 100;");
            // Use a valid action property – OnCalculate – to attach the script
            taxAmount.Actions.OnCalculate = calcJs;
            doc.Form.Add(taxAmount, 1);

            // Ensure automatic recalculation is enabled (default)
            doc.Form.AutoRecalculate = true;

            // Save the PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with tax calculation saved to '{outputPath}'.");
    }
}