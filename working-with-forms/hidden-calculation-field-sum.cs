using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Aspose.Pdf.Rectangle

class Program
{
    static void Main()
    {
        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            // Add a blank page (required before placing fields)
            Page page = doc.Pages.Add();

            // ---------- Item1 field ----------
            // Aspose.Pdf.Rectangle(x1, y1, width, height) – coordinates are in points
            NumberField item1 = new NumberField(page,
                new Aspose.Pdf.Rectangle(100, 700, 100, 20)); // left=100, bottom=700, width=100, height=20
            item1.PartialName = "Item1";
            item1.AlternateName = "First item";
            doc.Form.Add(item1);

            // ---------- Item2 field ----------
            NumberField item2 = new NumberField(page,
                new Aspose.Pdf.Rectangle(100, 660, 100, 20));
            item2.PartialName = "Item2";
            item2.AlternateName = "Second item";
            doc.Form.Add(item2);

            // ---------- Hidden calculation field ----------
            NumberField total = new NumberField(page,
                new Aspose.Pdf.Rectangle(100, 620, 100, 20));
            total.PartialName = "Total";
            total.AlternateName = "Sum of Item1 and Item2";
            total.ReadOnly = true;                     // make it non‑editable
            total.Color = Aspose.Pdf.Color.Transparent; // make it invisible (optional)

            // JavaScript that sums the two fields
            // 'event.value' is the value of the current field
            // 'this.getField(name).value' retrieves the value of another field
            JavascriptAction calcJs = new JavascriptAction(
                "event.value = this.getField('Item1').value + this.getField('Item2').value;");
            total.Actions.OnCalculate = calcJs;        // assign the calculation script

            // Hide the field using HideAction (field inherits from WidgetAnnotation)
            HideAction hide = new HideAction(total, true);
            // The HideAction is automatically attached to the field when constructed

            doc.Form.Add(total);

            // Optional: define calculation order (ensure 'Total' is processed after Item1 and Item2)
            doc.Form.CalculatedFields = new Field[] { item1, item2, total };

            // Save the PDF
            doc.Save("CalculatedForm.pdf");
        }

        Console.WriteLine("PDF with hidden calculation field created successfully.");
    }
}