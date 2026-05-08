using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page (1‑based indexing)
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define a rectangle for the text box field
            // Fully qualified to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField on the page
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                // Set the maximum allowed length to 50 characters
                MaxLen = 50,

                // Optional: give the field a name for later retrieval
                Name = "TestField"
            };

            // Add the field to the document's form collection (or page.Form)
            // Widgets must not be added directly to page.Annotations.
            doc.Form.Add(txtField);
            // Alternatively you could use: page.Form.Add(txtField);

            // Prepare a 60‑character string
            string longInput = new string('A', 60);

            // Assign the long string to the field; Aspose.Pdf will truncate to MaxLen
            txtField.Value = longInput;

            // Retrieve the stored value and verify its length
            string storedValue = txtField.Value;
            Console.WriteLine($"Original length: {longInput.Length}");
            Console.WriteLine($"Stored length : {storedValue.Length}");
            Console.WriteLine($"Stored value  : {storedValue}");

            // Expected output: Stored length should be 50
            // (No need to save the document for this validation, but you may save if desired)
            // doc.Save("output.pdf");
        }
    }
}