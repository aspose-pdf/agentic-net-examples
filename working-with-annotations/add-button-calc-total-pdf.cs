using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form object (created lazily when needed)
            Form pdfForm = doc.Form;

            // Target the first page
            Page page = doc.Pages[1];

            // Define button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a button field (acts as a button annotation)
            ButtonField button = new ButtonField(page, rect)
            {
                PartialName = "CalcTotalButton",
                Color = Color.LightGray,
                Value = "Calculate"
            };

            // Border must be created with the parent annotation instance
            button.Border = new Border(button) { Width = 1 };

            // JavaScript that sums fields named Field1, Field2, Field3 and writes to Total
            string js = @"
var f1 = this.getField('Field1').value;
var f2 = this.getField('Field2').value;
var f3 = this.getField('Field3').value;
var total = (parseFloat(f1) || 0) + (parseFloat(f2) || 0) + (parseFloat(f3) || 0);
this.getField('Total').value = total;
";

            // Assign the script to the button's press‑mouse‑button action (executed when the button is clicked)
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form (this also adds the annotation to the page)
            pdfForm.Add(button);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}
