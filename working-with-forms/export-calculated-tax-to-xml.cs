using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "filled.pdf";
        const string outputXml = "formData.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Access the form object
                Form form = doc.Form;

                // Retrieve the "Amount" field value
                if (form.HasField("Amount"))
                {
                    // The Form indexer returns a WidgetAnnotation; cast it to Field
                    Field amountField = form["Amount"] as Field;
                    double amount = 0;
                    if (amountField != null && double.TryParse(amountField.Value?.ToString(), out double parsed))
                    {
                        amount = parsed;
                    }

                    // Calculate tax (10% of amount) and round to 2 decimals
                    double tax = Math.Round(amount * 0.10, 2);

                    // Set the calculated tax into the "Tax" field
                    if (form.HasField("Tax"))
                    {
                        Field taxField = form["Tax"] as Field;
                        if (taxField != null)
                        {
                            taxField.Value = tax.ToString("F2");
                        }
                    }
                    else
                    {
                        // If the "Tax" field does not exist, create a hidden textbox field
                        TextBoxField taxField = new TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(0, 0, 0, 0))
                        {
                            PartialName = "Tax",
                            Value = tax.ToString("F2")
                        };
                        form.Add(taxField);
                    }
                }

                // Optional: save the PDF with the updated tax value
                doc.Save(outputPdf);

                // Export the form data to XML using XmlSaveOptions
                XmlSaveOptions xmlOpts = new XmlSaveOptions();
                doc.Save(outputXml, xmlOpts);
            }

            Console.WriteLine($"Form data exported to XML: {outputXml}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
