using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "FormWithTotal.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, width, height)
            Aspose.Pdf.Rectangle rectNum1 = new Aspose.Pdf.Rectangle(50, 700, 150, 720);
            Aspose.Pdf.Rectangle rectNum2 = new Aspose.Pdf.Rectangle(50, 650, 150, 670);
            Aspose.Pdf.Rectangle rectNum3 = new Aspose.Pdf.Rectangle(50, 600, 150, 620);
            Aspose.Pdf.Rectangle rectTotal = new Aspose.Pdf.Rectangle(50, 540, 150, 560);
            Aspose.Pdf.Rectangle rectButton = new Aspose.Pdf.Rectangle(200, 700, 300, 720);

            // Create three numeric fields
            NumberField num1 = new NumberField(doc, rectNum1) { Name = "num1", PartialName = "num1" };
            NumberField num2 = new NumberField(doc, rectNum2) { Name = "num2", PartialName = "num2" };
            NumberField num3 = new NumberField(doc, rectNum3) { Name = "num3", PartialName = "num3" };

            // Create a read‑only text field to display the total
            TextBoxField total = new TextBoxField(doc, rectTotal)
            {
                Name = "total",
                PartialName = "total",
                ReadOnly = true,
                Value = "0"
            };

            // Create a push button that will trigger the calculation
            // In Aspose.PDF the class is ButtonField (PushButtonField does not exist)
            ButtonField calcBtn = new ButtonField(doc, rectButton)
            {
                Name = "calcBtn",
                PartialName = "calcBtn",
                // Button label
                Contents = "Calculate Total"
            };

            // JavaScript that sums the three numeric fields and writes the result to the total field
            string jsCode = @"
                var f1 = this.getField('num1').value;
                var f2 = this.getField('num2').value;
                var f3 = this.getField('num3').value;
                var sum = (parseFloat(f1) || 0) + (parseFloat(f2) || 0) + (parseFloat(f3) || 0);
                this.getField('total').value = sum;
            ";

            // Attach the JavaScript to the button's mouse‑up (release) action
            calcBtn.Actions.OnReleaseMouseBtn = new JavascriptAction(jsCode);

            // Ensure the form recalculates automatically when fields change (optional)
            doc.Form.AutoRecalculate = true;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated total saved to '{outputPath}'.");
    }
}
