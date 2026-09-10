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

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the fields
            Page page = doc.Pages.Add();

            // Define rectangles for the numeric input fields and the total field
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 660, 200, 680);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 620, 200, 640);

            // Create two numeric input fields
            NumberField field1 = new NumberField(page, rect1);
            field1.PartialName = "Field1";
            field1.Value = "0";

            NumberField field2 = new NumberField(page, rect2);
            field2.PartialName = "Field2";
            field2.Value = "0";

            // Create a read‑only field that will display the running total
            NumberField totalField = new NumberField(page, totalRect);
            totalField.PartialName = "Total";
            totalField.ReadOnly = true;

            // JavaScript that sums the two numeric fields
            string js = "event.value = this.getField('Field1').value + this.getField('Field2').value;";

            // Attach the JavaScript to the OnCalculate action of the total field
            totalField.Actions.OnCalculate = new JavascriptAction(js);

            // Add all fields to the document form
            doc.Form.Add(field1);
            doc.Form.Add(field2);
            doc.Form.Add(totalField);

            // Enable automatic recalculation when any field changes
            doc.Form.AutoRecalculate = true;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated field saved to '{outputPath}'.");
    }
}