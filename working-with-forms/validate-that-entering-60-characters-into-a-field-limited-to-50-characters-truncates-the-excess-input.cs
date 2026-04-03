using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Border class resides here

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a TextBoxField on the page
            TextBoxField txtField = new TextBoxField(page, rect);
            // Set the maximum allowed length to 50 characters
            txtField.MaxLen = 50;
            // Optional visual settings – Border must be assigned after the field is instantiated
            txtField.Border = new Border(txtField) { Width = 1 };
            txtField.Color = Aspose.Pdf.Color.LightGray;

            // Add the field to the document's form collection (not directly to page annotations)
            doc.Form.Add(txtField);

            // Input a string of 60 characters
            string longInput = new string('A', 60);
            txtField.Value = longInput;

            // Retrieve the stored value; Aspose.Pdf truncates it to MaxLen
            string storedValue = txtField.Value?.ToString() ?? string.Empty;

            // Validate that the length is 50 characters (excess truncated)
            Console.WriteLine($"Input length : {longInput.Length}");
            Console.WriteLine($"Stored length: {storedValue.Length}");
            Console.WriteLine($"Truncation successful: {storedValue.Length == 50}");

            // Save the document (optional, demonstrates proper save usage)
            doc.Save("TruncatedField.pdf");
        }
    }
}
