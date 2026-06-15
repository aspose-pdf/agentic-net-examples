using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class TaxFormGenerator
{
    static void Main()
    {
        const string outputPath = "TaxForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define rectangles for the fields (left, bottom, right, top)
            // Positions are in points (1/72 inch)
            Aspose.Pdf.Rectangle subtotalRect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle rateRect     = new Aspose.Pdf.Rectangle(100, 650, 200, 670);
            Aspose.Pdf.Rectangle taxRect      = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create Subtotal field (numeric input)
            NumberField subtotalField = new NumberField(doc, subtotalRect)
            {
                PartialName = "Subtotal",
                Value = "0"
            };
            doc.Form.Add(subtotalField, 1);

            // Create Tax Rate field (percentage)
            NumberField rateField = new NumberField(doc, rateRect)
            {
                PartialName = "TaxRate",
                Value = "0"
            };
            doc.Form.Add(rateField, 1);

            // Create Tax Amount field (calculated, read‑only)
            NumberField taxField = new NumberField(doc, taxRect)
            {
                PartialName = "TaxAmount",
                ReadOnly = true,
                Value = "0"
            };
            doc.Form.Add(taxField, 1);

            // Attach JavaScript to calculate tax whenever any field changes
            string js = @"
                var subtotal = this.getField('Subtotal').value;
                var rate = this.getField('TaxRate').value;
                var tax = (Number(subtotal) * Number(rate)) / 100;
                event.value = tax.toFixed(2);
            ";
            taxField.Actions.OnCalculate = new JavascriptAction(js);

            // Add visual labels
            AddLabel(page, "Subtotal:", 50, 710);
            AddLabel(page, "Tax Rate (%):", 50, 660);
            AddLabel(page, "Tax Amount:", 50, 610);

            // Save the PDF – guard against missing libgdiplus on macOS/Linux
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Tax calculation form saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform. libgdiplus is required for PDF creation. Skipping doc.Save().");
            }
        }
    }

    // Helper method to place a simple text label on the page
    private static void AddLabel(Page page, string text, double x, double y)
    {
        TextFragment tf = new TextFragment(text)
        {
            Position = new Position(x, y)
        };
        tf.TextState.Font = FontRepository.FindFont("Helvetica");
        tf.TextState.FontSize = 12;
        tf.TextState.ForegroundColor = Color.Black;
        page.Paragraphs.Add(tf);
    }
}
