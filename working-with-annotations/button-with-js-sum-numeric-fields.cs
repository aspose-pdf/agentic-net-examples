using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "ButtonWithJs.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // ---- Create numeric fields ----
            // First number field
            NumberField numField1 = new NumberField(page,
                new Aspose.Pdf.Rectangle(50, 700, 150, 720));
            numField1.Name = "num1";
            numField1.AlternateName = "Number 1"; // Fixed property name
            doc.Form.Add(numField1);

            // Second number field
            NumberField numField2 = new NumberField(page,
                new Aspose.Pdf.Rectangle(200, 700, 300, 720));
            numField2.Name = "num2";
            numField2.AlternateName = "Number 2"; // Fixed property name
            doc.Form.Add(numField2);

            // Result field (read‑only)
            NumberField totalField = new NumberField(page,
                new Aspose.Pdf.Rectangle(350, 700, 450, 720));
            totalField.Name = "total";
            totalField.AlternateName = "Total"; // Fixed property name
            totalField.ReadOnly = true;
            doc.Form.Add(totalField);

            // ---- Create a push button that runs JavaScript ----
            ButtonField calcButton = new ButtonField(page,
                new Aspose.Pdf.Rectangle(50, 650, 150, 670));
            calcButton.Name = "calcButton";
            calcButton.AlternateName = "Calculate";

            // JavaScript to sum the two numeric fields and place the result in 'total'
            string js = @"
                var v1 = this.getField('num1').value;
                var v2 = this.getField('num2').value;
                var sum = parseFloat(v1) + parseFloat(v2);
                this.getField('total').value = sum;
            ";

            // Assign the script to the button's mouse‑up (release) action
            calcButton.Actions.OnReleaseMouseBtn = new JavascriptAction(js);

            // Add the button to the form (and thus to the page)
            doc.Form.Add(calcButton);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with button and JavaScript saved to '{outputPath}'.");
    }
}
