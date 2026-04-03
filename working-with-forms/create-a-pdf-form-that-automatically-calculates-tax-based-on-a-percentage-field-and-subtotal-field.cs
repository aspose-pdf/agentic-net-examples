using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class TaxFormGenerator
{
    static void Main()
    {
        const string outputPath = "tax_form.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // ---------- Subtotal field ----------
            // Rectangle: left, bottom, width, height
            Aspose.Pdf.Rectangle subtotalRect = new Aspose.Pdf.Rectangle(100, 700, 200, 20);
            NumberField subtotalField = new NumberField(page, subtotalRect);
            subtotalField.PartialName = "Subtotal";
            subtotalField.AlternateName = "Subtotal";
            subtotalField.TextHorizontalAlignment = HorizontalAlignment.Center;
            subtotalField.Border = new Border(subtotalField) { Width = 1 };
            doc.Form.Add(subtotalField);

            // ---------- Tax Rate field ----------
            Aspose.Pdf.Rectangle taxRateRect = new Aspose.Pdf.Rectangle(100, 650, 200, 20);
            NumberField taxRateField = new NumberField(page, taxRateRect);
            taxRateField.PartialName = "TaxRate";
            taxRateField.AlternateName = "Tax Rate (%)";
            taxRateField.TextHorizontalAlignment = HorizontalAlignment.Center;
            taxRateField.Border = new Border(taxRateField) { Width = 1 };
            doc.Form.Add(taxRateField);

            // ---------- Tax Amount (calculated) ----------
            Aspose.Pdf.Rectangle taxAmountRect = new Aspose.Pdf.Rectangle(100, 600, 200, 20);
            NumberField taxAmountField = new NumberField(page, taxAmountRect);
            taxAmountField.PartialName = "TaxAmount";
            taxAmountField.AlternateName = "Tax Amount";
            taxAmountField.ReadOnly = true;                     // user cannot edit
            taxAmountField.TextHorizontalAlignment = HorizontalAlignment.Center;
            taxAmountField.Border = new Border(taxAmountField) { Width = 1 };
            // JavaScript to calculate tax = Subtotal * TaxRate / 100
            string js = "event.value = this.getField('Subtotal').value * this.getField('TaxRate').value / 100;";
            taxAmountField.Actions.OnCalculate = new JavascriptAction(js);
            doc.Form.Add(taxAmountField);

            // Ensure automatic recalculation when any field changes
            doc.Form.AutoRecalculate = true;

            // Optional: add labels for visual clarity (using TextFragment)
            TextFragment labelSubtotal = new TextFragment("Subtotal:");
            labelSubtotal.Position = new Position(50, 710);
            page.Paragraphs.Add(labelSubtotal);

            TextFragment labelTaxRate = new TextFragment("Tax Rate (%):");
            labelTaxRate.Position = new Position(50, 660);
            page.Paragraphs.Add(labelTaxRate);

            TextFragment labelTaxAmount = new TextFragment("Tax Amount:");
            labelTaxAmount.Position = new Position(50, 610);
            page.Paragraphs.Add(labelTaxAmount);

            // ---------- Save the PDF ----------
            // On non‑Windows platforms Aspose.Pdf may require libgdiplus (GDI+). Guard the save call.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Tax calculation form saved to '{outputPath}'.");
            }
            else
            {
                // Attempt to save and gracefully handle missing GDI+.
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Tax calculation form saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated in memory but could not be saved to disk.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException caused by missing libgdiplus.
    private static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
