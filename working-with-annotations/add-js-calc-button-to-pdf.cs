using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF with numeric fields
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field on the page
            ButtonField calcButton = new ButtonField(page, btnRect)
            {
                // Internal name of the field
                PartialName = "CalcButton",
                // Text shown on the button
                NormalCaption = "Calculate Total"
            };

            // JavaScript that sums two numeric fields (field1, field2) and writes the result to a field named "total"
            string jsCode = @"
                var f1 = this.getField('field1').value;
                var f2 = this.getField('field2').value;
                var total = (parseFloat(f1) || 0) + (parseFloat(f2) || 0);
                this.getField('total').value = total;
            ";

            // Assign the JavaScript to the button's activation action
            calcButton.OnActivated = new JavascriptAction(jsCode);

            // Add the button to the page annotations collection
            page.Annotations.Add(calcButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with button saved to '{outputPath}'.");
    }
}