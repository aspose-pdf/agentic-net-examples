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
        const string buttonName = "calcButton";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // JavaScript that reads values of fields "price1" and "price2", adds them, and sets "total"
        string js = "var p1 = this.getField('price1').value;" +
                    "var p2 = this.getField('price2').value;" +
                    "var total = parseFloat(p1) + parseFloat(p2);" +
                    "this.getField('total').value = total;";

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the button field
            ButtonField button = doc.Form[buttonName] as ButtonField;
            if (button == null)
            {
                Console.Error.WriteLine($"Button field '{buttonName}' not found.");
                return;
            }

            // Attach JavaScript to the button's calculation action
            button.Actions.OnCalculate = new JavascriptAction(js);

            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript added and saved to '{outputPath}'.");
    }
}