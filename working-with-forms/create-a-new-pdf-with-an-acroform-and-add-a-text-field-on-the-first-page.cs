using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            // Here we want a field 200 points wide and 50 points high starting at (100,500)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField on the specified page using the rectangle
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleTextField",
                Value = "Enter text here"
            };

            // Add the field to the document's form
            doc.Form.Add(textField);

            // Save the PDF
            doc.Save("AcroFormWithTextField.pdf");
        }

        Console.WriteLine("PDF with AcroForm text field created successfully.");
    }
}