using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField on the specific page using the rectangle
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                // Optional: set a name and default value
                PartialName = "SampleTextField",
                Value = "Enter text here"
            };

            // Add the field to the document's AcroForm
            doc.Form.Add(textField);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("AcroFormWithTextField.pdf");
        }

        Console.WriteLine("PDF with AcroForm text field created successfully.");
    }
}