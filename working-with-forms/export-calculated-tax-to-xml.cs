using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for input PDF, output XML and (optional) temporary PDF after processing
        const string inputPdfPath  = "input_form.pdf";
        const string outputXmlPath = "tax_data.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document containing the form
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Example: calculate tax based on a form field value (e.g., "Amount")
            // ------------------------------------------------------------
            // Retrieve the "Amount" field value (as string)
            string amountStr = string.Empty;
            foreach (Aspose.Pdf.Forms.Field field in pdfDoc.Form.Fields)
            {
                if (field.PartialName.Equals("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    // The field value is stored in the field's "Value" property (as string)
                    amountStr = field.Value?.ToString() ?? "0";
                    break;
                }
            }

            // Parse the amount and calculate tax (simple 10% example)
            if (!double.TryParse(amountStr, out double amount))
                amount = 0.0;

            double tax = Math.Round(amount * 0.10, 2); // 10% tax

            // Update (or create) a hidden field named "Tax" with the calculated value
            bool taxFieldFound = false;
            foreach (Aspose.Pdf.Forms.Field field in pdfDoc.Form.Fields)
            {
                if (field.PartialName.Equals("Tax", StringComparison.OrdinalIgnoreCase))
                {
                    field.Value = tax.ToString("F2");
                    taxFieldFound = true;
                    break;
                }
            }

            if (!taxFieldFound)
            {
                // If the "Tax" field does not exist, create a new text field and set its value
                Aspose.Pdf.Forms.TextBoxField taxField = new Aspose.Pdf.Forms.TextBoxField(pdfDoc.Pages[1], new Aspose.Pdf.Rectangle(0, 0, 0, 0));
                taxField.PartialName = "Tax";
                taxField.Value = tax.ToString("F2");
                pdfDoc.Form.Add(taxField);
            }

            // ------------------------------------------------------------
            // Export the entire PDF document (including form data) to XML
            // ------------------------------------------------------------
            Aspose.Pdf.XmlSaveOptions xmlOptions = new Aspose.Pdf.XmlSaveOptions();
            pdfDoc.Save(outputXmlPath, xmlOptions);

            Console.WriteLine($"Tax value ({tax:F2}) exported to XML at '{outputXmlPath}'.");
        }
    }
}