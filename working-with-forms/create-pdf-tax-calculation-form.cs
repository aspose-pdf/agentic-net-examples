using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class TaxFormGenerator
{
    static void Main()
    {
        const string outputPath = "tax_form.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define rectangles for the form fields (left, bottom, width, height)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rectSubtotal = new Aspose.Pdf.Rectangle(50, 700, 200, 720);
            Aspose.Pdf.Rectangle rectTaxRate  = new Aspose.Pdf.Rectangle(50, 650, 200, 670);
            Aspose.Pdf.Rectangle rectTaxAmt   = new Aspose.Pdf.Rectangle(50, 600, 200, 620);

            // Create Subtotal field (numeric input)
            NumberField subtotalField = new NumberField(page, rectSubtotal);
            subtotalField.Name = "Subtotal";
            subtotalField.PartialName = "Subtotal";
            subtotalField.AlternateName = "Enter Subtotal";
            subtotalField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            page.Paragraphs.Add(subtotalField);

            // Create Tax Rate field (percentage input)
            NumberField taxRateField = new NumberField(page, rectTaxRate);
            taxRateField.Name = "TaxRate";
            taxRateField.PartialName = "TaxRate";
            taxRateField.AlternateName = "Enter Tax Rate (%)";
            taxRateField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            page.Paragraphs.Add(taxRateField);

            // Create Tax Amount field (calculated, read‑only)
            NumberField taxAmtField = new NumberField(page, rectTaxAmt);
            taxAmtField.Name = "TaxAmount";
            taxAmtField.PartialName = "TaxAmount";
            taxAmtField.AlternateName = "Tax Amount";
            taxAmtField.ReadOnly = true; // user cannot edit directly
            taxAmtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            page.Paragraphs.Add(taxAmtField);

            // Add JavaScript to calculate TaxAmount = Subtotal * TaxRate / 100
            // The script runs when any field changes because AutoRecalculate is true by default
            string js = @"
                var subtotal = this.getField('Subtotal').value;
                var rate = this.getField('TaxRate').value;
                if (!isNaN(subtotal) && !isNaN(rate)) {
                    this.getField('TaxAmount').value = (subtotal * rate / 100).toFixed(2);
                } else {
                    this.getField('TaxAmount').value = '';
                }
            ";
            // Attach the script to the TaxAmount field using the correct action property
            taxAmtField.Actions.OnCalculate = new JavascriptAction(js);

            // Optional: set a visual border for each field
            subtotalField.Border = new Border(subtotalField) { Width = 1 };
            taxRateField.Border = new Border(taxRateField) { Width = 1 };
            taxAmtField.Border = new Border(taxAmtField) { Width = 1 };

            // Save the PDF document (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tax calculation form saved to '{outputPath}'.");
    }
}
