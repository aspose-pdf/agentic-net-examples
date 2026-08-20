using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a page and keep a reference to it (Aspose.Pdf pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            Aspose.Pdf.Rectangle rectItem1 = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle rectItem2 = new Aspose.Pdf.Rectangle(100, 660, 200, 680);
            Aspose.Pdf.Rectangle rectTotal = new Aspose.Pdf.Rectangle(100, 620, 200, 640);

            // Create visible text fields for Item1 and Item2 – note the constructor takes a Page, not a Document
            TextBoxField item1 = new TextBoxField(page, rectItem1) { PartialName = "Item1" };
            TextBoxField item2 = new TextBoxField(page, rectItem2) { PartialName = "Item2" };

            // Create the hidden calculation field (Total)
            TextBoxField total = new TextBoxField(page, rectTotal) { PartialName = "Total" };
            total.ReadOnly = true;                     // make it read‑only
            total.Flags = AnnotationFlags.Hidden;      // hide the field from the viewer

            // Attach JavaScript that sums Item1 and Item2 numeric values
            total.Actions.OnCalculate = new JavascriptAction(
                "var v1 = parseFloat(this.getField('Item1').value) || 0;" +
                "var v2 = parseFloat(this.getField('Item2').value) || 0;" +
                "event.value = v1 + v2;");

            // Add fields to the form
            doc.Form.Add(item1);
            doc.Form.Add(item2);
            doc.Form.Add(total);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with hidden calculation field created.");
    }
}
