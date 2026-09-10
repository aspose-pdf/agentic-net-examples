using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // ------------------------------------------------------------
        // Create a minimal PDF with a form field named "Discount" if it
        // does not already exist. This makes the example self‑contained
        // and prevents FileNotFoundException in the sandbox.
        // ------------------------------------------------------------
        if (!System.IO.File.Exists(inputPdf))
        {
            // Create a new PDF document with a single page.
            var doc = new Document();
            var page = doc.Pages.Add();

            // Define the rectangle (position and size) for the text box.
            // Coordinates are in points; (llx, lly, urx, ury).
            var rect = new Rectangle(100, 600, 200, 620);

            // Create a TextBoxField named "Discount".
            var discountField = new TextBoxField(page, rect)
            {
                PartialName = "Discount",
                Value = string.Empty
            };

            // Add the field to the document's form collection.
            doc.Form.Add(discountField);

            // Save the placeholder PDF.
            doc.Save(inputPdf);
        }

        // ------------------------------------------------------------
        // Open the PDF with FormEditor and apply validation script and
        // character limit to the "Discount" field.
        // ------------------------------------------------------------
        using (var formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // JavaScript that allows only numeric input with up to two
            // decimal places. If the value does not match, an alert is
            // shown and the entry is rejected.
            string validationScript =
                "if (event.value.match(/^\\d+(\\.\\d{0,2})?$/) == null) {" +
                "    app.alert('Please enter a numeric value with up to two decimal places.');" +
                "    event.rc = false;" +
                "}";

            formEditor.SetFieldScript("Discount", validationScript);
            // Optional: limit the total number of characters the user can type.
            formEditor.SetFieldLimit("Discount", 10);

            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"Discount\" configured and saved to '{outputPdf}'.");
    }
}
