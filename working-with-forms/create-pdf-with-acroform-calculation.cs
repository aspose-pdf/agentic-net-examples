using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class CreateAcroFormWithJavaScript
{
    static void Main()
    {
        const string outputPath = "OrderForm.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Add labels for the fields
            TextFragment labelQty = new TextFragment("Quantity:");
            labelQty.Position = new Position(50, 720);
            page.Paragraphs.Add(labelQty);

            TextFragment labelPrice = new TextFragment("Unit Price:");
            labelPrice.Position = new Position(50, 680);
            page.Paragraphs.Add(labelPrice);

            TextFragment labelTotal = new TextFragment("Total:");
            labelTotal.Position = new Position(50, 640);
            page.Paragraphs.Add(labelTotal);

            // Define rectangles for the form fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectQty   = new Aspose.Pdf.Rectangle(150, 710, 250, 730);
            Aspose.Pdf.Rectangle rectPrice = new Aspose.Pdf.Rectangle(150, 670, 250, 690);
            Aspose.Pdf.Rectangle rectTotal = new Aspose.Pdf.Rectangle(150, 630, 250, 650);

            // Create TextBox fields (2‑argument constructor) and set their names
            TextBoxField qtyField = new TextBoxField(page, rectQty);
            qtyField.PartialName = "Qty";

            TextBoxField priceField = new TextBoxField(page, rectPrice);
            priceField.PartialName = "Price";

            TextBoxField totalField = new TextBoxField(page, rectTotal);
            totalField.PartialName = "Total";

            // Set default values (optional)
            qtyField.Value = "0";
            priceField.Value = "0.00";
            totalField.Value = "0.00";

            // Add the fields to the document's AcroForm
            doc.Form.Add(qtyField);
            doc.Form.Add(priceField);
            doc.Form.Add(totalField);

            // JavaScript that calculates the total
            string calculateScript = @"
function calculate()
{
    var qty = this.getField('Qty').value;
    var price = this.getField('Price').value;
    var total = parseFloat(qty) * parseFloat(price);
    if (!isNaN(total))
        this.getField('Total').value = total.toFixed(2);
}
calculate();
";

            // Attach the script to the document open action (runs when the PDF is opened)
            doc.OpenAction = new JavascriptAction(calculateScript);

            // Attach JavaScript to recalculate total when either Qty or Price changes
            qtyField.Actions.OnCalculate = new JavascriptAction("calculate();");
            priceField.Actions.OnCalculate = new JavascriptAction("calculate();");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields and JavaScript saved to '{outputPath}'.");
    }
}
