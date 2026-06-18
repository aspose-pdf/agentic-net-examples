using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

namespace CalculateTotalOrderAmount
{
    class Program
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------------------------
            // 1. Create a sample PDF (self‑contained example)
            // -----------------------------------------------------------------
            using (Document createDoc = new Document())
            {
                // Add a blank page (page indexing is 1‑based)
                Page createPage = createDoc.Pages.Add();
                // Save the temporary PDF that will later be opened for form work
                createDoc.Save("input.pdf");
            }

            // -----------------------------------------------------------------
            // 2. Open the PDF and add form fields with JavaScript calculations
            // -----------------------------------------------------------------
            using (Document doc = new Document("input.pdf"))
            {
                // Enable automatic recalculation of form fields
                doc.Form.AutoRecalculate = true;

                // Get the first (and only) page – 1‑based indexing
                Page page = doc.Pages[1];

                // -----------------------------------------------------------------
                // Quantity field (TextBox)
                // -----------------------------------------------------------------
                Aspose.Pdf.Rectangle qtyRect = new Aspose.Pdf.Rectangle(100, 700, 150, 720);
                TextBoxField quantityField = new TextBoxField(page, qtyRect);
                quantityField.PartialName = "Quantity";
                quantityField.Value = "1";
                doc.Form.Add(quantityField, 1);

                // -----------------------------------------------------------------
                // Price field (TextBox)
                // -----------------------------------------------------------------
                Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(200, 700, 250, 720);
                TextBoxField priceField = new TextBoxField(page, priceRect);
                priceField.PartialName = "Price";
                priceField.Value = "10";
                doc.Form.Add(priceField, 1);

                // -----------------------------------------------------------------
                // Amount field – calculated as Quantity * Price
                // -----------------------------------------------------------------
                Aspose.Pdf.Rectangle amountRect = new Aspose.Pdf.Rectangle(300, 700, 350, 720);
                TextBoxField amountField = new TextBoxField(page, amountRect);
                amountField.PartialName = "Amount";
                doc.Form.Add(amountField, 1);
                // Assign JavaScript to the field's Actions.OnCalculate (not Annotation)
                string amountJs = "var qty = this.getField('Quantity').value; " +
                                 "var prc = this.getField('Price').value; " +
                                 "this.getField('Amount').value = qty * prc;";
                amountField.Actions.OnCalculate = new JavascriptAction(amountJs);

                // -----------------------------------------------------------------
                // Total field – simply mirrors the Amount field
                // -----------------------------------------------------------------
                Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(400, 700, 450, 720);
                TextBoxField totalField = new TextBoxField(page, totalRect);
                totalField.PartialName = "Total";
                doc.Form.Add(totalField, 1);
                string totalJs = "var amt = this.getField('Amount').value; " +
                                "this.getField('Total').value = amt;";
                totalField.Actions.OnCalculate = new JavascriptAction(totalJs);

                // -----------------------------------------------------------------
                // Save the resulting PDF with the form fields and JavaScript attached
                // -----------------------------------------------------------------
                doc.Save("output.pdf");
            }
        }
    }
}
