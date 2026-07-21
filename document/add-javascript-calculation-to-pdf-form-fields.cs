using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "sum_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define field rectangles (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 650, 250, 680);
            Aspose.Pdf.Rectangle rectSum = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the first numeric field
            NumberField field1 = new NumberField(page, rect1);
            field1.PartialName = "field1";
            field1.Contents = "Field 1";
            doc.Form.Add(field1);

            // Create the second numeric field
            NumberField field2 = new NumberField(page, rect2);
            field2.PartialName = "field2";
            field2.Contents = "Field 2";
            doc.Form.Add(field2);

            // Create a read‑only field that will display the sum
            TextBoxField sumField = new TextBoxField(page, rectSum);
            sumField.PartialName = "sumField";
            sumField.Contents = "Sum";
            sumField.ReadOnly = true;
            doc.Form.Add(sumField);

            // JavaScript that calculates the sum of field1 and field2
            string js = @"
                var f1 = this.getField('field1').value;
                var f2 = this.getField('field2').value;
                var v1 = isNaN(parseFloat(f1)) ? 0 : parseFloat(f1);
                var v2 = isNaN(parseFloat(f2)) ? 0 : parseFloat(f2);
                event.value = v1 + v2;
            ";

            // Attach the calculation script to the sum field
            sumField.Actions.OnCalculate = new JavascriptAction(js);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated sum saved to '{outputPath}'.");
    }
}