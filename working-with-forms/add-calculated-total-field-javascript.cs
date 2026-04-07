using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "calculated.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // A page is required for form fields
            Page page = doc.Pages.Add();

            // First numeric field
            Rectangle rect1 = new Rectangle(100, 700, 200, 720);
            NumberField field1 = new NumberField(page, rect1);
            field1.PartialName = "Field1";
            doc.Form.Add(field1);

            // Second numeric field
            Rectangle rect2 = new Rectangle(100, 650, 200, 670);
            NumberField field2 = new NumberField(page, rect2);
            field2.PartialName = "Field2";
            doc.Form.Add(field2);

            // Total field that sums the two numeric fields
            Rectangle rectTotal = new Rectangle(100, 600, 200, 620);
            NumberField totalField = new NumberField(page, rectTotal);
            totalField.PartialName = "Total";

            // JavaScript to calculate the running total
            totalField.Actions.OnCalculate = new JavascriptAction(
                "event.value = this.getField('Field1').value + this.getField('Field2').value;");

            doc.Form.Add(totalField);

            // Enable automatic recalculation when any field changes
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated field saved to '{outputPath}'.");
    }
}
