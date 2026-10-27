using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "OrderForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Helper to create a textbox field
            TextBoxField CreateTextBox(string name, double llx, double lly, double urx, double ury, string defaultValue)
            {
                var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
                var field = new TextBoxField(page, rect)
                {
                    PartialName = name,
                    Value = defaultValue,
                    // Set border color (the annotation's own Color property)
                    Color = Aspose.Pdf.Color.Black
                };
                // Border requires the parent annotation in its constructor
                field.Border = new Border(field) { Width = 1 };
                // Add the field to the document's form
                doc.Form.Add(field);
                return field;
            }

            // Create fields for two line items
            CreateTextBox("qty1", 50, 750, 100, 770, "0");
            CreateTextBox("price1", 110, 750, 160, 770, "0.00");
            CreateTextBox("total1", 170, 750, 220, 770, "0.00");

            CreateTextBox("qty2", 50, 720, 100, 740, "0");
            CreateTextBox("price2", 110, 720, 160, 740, "0.00");
            CreateTextBox("total2", 170, 720, 220, 740, "0.00");

            // Field for grand total
            CreateTextBox("grandTotal", 170, 690, 220, 710, "0.00");

            // Add a button that triggers the calculation
            var btnRect = new Aspose.Pdf.Rectangle(50, 690, 120, 710);
            var calcButton = new ButtonField(page, btnRect);
            calcButton.PartialName = "calcButton";
            calcButton.Color = Aspose.Pdf.Color.Black;
            calcButton.Border = new Border(calcButton) { Width = 1 };

            // Add the button to the form *before* setting caption or actions
            doc.Form.Add(calcButton);

            // Set the visible caption after the field is part of the form
            calcButton.AlternateCaption = "Calculate";

            // JavaScript to compute totals
            string js = @"
                var q1 = this.getField('qty1').value;
                var p1 = this.getField('price1').value;
                var t1 = q1 * p1;
                this.getField('total1').value = t1.toFixed(2);

                var q2 = this.getField('qty2').value;
                var p2 = this.getField('price2').value;
                var t2 = q2 * p2;
                this.getField('total2').value = t2.toFixed(2);

                var grand = t1 + t2;
                this.getField('grandTotal').value = grand.toFixed(2);
            ";
            // Assign the JavaScript to the button's mouse‑press action
            calcButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Save the PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields and JavaScript saved to '{outputPath}'.");
    }
}
