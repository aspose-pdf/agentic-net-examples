using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields for line items (e.g., Qty1, Price1, Qty2, Price2, …) 
        // and a field named "TotalAmount" where the calculated total will be stored.
        const string inputPath  = "order_form.pdf";
        const string outputPath = "order_form_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Disable automatic recalculation while we set intermediate values for performance.
            doc.Form.AutoRecalculate = false;

            // Helper to safely retrieve numeric values from form fields.
            double GetFieldValue(string fieldName)
            {
                if (!doc.Form.HasField(fieldName))
                    return 0.0;

                // The indexer returns a WidgetAnnotation; cast to a concrete field type that exposes Value.
                if (doc.Form[fieldName] is TextBoxField txtField)
                {
                    if (txtField.Value == null)
                        return 0.0;

                    if (double.TryParse(txtField.Value.ToString(), out double result))
                        return result;
                }
                // If the field is not a TextBoxField (e.g., a button), treat it as zero.
                return 0.0;
            }

            // Example: assume up to 5 line items. Adjust as needed.
            const int maxItems = 5;
            double total = 0.0;

            for (int i = 1; i <= maxItems; i++)
            {
                // Field naming convention: Qty{i}, Price{i}
                string qtyName  = $"Qty{i}";
                string priceName = $"Price{i}";

                double qty   = GetFieldValue(qtyName);
                double price = GetFieldValue(priceName);

                total += qty * price;
            }

            // Set the calculated total into the "TotalAmount" field.
            if (doc.Form.HasField("TotalAmount") && doc.Form["TotalAmount"] is TextBoxField totalField)
            {
                // Format as currency with two decimal places.
                totalField.Value = total.ToString("F2");
            }

            // Re‑enable auto‑recalculation (optional). The document will recalculate when saved.
            doc.Form.AutoRecalculate = true;

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Order total calculated and saved to '{outputPath}'.");
    }
}
