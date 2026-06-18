using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF (can be blank)
        const string outputPath = "output_with_calculations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure automatic recalculation is enabled (default is true)
            doc.Form.AutoRecalculate = true;

            // -----------------------------------------------------------------
            // Create form fields: Quantity, Price, and Total (all on page 1)
            // -----------------------------------------------------------------
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle qtyRect   = new Aspose.Pdf.Rectangle(100, 700, 200, 720);
            Aspose.Pdf.Rectangle priceRect = new Aspose.Pdf.Rectangle(100, 660, 200, 680);
            Aspose.Pdf.Rectangle totalRect = new Aspose.Pdf.Rectangle(100, 620, 200, 640);

            NumberField quantity = new NumberField(doc.Pages[1], qtyRect);
            quantity.Name = "Quantity";
            quantity.PartialName = "Quantity";
            quantity.Value = "0";

            NumberField price = new NumberField(doc.Pages[1], priceRect);
            price.Name = "Price";
            price.PartialName = "Price";
            price.Value = "0";

            NumberField total = new NumberField(doc.Pages[1], totalRect);
            total.Name = "Total";
            total.PartialName = "Total";
            total.ReadOnly = true; // user should not edit directly

            // Add fields to the form
            doc.Form.Add(quantity);
            doc.Form.Add(price);
            doc.Form.Add(total);

            // -----------------------------------------------------------------
            // Define calculation for the Total field using JavaScript
            // -----------------------------------------------------------------
            // The JavaScript accesses other fields by their names and sets the result
            total.Actions.OnCalculate = new JavascriptAction(
                "event.value = this.getField('Quantity').value * this.getField('Price').value;"
            );

            // -----------------------------------------------------------------
            // Specify the order in which calculated fields are evaluated
            // -----------------------------------------------------------------
            // Only the Total field needs calculation, but we list it explicitly
            doc.Form.CalculatedFields = new[] { total };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with calculated fields saved to '{outputPath}'.");
    }
}