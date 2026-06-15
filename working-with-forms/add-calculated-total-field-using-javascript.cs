using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure fields are recalculated automatically when any field changes
            doc.Form.AutoRecalculate = true;

            // ----- Add first numeric field -----
            // Rectangle coordinates: llx, lly, urx, ury
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            NumberField numField1 = new NumberField(doc, rect1);
            numField1.PartialName = "num1";   // field name used in JavaScript
            numField1.Value = "0";            // initial value
            doc.Form.Add(numField1);

            // ----- Add second numeric field -----
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 650, 200, 670);
            NumberField numField2 = new NumberField(doc, rect2);
            numField2.PartialName = "num2";
            numField2.Value = "0";
            doc.Form.Add(numField2);

            // ----- Add calculated total field -----
            Aspose.Pdf.Rectangle rectTotal = new Aspose.Pdf.Rectangle(100, 600, 200, 620);
            TextBoxField totalField = new TextBoxField(doc, rectTotal);
            totalField.PartialName = "total";
            totalField.ReadOnly = true; // user should not edit the total

            // JavaScript that sums the two numeric fields
            string js =
                "var f1 = this.getField('num1').value;" +
                "var f2 = this.getField('num2').value;" +
                "var sum = (parseFloat(f1) || 0) + (parseFloat(f2) || 0);" +
                "event.value = sum.toString();";

            // Assign the JavaScript to the OnCalculate action (AnnotationActionCollection.OnCalculate)
            totalField.Actions.OnCalculate = new JavascriptAction(js);
            doc.Form.Add(totalField);

            // No explicit Form.Calculate() call is required – AutoRecalculate will trigger the script
            // when the PDF is opened or when field values change in a viewer.

            // Save the modified PDF (using rule: save-to-non-pdf-always-use-save-options is not needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with calculated field to '{outputPath}'.");
    }
}
