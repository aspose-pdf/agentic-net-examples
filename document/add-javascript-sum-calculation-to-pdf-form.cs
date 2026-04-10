using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        Document doc;
        if (File.Exists(inputPath))
        {
            // Load existing PDF
            doc = new Document(inputPath);
        }
        else
        {
            // Create a new PDF with a single blank page when the source file is missing
            doc = new Document();
            doc.Pages.Add();
        }

        // Ensure that calculated fields are updated automatically
        doc.Form.AutoRecalculate = true;

        // -----------------------------------------------------------------
        // Ensure the two source fields exist (named "Field1" and "Field2")
        // -----------------------------------------------------------------
        TextBoxField field1;
        if (!doc.Form.HasField("Field1"))
        {
            field1 = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 700, 250, 730))
            {
                PartialName = "Field1",
                Name = "Field1"
            };
            doc.Form.Add(field1);
        }
        else
        {
            // The indexer returns a WidgetAnnotation; cast it to TextBoxField (or Field)
            field1 = doc.Form["Field1"] as TextBoxField;
            if (field1 == null)
                throw new InvalidOperationException("Existing field 'Field1' is not a TextBoxField.");
        }

        TextBoxField field2;
        if (!doc.Form.HasField("Field2"))
        {
            field2 = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 650, 250, 680))
            {
                PartialName = "Field2",
                Name = "Field2"
            };
            doc.Form.Add(field2);
        }
        else
        {
            field2 = doc.Form["Field2"] as TextBoxField;
            if (field2 == null)
                throw new InvalidOperationException("Existing field 'Field2' is not a TextBoxField.");
        }

        // ---------------------------------------------------------------
        // Create the result field that will display the sum (read‑only)
        // ---------------------------------------------------------------
        TextBoxField sumField;
        if (!doc.Form.HasField("SumField"))
        {
            sumField = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 600, 250, 630))
            {
                PartialName = "SumField",
                Name = "SumField",
                ReadOnly = true // make the field non‑editable
            };
            doc.Form.Add(sumField);
        }
        else
        {
            sumField = doc.Form["SumField"] as TextBoxField;
            if (sumField == null)
                throw new InvalidOperationException("Existing field 'SumField' is not a TextBoxField.");
        }

        // ---------------------------------------------------------------
        // JavaScript that reads the two source fields, adds them,
        // and writes the result into the sum field.
        // ---------------------------------------------------------------
        string js = @"
            var v1 = this.getField('Field1').value;
            var v2 = this.getField('Field2').value;
            var sum = (parseFloat(v1) || 0) + (parseFloat(v2) || 0);
            this.getField('SumField').value = sum;
        ";

        // Attach the script to the sum field's OnCalculate action
        sumField.Actions.OnCalculate = new JavascriptAction(js);

        // Optionally trigger the calculation immediately (e.g., for a fresh PDF)
        sumField.ExecuteFieldJavaScript(new JavascriptAction(js));

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF with custom JavaScript saved to '{outputPath}'.");
    }
}
