using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths
        const string outputPath = "sum_form.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // First input field
            TextBoxField field1 = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 250, 720));
            field1.PartialName = "field1";
            field1.Value = "";
            doc.Form.Add(field1);

            // Second input field
            TextBoxField field2 = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 250, 670));
            field2.PartialName = "field2";
            field2.Value = "";
            doc.Form.Add(field2);

            // Result field (read‑only)
            TextBoxField resultField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 250, 620));
            resultField.PartialName = "result";
            resultField.ReadOnly = true;
            resultField.Value = "";
            doc.Form.Add(resultField);

            // Button that triggers the JavaScript calculation
            ButtonField sumButton = new ButtonField(page, new Aspose.Pdf.Rectangle(100, 550, 250, 570));
            sumButton.PartialName = "sumButton";
            // Appearance must be set before assigning a caption
            sumButton.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            sumButton.NormalCaption = "Calculate Sum";

            // JavaScript: read values, compute sum, display in result field
            string js = @"
                var f1 = this.getField('field1').value;
                var f2 = this.getField('field2').value;
                var n1 = parseFloat(f1);
                var n2 = parseFloat(f2);
                var sum = n1 + n2;
                if (!isNaN(sum)) {
                    this.getField('result').value = sum;
                } else {
                    this.getField('result').value = '';
                }";

            // Assign the JavaScript action to the button's mouse‑up (release) event
            sumButton.Actions.OnReleaseMouseBtn = new JavascriptAction(js);
            doc.Form.Add(sumButton);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript sum action saved to '{outputPath}'.");
    }
}
