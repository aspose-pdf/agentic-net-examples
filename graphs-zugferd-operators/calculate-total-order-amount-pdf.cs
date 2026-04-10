using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "order_form.pdf";   // PDF with line‑item fields
        const string outputPath = "order_form_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Disable automatic recalculation for performance while we compute the total manually
            form.AutoRecalculate = false;

            decimal totalAmount = 0m;

            // Iterate over all fields in the form
            foreach (Field field in form.Fields)
            {
                // Assume line‑item fields are named "Item1", "Item2", ... etc.
                // Adjust the prefix as needed for your specific form.
                if (!string.IsNullOrEmpty(field.PartialName) &&
                    field.PartialName.StartsWith("Item", StringComparison.OrdinalIgnoreCase))
                {
                    // Try to parse the field value as a decimal number
                    if (decimal.TryParse(field.Value?.ToString(), out decimal itemValue))
                    {
                        totalAmount += itemValue;
                    }
                }
            }

            // Set the calculated total into the field named "Total"
            // The indexer returns a WidgetAnnotation, so cast it to Field.
            Field totalField = form["Total"] as Field;
            if (totalField == null)
            {
                Console.Error.WriteLine("Field 'Total' not found or is not a form field.");
                return;
            }
            totalField.Value = totalAmount.ToString("F2"); // format with two decimal places

            // Optionally trigger recalculation of any dependent calculated fields
            // (e.g., taxes, discounts) if they are defined in the form.
            totalField.Recalculate();

            // Save the updated PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Total amount calculated and saved to '{outputPath}'.");
    }
}
