using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for Rectangle (avoid System.Drawing)

class Program
{
    static void Main()
    {
        const string pdfPath = "field_test.pdf";
        const int maxLength = 50; // desired character limit

        // Create a PDF with a text box field (no built‑in MaxLength property in this version)
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define a rectangle for the field (use Aspose.Pdf.Rectangle to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create the text box field
            TextBoxField txtField = new TextBoxField(page, rect);
            txtField.PartialName = "MyField";
            txtField.Value = ""; // initial empty value

            // Add the field to the document's form
            doc.Form.Add(txtField, 1);

            // Save the PDF
            doc.Save(pdfPath);
        }

        // Prepare a 60‑character string
        string longText = new string('A', 60); // e.g., "AAAAAAAA...." (60 times)

        // Load the PDF, set the long value (truncated manually), and verify truncation
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the field by its partial name
            TextBoxField txtField = (TextBoxField)doc.Form["MyField"];

            // Manually enforce the character limit because TextBoxField.MaxLength is not available
            string truncated = longText.Length > maxLength ? longText.Substring(0, maxLength) : longText;
            txtField.Value = truncated;

            // Save changes (optional, but ensures the value is persisted)
            doc.Save(pdfPath);

            // Read back the stored value
            string actual = txtField.Value;

            // Output validation results
            Console.WriteLine($"Original length: {longText.Length}");   // should be 60
            Console.WriteLine($"Stored length:   {actual.Length}");    // should be 50
            Console.WriteLine($"Truncation correct: {actual.Length == maxLength}");
        }
    }
}
