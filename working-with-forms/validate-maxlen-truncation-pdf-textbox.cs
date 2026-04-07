using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPdf = "field_test.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define rectangle for the textbox field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 400, 750);

            // Create a TextBoxField on the page
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "TestField",
                MaxLen = 50 // Limit the field to 50 characters
            };

            // Add the field to the document's Form collection (Page does not expose Form)
            doc.Form.Add(txtField);

            // 60‑character test string
            string longText = new string('A', 60);

            // Simulate user entry (Aspose will truncate to MaxLen automatically)
            txtField.Value = longText;

            // Save the document (required for proper field appearance)
            doc.Save(outputPdf);
        }

        // Re‑open the document to read the stored value
        using (Document doc = new Document(outputPdf))
        {
            // Locate the field by name via the Form collection
            TextBoxField txtField = (TextBoxField)doc.Form["TestField"];

            // Retrieve the value that was saved
            string storedValue = txtField.Value?.ToString() ?? string.Empty;

            // Output the length – should be 50 if truncation occurred
            Console.WriteLine($"Stored value length: {storedValue.Length}");
            Console.WriteLine($"Stored value: {storedValue}");
        }
    }
}
