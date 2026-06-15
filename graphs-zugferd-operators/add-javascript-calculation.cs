using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

namespace AddJavaScriptCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with a single page (self‑contained example)
            using (Document createDoc = new Document())
            {
                Page page = createDoc.Pages.Add();
                createDoc.Save("input.pdf");
            }

            // Open the sample PDF and add form fields with JavaScript calculation
            using (Document doc = new Document("input.pdf"))
            {
                // Define rectangles for fields (use Aspose.Pdf.Rectangle explicitly to avoid ambiguity)
                Aspose.Pdf.Rectangle price1Rect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
                Aspose.Pdf.Rectangle price2Rect = new Aspose.Pdf.Rectangle(100, 660, 200, 680);
                Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 620, 200, 640);

                // Create price fields
                TextBoxField price1Field = new TextBoxField(doc, price1Rect);
                price1Field.PartialName = "Price1";
                price1Field.Value = "0";

                TextBoxField price2Field = new TextBoxField(doc, price2Rect);
                price2Field.PartialName = "Price2";
                price2Field.Value = "0";

                // Create total field (read‑only summary field)
                TextBoxField totalField = new TextBoxField(doc, totalRect);
                totalField.PartialName = "Total";
                totalField.ReadOnly = true;

                // Add fields to the form on page 1 (1‑based indexing)
                doc.Form.Add(price1Field, 1);
                doc.Form.Add(price2Field, 1);
                doc.Form.Add(totalField, 1);

                // Attach JavaScript to calculate total when the field is evaluated
                JavascriptAction calcAction = new JavascriptAction(
                    "event.value = parseFloat(this.getField('Price1').value) + parseFloat(this.getField('Price2').value);"
                );
                totalField.Actions.OnCalculate = calcAction;

                // Save the result
                doc.Save("output.pdf");
            }
        }
    }
}
