using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // for JavascriptAction
using Aspose.Pdf.Forms;               // for form fields

class Program
{
    static void Main()
    {
        const string outputPath = "SumForm.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (coordinates are in points)
            Aspose.Pdf.Rectangle rectField1 = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            Aspose.Pdf.Rectangle rectField2 = new Aspose.Pdf.Rectangle(100, 650, 250, 680);
            Aspose.Pdf.Rectangle rectResult = new Aspose.Pdf.Rectangle(100, 600, 250, 630);
            Aspose.Pdf.Rectangle rectButton = new Aspose.Pdf.Rectangle(300, 700, 380, 730);

            // Create first numeric text box field
            TextBoxField field1 = new TextBoxField(page, rectField1);
            field1.PartialName = "field1";
            field1.Contents = "Number 1";
            field1.Value = "0";
            doc.Form.Add(field1);

            // Create second numeric text box field
            TextBoxField field2 = new TextBoxField(page, rectField2);
            field2.PartialName = "field2";
            field2.Contents = "Number 2";
            field2.Value = "0";
            doc.Form.Add(field2);

            // Create result text box field (read‑only)
            TextBoxField resultField = new TextBoxField(page, rectResult);
            resultField.PartialName = "result";
            resultField.Contents = "Sum";
            resultField.Value = "0";
            resultField.ReadOnly = true;
            doc.Form.Add(resultField);

            // Create a push button field
            ButtonField sumButton = new ButtonField(page, rectButton);
            sumButton.PartialName = "sumButton";
            sumButton.Contents = "Calculate Sum";

            // JavaScript to read the two fields, compute the sum, and set the result field
            string jsCode = @"
                var a = this.getField('field1').value;
                var b = this.getField('field2').value;
                var sum = parseFloat(a) + parseFloat(b);
                this.getField('result').value = sum;
            ";

            // Assign the JavaScript action to the button's activation event
            sumButton.OnActivated = new JavascriptAction(jsCode);
            doc.Form.Add(sumButton);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with sum button saved to '{outputPath}'.");
    }
}