using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class TaxFormGenerator
{
    static void Main()
    {
        const string outputPath = "TaxForm.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            // Use fully qualified Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rectSubtotal = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle rectTaxRate   = new Aspose.Pdf.Rectangle(100, 650, 200, 670);
            Aspose.Pdf.Rectangle rectTaxAmount = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create Subtotal field (numeric input)
            NumberField subtotalField = new NumberField(page, rectSubtotal);
            subtotalField.PartialName = "Subtotal";
            subtotalField.AlternateName = "Enter Subtotal";
            subtotalField.Color = Aspose.Pdf.Color.LightGray;
            subtotalField.Border = new Border(subtotalField) { Width = 1 };
            // Add the field to the document's form collection (specify page number)
            doc.Form.Add(subtotalField, 1);

            // Create Tax Rate field (percentage)
            NumberField taxRateField = new NumberField(page, rectTaxRate);
            taxRateField.PartialName = "TaxRate";
            taxRateField.AlternateName = "Enter Tax Rate (%)";
            taxRateField.Color = Aspose.Pdf.Color.LightGray;
            taxRateField.Border = new Border(taxRateField) { Width = 1 };
            doc.Form.Add(taxRateField, 1);

            // Create Tax Amount field (calculated)
            NumberField taxAmountField = new NumberField(page, rectTaxAmount);
            taxAmountField.PartialName = "TaxAmount";
            taxAmountField.AlternateName = "Tax Amount (auto)";
            taxAmountField.ReadOnly = true; // user cannot edit directly
            taxAmountField.Color = Aspose.Pdf.Color.LightGray;
            taxAmountField.Border = new Border(taxAmountField) { Width = 1 };
            doc.Form.Add(taxAmountField, 1);

            // JavaScript that calculates TaxAmount = Subtotal * TaxRate / 100
            // This script is attached to the calculated field's OnCalculate action.
            string js = @"
var subtotal = this.getField('Subtotal').value;
var rate = this.getField('TaxRate').value;
if (!isNaN(subtotal) && !isNaN(rate)) {
    event.value = (subtotal * rate / 100).toFixed(2);
} else {
    event.value = '';
}";

            // Attach the script to the TaxAmount field's OnCalculate action.
            taxAmountField.Actions.OnCalculate = new JavascriptAction(js);

            // Ensure automatic recalculation (default is true, set explicitly for clarity)
            doc.Form.AutoRecalculate = true;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tax calculation form saved to '{outputPath}'.");
    }
}
