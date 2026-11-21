using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "field_test.pdf";

        // Create a 60‑character string to test the field limit.
        string longText = new string('A', 60);

        // Create a new PDF document and ensure deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field.
            Page page = doc.Pages.Add();

            // Define a textbox field with a maximum length of 50 characters.
            // Use the fully qualified Rectangle type to avoid ambiguity.
            TextBoxField txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 300, 730))
            {
                MaxLen = 50 // Correct property name for character limit
            };

            // Assign the 60‑character string to the field.
            txtField.Value = longText;

            // Add the field to the document's form collection.
            doc.Form.Add(txtField);

            // Save the PDF (optional, for visual verification).
            doc.Save(outputPath);

            // Validate that the stored value has been truncated to the max length.
            string storedValue = txtField.Value;
            Console.WriteLine($"Original length: {longText.Length}");
            Console.WriteLine($"Stored length:   {storedValue.Length}");
            Console.WriteLine($"Truncation correct: {storedValue.Length == txtField.MaxLen}");
        }
    }
}
