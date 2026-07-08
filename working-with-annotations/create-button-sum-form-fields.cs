using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ---------- Create first numeric field ----------
            NumberField field1 = new NumberField(page,
                new Aspose.Pdf.Rectangle(50, 600, 150, 620));
            field1.PartialName = "Field1";
            field1.AlternateName = "Value 1"; // Fixed property name
            // Add the field to the document's form collection
            doc.Form.Add(field1);

            // ---------- Create second numeric field ----------
            NumberField field2 = new NumberField(page,
                new Aspose.Pdf.Rectangle(200, 600, 300, 620));
            field2.PartialName = "Field2";
            field2.AlternateName = "Value 2"; // Fixed property name
            doc.Form.Add(field2);

            // ---------- Create result field ----------
            TextBoxField resultField = new TextBoxField(page,
                new Aspose.Pdf.Rectangle(350, 600, 450, 620));
            resultField.PartialName = "Result";
            resultField.AlternateName = "Sum"; // Fixed property name
            doc.Form.Add(resultField);

            // ---------- Create button that performs the calculation ----------
            ButtonField sumButton = new ButtonField(page,
                new Aspose.Pdf.Rectangle(100, 500, 200, 550));
            sumButton.PartialName = "SumButton";
            sumButton.AlternateName = "Calculate"; // Optional, for consistency

            // JavaScript that reads the two fields, adds them, and writes the result
            string js = @"
                var f1 = this.getField('Field1').value;
                var f2 = this.getField('Field2').value;
                var sum = parseFloat(f1) + parseFloat(f2);
                this.getField('Result').value = sum;
            ";

            // Assign the JavaScript to the button's mouse‑press action
            sumButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form collection
            doc.Form.Add(sumButton);

            // Save the PDF (lifecycle rule: use Document.Save)
            doc.Save("FormWithSumButton.pdf");
        }

        Console.WriteLine("PDF with button JavaScript created successfully.");
    }
}
