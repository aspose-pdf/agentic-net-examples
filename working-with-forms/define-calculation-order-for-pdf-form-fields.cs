using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction resides here

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;

            // Item1 field
            TextBoxField item1 = form["Item1"] as TextBoxField;
            if (item1 == null)
            {
                item1 = new TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(100, 700, 200, 720))
                { PartialName = "Item1" };
                form.Add(item1);
            }

            // Item2 field
            TextBoxField item2 = form["Item2"] as TextBoxField;
            if (item2 == null)
            {
                item2 = new TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(100, 660, 200, 680))
                { PartialName = "Item2" };
                form.Add(item2);
            }

            // Total field
            TextBoxField total = form["Total"] as TextBoxField;
            if (total == null)
            {
                total = new TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(100, 620, 200, 640))
                { PartialName = "Total" };
                form.Add(total);
            }

            // NOTE: The CalcOrder property is not available in the referenced Aspose.Pdf version.
            // Calculation order is handled automatically when AutoRecalculate is enabled.

            // JavaScript that calculates the total (correct class name is JavascriptAction)
            total.Actions.OnCalculate = new JavascriptAction(
                "event.value = Number(this.getField('Item1').value) + Number(this.getField('Item2').value);"
            );

            // Enable automatic recalculation of fields
            form.AutoRecalculate = true;

            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
