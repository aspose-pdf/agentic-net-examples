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

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the two input fields and the result field
            Aspose.Pdf.Rectangle rect1   = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            Aspose.Pdf.Rectangle rect2   = new Aspose.Pdf.Rectangle(100, 650, 250, 680);
            Aspose.Pdf.Rectangle rectSum = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the first number field
            NumberField field1 = new NumberField(page, rect1);
            field1.PartialName = "Field1";
            field1.Contents = "0";

            // Create the second number field
            NumberField field2 = new NumberField(page, rect2);
            field2.PartialName = "Field2";
            field2.Contents = "0";

            // Create a read‑only field that will display the sum
            NumberField sumField = new NumberField(page, rectSum);
            sumField.PartialName = "SumField";
            sumField.ReadOnly = true;
            sumField.Contents = "0";

            // Add the fields to the form (page numbers are 1‑based)
            doc.Form.Add(field1, 1);
            doc.Form.Add(field2, 1);
            doc.Form.Add(sumField, 1);

            // JavaScript that calculates the sum of the two input fields
            string js = @"
                var v1 = this.getField('Field1').value;
                var v2 = this.getField('Field2').value;
                var sum = Number(v1) + Number(v2);
                this.getField('SumField').value = sum;
            ";

            // Attach the script to the OnCalculate action of the result field
            sumField.Actions.OnCalculate = new JavascriptAction(js);

            // Also trigger the calculation when either input field changes
            field1.Actions.OnCalculate = new JavascriptAction(js);
            field2.Actions.OnCalculate = new JavascriptAction(js);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated sum saved to '{outputPath}'.");
    }
}