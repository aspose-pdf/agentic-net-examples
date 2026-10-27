using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_hidden_sum.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectItem1 = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle rectItem2 = new Aspose.Pdf.Rectangle(100, 650, 200, 670);
            Aspose.Pdf.Rectangle rectTotal = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create Item1 text box field
            TextBoxField item1 = new TextBoxField(page, rectItem1)
            {
                PartialName = "Item1",
                Value = "0"
            };

            // Create Item2 text box field
            TextBoxField item2 = new TextBoxField(page, rectItem2)
            {
                PartialName = "Item2",
                Value = "0"
            };

            // Create hidden total field
            TextBoxField total = new TextBoxField(page, rectTotal)
            {
                PartialName = "Total",
                Value = "0"
            };
            // Hide the field from the viewer using annotation flags
            total.Flags = AnnotationFlags.Hidden;

            // JavaScript that calculates the sum of Item1 and Item2
            // Number() ensures string values are treated as numbers
            string js = "event.value = Number(this.getField('Item1').value) + Number(this.getField('Item2').value);";
            total.Actions.OnCalculate = new JavascriptAction(js);

            // Add fields to the document's form collection
            doc.Form.Add(item1);
            doc.Form.Add(item2);
            doc.Form.Add(total);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden calculation field saved to '{outputPath}'.");
    }
}
