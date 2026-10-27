using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // First numeric text box
            TextBoxField field1 = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 200, 730));
            field1.PartialName = "Field1";
            field1.Value = "0";
            doc.Form.Add(field1);

            // Second numeric text box
            TextBoxField field2 = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 650, 200, 680));
            field2.PartialName = "Field2";
            field2.Value = "0";
            doc.Form.Add(field2);

            // Result text box (read‑only)
            TextBoxField result = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 200, 630));
            result.PartialName = "Result";
            result.Value = "";
            result.ReadOnly = true;
            doc.Form.Add(result);

            // Button that triggers JavaScript to calculate the sum
            ButtonField sumButton = new ButtonField(page, new Aspose.Pdf.Rectangle(100, 550, 200, 580));
            sumButton.PartialName = "SumButton";
            // Use AlternateCaption (or NormalCaption) instead of the non‑existent Caption property
            sumButton.AlternateCaption = "Calculate Sum";

            // JavaScript code executed when the button is pressed
            string js = @"
                var f1 = this.getField('Field1').value;
                var f2 = this.getField('Field2').value;
                var n1 = parseFloat(f1);
                var n2 = parseFloat(f2);
                if (isNaN(n1)) n1 = 0;
                if (isNaN(n2)) n2 = 0;
                this.getField('Result').value = (n1 + n2).toString();
            ";

            // Assign the JavaScript action to the button's OnActivated event
            sumButton.OnActivated = new JavascriptAction(js);
            doc.Form.Add(sumButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript button saved to '{outputPath}'.");
    }
}
