using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";   // can be an empty PDF or existing one
        const string outputPath = "form_with_sum_button.pdf";

        // Ensure the input PDF exists; if not, create a blank one with a single page
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add(); // add first page
                blank.Save(inputPath);
            }
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the form fields
            Page page = doc.Pages[1];

            // Define rectangles for the fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectField1 = new Aspose.Pdf.Rectangle(50, 700, 200, 730);
            Aspose.Pdf.Rectangle rectField2 = new Aspose.Pdf.Rectangle(50, 650, 200, 680);
            Aspose.Pdf.Rectangle rectResult = new Aspose.Pdf.Rectangle(50, 600, 200, 630);
            Aspose.Pdf.Rectangle rectButton = new Aspose.Pdf.Rectangle(250, 700, 350, 730);

            // Create first input text box
            TextBoxField field1 = new TextBoxField(page, rectField1)
            {
                PartialName = "field1",
                Value = "0"
            };
            doc.Form.Add(field1);

            // Create second input text box
            TextBoxField field2 = new TextBoxField(page, rectField2)
            {
                PartialName = "field2",
                Value = "0"
            };
            doc.Form.Add(field2);

            // Create result text box (read‑only)
            TextBoxField resultField = new TextBoxField(page, rectResult)
            {
                PartialName = "result",
                Value = "0",
                ReadOnly = true
            };
            doc.Form.Add(resultField);

            // JavaScript to calculate sum and place it in the result field
            string js = @"
                var f1 = this.getField('field1').value;
                var f2 = this.getField('field2').value;
                var sum = parseFloat(f1) + parseFloat(f2);
                this.getField('result').value = sum;
            ";

            // Create a push button (ButtonField) that runs the JavaScript
            ButtonField sumButton = new ButtonField(page, rectButton)
            {
                PartialName = "calcButton"
            };
            // Appearance must be set before setting the caption
            sumButton.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            sumButton.NormalCaption = "Calculate Sum";
            // Assign JavaScript action to the button – use a valid action property
            sumButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

            doc.Form.Add(sumButton);

            // Save the PDF with the interactive form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sum button saved to '{outputPath}'.");
    }
}
