using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "order_form.pdf";
        const string outputPdf = "order_form_updated.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access the form object (core API, not Facades)
            Form form = doc.Form;

            // Disable automatic recalculation for performance
            form.AutoRecalculate = false;

            decimal totalAmount = 0m;

            // Collect all field names for quick lookup (case‑insensitive)
            HashSet<string> fieldNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Field f in form.Fields)
                fieldNames.Add(f.PartialName);

            // Iterate over quantity fields (assumed naming pattern: Qty1, Qty2, ...)
            foreach (Field qtyField in form.Fields)
            {
                if (!qtyField.PartialName.StartsWith("Qty", StringComparison.OrdinalIgnoreCase))
                    continue;

                // Derive the corresponding price field name
                string priceFieldName = qtyField.PartialName.Replace("Qty", "Price", StringComparison.OrdinalIgnoreCase);

                // Ensure the price field exists
                if (!fieldNames.Contains(priceFieldName))
                    continue;

                // Retrieve the price field via the indexer and cast to Field
                Field priceField = form[priceFieldName] as Field;
                if (priceField == null)
                    continue;

                // Parse numeric values (fallback to 0 if parsing fails)
                decimal qty = 0m;
                decimal price = 0m;
                Decimal.TryParse(qtyField.Value?.ToString(), out qty);
                Decimal.TryParse(priceField.Value?.ToString(), out price);

                // Accumulate line total
                totalAmount += qty * price;
            }

            // Set the total amount into the "Total" field (if it exists)
            if (fieldNames.Contains("Total"))
            {
                Field totalField = form["Total"] as Field;
                if (totalField != null)
                    totalField.Value = totalAmount.ToString("F2");
            }

            // Re‑enable auto‑recalculation (optional)
            form.AutoRecalculate = true;

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}
