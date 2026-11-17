using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "sum_form.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the fields and the button
            Rectangle fieldRect1 = new Rectangle(100, 700, 250, 730);
            Rectangle fieldRect2 = new Rectangle(300, 700, 450, 730);
            Rectangle buttonRect = new Rectangle(100, 600, 250, 630);
            Rectangle resultRect = new Rectangle(100, 500, 250, 530);

            // First input text box (field1)
            TextBoxField field1 = new TextBoxField(page, fieldRect1)
            {
                PartialName = "field1",
                Value = "0"
            };
            doc.Form.Add(field1, 1);

            // Second input text box (field2)
            TextBoxField field2 = new TextBoxField(page, fieldRect2)
            {
                PartialName = "field2",
                Value = "0"
            };
            doc.Form.Add(field2, 1);

            // Result text box (read‑only)
            TextBoxField resultField = new TextBoxField(page, resultRect)
            {
                PartialName = "result",
                Value = "0",
                ReadOnly = true
            };
            doc.Form.Add(resultField, 1);

            // Button that triggers JavaScript to calculate the sum
            ButtonField sumButton = new ButtonField(page, buttonRect)
            {
                PartialName = "sumButton",
                // Appearance must be defined before setting the caption
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black),
                OnActivated = new JavascriptAction(@"
                    var f1 = this.getField('field1').value;
                    var f2 = this.getField('field2').value;
                    var sum = Number(f1) + Number(f2);
                    this.getField('result').value = sum;
                ")
            };
            // Set the visible label of the button
            sumButton.NormalCaption = "Calculate Sum";

            doc.Form.Add(sumButton, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sum button saved to '{outputPath}'.");
    }
}
