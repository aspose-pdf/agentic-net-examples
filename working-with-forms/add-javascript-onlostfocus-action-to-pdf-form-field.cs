using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_js.pdf";

        // Wrap Document in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define rectangle for the quantity field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle qtyRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a NumberField on the page
            NumberField qtyField = new NumberField(page, qtyRect);
            qtyField.PartialName = "Quantity";          // Field name used in JavaScript
            qtyField.AlternateName = "Enter quantity"; // Tooltip

            // Add the field to the form
            doc.Form.Add(qtyField);

            // Define rectangle for the total field (read‑only display)
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 460, 300, 490);
            TextBoxField totalField = new TextBoxField(page, totalRect);
            totalField.PartialName = "Total";
            totalField.ReadOnly = true;                 // Prevent user editing
            doc.Form.Add(totalField);

            // JavaScript to calculate total = quantity * 10 when the quantity field loses focus
            string jsCode = @"
                var qty = this.getField('Quantity').value;
                var total = qty * 10;
                this.getField('Total').value = total;
            ";

            // Assign the JavaScript action to the OnLostFocus event of the quantity field
            qtyField.Actions.OnLostFocus = new JavascriptAction(jsCode);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}