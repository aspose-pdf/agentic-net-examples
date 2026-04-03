using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output XML
        const string inputPdfPath = "TaxForm.pdf";
        const string outputXmlPath = "TaxFormData.xml";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document, calculate tax, update the form, and export to XML
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Retrieve the "Amount", "TaxRate" and "Tax" fields.
            // The Form indexer returns a WidgetAnnotation, so we cast it to Field.
            Field amountField   = pdfDocument.Form["Amount"] as Field;
            Field taxRateField  = pdfDocument.Form["TaxRate"] as Field;
            Field taxResultField = pdfDocument.Form["Tax"] as Field;

            if (amountField == null || taxRateField == null || taxResultField == null)
            {
                Console.Error.WriteLine("One or more required form fields (Amount, TaxRate, Tax) are missing.");
                return;
            }

            // Parse numeric values from the fields
            double amount  = ParseDouble(amountField.Value);
            double taxRate = ParseDouble(taxRateField.Value);

            // Simple tax calculation: Tax = Amount * TaxRate / 100
            double tax = amount * taxRate / 100.0;

            // Write the calculated tax back to the "Tax" field
            taxResultField.Value = tax.ToString("F2");

            // Export the entire PDF document (including updated form data) to XML
            XmlSaveOptions xmlOptions = new XmlSaveOptions(); // default options
            pdfDocument.Save(outputXmlPath, xmlOptions);
        }

        Console.WriteLine($"Tax calculation completed and exported to XML: {outputXmlPath}");
    }

    // Helper method to safely convert an object to double
    private static double ParseDouble(object value)
    {
        if (value == null) return 0.0;
        if (double.TryParse(value.ToString(), out double result))
            return result;
        return 0.0;
    }
}
