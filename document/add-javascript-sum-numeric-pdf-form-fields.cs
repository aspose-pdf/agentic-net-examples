using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a read‑only textbox to display the total (if it does not already exist)
            // Note: Aspose.Pdf uses 1‑based page indexing
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);
            TextBoxField totalField = new TextBoxField(doc.Pages[1], totalRect)
            {
                PartialName = "Total",
                Value       = "0",
                ReadOnly    = true
            };
            doc.Form.Add(totalField, 1);

            // JavaScript that sums numeric fields named Field1, Field2, Field3
            // and writes the result into the Total field
            string js = @"
                var sum = 0;
                var fields = ['Field1','Field2','Field3'];
                for (var i = 0; i < fields.length; i++) {
                    var f = this.getField(fields[i]);
                    if (f && !isNaN(parseFloat(f.value))) {
                        sum += parseFloat(f.value);
                    }
                }
                this.getField('Total').value = sum.toString();
            ";

            // Attach the script to the document's Open action so it runs when the PDF is opened
            doc.OpenAction = new JavascriptAction(js);

            // Alternatively, you could attach the script to a button's MouseUp action:
            // Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 540, 200, 560);
            // PushButtonField calcBtn = new PushButtonField(doc.Pages[1], btnRect)
            // {
            //     PartialName = "CalcButton",
            //     Caption     = "Calculate",
            //     Action      = new JavascriptAction(js)
            // };
            // doc.Form.Add(calcBtn, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
    }
}
