using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TaxForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the form fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectSubtotal = new Aspose.Pdf.Rectangle(50, 750, 200, 770);
            Aspose.Pdf.Rectangle rectPercent   = new Aspose.Pdf.Rectangle(50, 700, 200, 720);
            Aspose.Pdf.Rectangle rectTax       = new Aspose.Pdf.Rectangle(50, 650, 200, 670);

            // Create a TextBox for Subtotal
            TextBoxField subtotalField = new TextBoxField(page, rectSubtotal);
            subtotalField.PartialName = "subtotal";
            subtotalField.Value = "0.00";
            subtotalField.Border = new Border(subtotalField) { Width = 1 };
            subtotalField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(subtotalField);

            // Create a TextBox for Tax Percentage
            TextBoxField percentField = new TextBoxField(page, rectPercent);
            percentField.PartialName = "percent";
            percentField.Value = "0";
            percentField.Border = new Border(percentField) { Width = 1 };
            percentField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(percentField);

            // Create a read‑only TextBox for Tax Amount
            TextBoxField taxField = new TextBoxField(page, rectTax);
            taxField.PartialName = "tax";
            taxField.Value = "0.00";
            taxField.Border = new Border(taxField) { Width = 1 };
            taxField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            taxField.ReadOnly = true;
            doc.Form.Add(taxField);

            // JavaScript that calculates tax = subtotal * percent / 100
            string jsCalc = @"
                var sub = this.getField('subtotal').value;
                var pct = this.getField('percent').value;
                var tax = parseFloat(sub) * parseFloat(pct) / 100;
                if (!isNaN(tax)) {
                    this.getField('tax').value = tax.toFixed(2);
                } else {
                    this.getField('tax').value = '';
                }
            ";

            // Attach the script to the OnCalculate event of the fields that affect the tax value
            subtotalField.Actions.OnCalculate = new JavascriptAction(jsCalc);
            percentField.Actions.OnCalculate   = new JavascriptAction(jsCalc);

            // Optional: add labels for visual clarity
            page.Paragraphs.Add(new TextFragment("Subtotal:") { Position = new Position(250, 760) });
            page.Paragraphs.Add(new TextFragment("Tax %:")   { Position = new Position(250, 710) });
            page.Paragraphs.Add(new TextFragment("Tax:")     { Position = new Position(250, 660) });

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form saved to '{outputPath}'.");
    }
}
