using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice_form_v3.pdf";
        const string outputPath = "required_payment.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so we use a using block
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Define the items for the list box
            string[] paymentOptions = new[] { "Credit Card", "Bank Transfer", "PayPal" };

            // Set the items that will be used for the next ComboBox/ListBox field
            editor.Items = paymentOptions;

            // Add a ListBox field named "PaymentMethod" on page 2
            // Coordinates are in points (1/72 inch): lower‑left (100,500), upper‑right (300,520)
            editor.AddField(FieldType.ListBox, "PaymentMethod", 2, 100, 500, 300, 520);

            // Mark the field as required.  The SetFieldAttribute method sets a property flag for a field.
            // PropertyFlag.Required corresponds to the PDF "Required" flag.
            editor.SetFieldAttribute("PaymentMethod", PropertyFlag.Required);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
