using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // JavascriptAction resides here

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";   // existing PDF with at least one page
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Template not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Enable automatic recalculation (default is true, set explicitly for clarity)
            doc.Form.AutoRecalculate = true;

            // ------------------------------------------------------------
            // Create form fields
            // ------------------------------------------------------------
            Page page = doc.Pages[1]; // 1‑based page indexing

            // Define rectangles for the fields (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rectQty1   = new Aspose.Pdf.Rectangle(100, 600, 200, 620);
            Aspose.Pdf.Rectangle rectQty2   = new Aspose.Pdf.Rectangle(100, 560, 200, 580);
            Aspose.Pdf.Rectangle rectTotal  = new Aspose.Pdf.Rectangle(100, 520, 200, 540);

            // Input fields (numeric)
            NumberField qty1 = new NumberField(page, rectQty1)
            {
                PartialName = "Qty1",
                Value = "0"
            };

            NumberField qty2 = new NumberField(page, rectQty2)
            {
                PartialName = "Qty2",
                Value = "0"
            };

            // Output field (read‑only total)
            TextBoxField total = new TextBoxField(page, rectTotal)
            {
                PartialName = "Total",
                ReadOnly = true
            };

            // Add fields to the form collection
            doc.Form.Add(qty1);
            doc.Form.Add(qty2);
            doc.Form.Add(total);

            // ------------------------------------------------------------
            // Define calculation logic for the total field
            // ------------------------------------------------------------
            // JavaScript that sums the two quantity fields
            string js = "event.value = this.getField('Qty1').value + this.getField('Qty2').value;";
            total.Actions.OnCalculate = new JavascriptAction(js);

            // ------------------------------------------------------------
            // Specify calculation order
            // ------------------------------------------------------------
            // Total must be calculated after the two input fields
            doc.Form.CalculatedFields = new[] { total };

            // No explicit Recalculate call is needed – AutoRecalculate will handle it.

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
