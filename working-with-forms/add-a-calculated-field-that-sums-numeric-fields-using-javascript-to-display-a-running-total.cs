using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "calculated.pdf";

        // Use the recommended using pattern for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the fields
            Page page = doc.Pages.Add();

            // Define field rectangles (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 650, 200, 670);
            Aspose.Pdf.Rectangle rectTotal = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create two numeric input fields
            NumberField numField1 = new NumberField(page, rect1);
            numField1.PartialName = "num1";
            numField1.Value = "0";

            NumberField numField2 = new NumberField(page, rect2);
            numField2.PartialName = "num2";
            numField2.Value = "0";

            // Create a read‑only field that will display the sum
            NumberField totalField = new NumberField(page, rectTotal);
            totalField.PartialName = "total";
            totalField.ReadOnly = true;

            // JavaScript that sums the two numeric fields
            string js = "event.value = this.getField('num1').value + this.getField('num2').value;";
            JavascriptAction calcAction = new JavascriptAction(js);
            totalField.Actions.OnCalculate = calcAction;

            // Add fields to the document form
            doc.Form.Add(numField1);
            doc.Form.Add(numField2);
            doc.Form.Add(totalField);

            // Enable automatic recalculation when any field changes
            doc.Form.AutoRecalculate = true;

            // No explicit Recalculate call – AutoRecalculate handles it

            // Save the PDF (standard Save without extra options writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated field saved to '{outputPath}'.");
    }
}
