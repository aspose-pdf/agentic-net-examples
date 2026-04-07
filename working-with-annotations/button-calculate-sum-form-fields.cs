using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ---------- First number field ----------
            // Rectangle: left, bottom, width, height
            NumberField field1 = new NumberField(page, new Rectangle(100, 700, 100, 20));
            field1.PartialName = "field1";               // field name used in JavaScript
            field1.Contents = "Number 1";
            doc.Form.Add(field1);

            // ---------- Second number field ----------
            NumberField field2 = new NumberField(page, new Rectangle(100, 660, 100, 20));
            field2.PartialName = "field2";
            field2.Contents = "Number 2";
            doc.Form.Add(field2);

            // ---------- Result field ----------
            TextBoxField sumField = new TextBoxField(page, new Rectangle(100, 620, 100, 20));
            sumField.PartialName = "sumField";
            sumField.Contents = "Result";
            doc.Form.Add(sumField);

            // ---------- Button that triggers the calculation ----------
            ButtonField calcButton = new ButtonField(page, new Rectangle(100, 580, 100, 20));
            calcButton.PartialName = "calcButton";
            calcButton.Contents = "Calculate";

            // JavaScript to sum the two fields and place the result in sumField
            string js = @"
                var f1 = this.getField('field1').value;
                var f2 = this.getField('field2').value;
                var sum = parseFloat(f1) + parseFloat(f2);
                this.getField('sumField').value = sum;
            ";
            // Assign the script to the button's activation action
            calcButton.OnActivated = new JavascriptAction(js);

            doc.Form.Add(calcButton);

            // Save the PDF
            doc.Save("ButtonCalculateSum.pdf");
        }

        Console.WriteLine("PDF with button calculation created successfully.");
    }
}