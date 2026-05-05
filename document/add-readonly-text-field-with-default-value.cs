using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_textfield.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will be placed (left, bottom, width, height)
            // Rectangle constructor expects float values, so use the 'f' suffix
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100f, 500f, 300f, 530f);

            // Create a TextBoxField on the page with the specified rectangle
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                // Set the default value that appears in the field
                Value = "Default Text",
                // Make the field read‑only so the user cannot modify it
                ReadOnly = true
            };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with read‑only text field saved to '{outputPath}'.");
    }
}
