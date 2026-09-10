using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTextBox.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a text box field on the first page
            TextBoxField textField = new TextBoxField(page, rect)
            {
                // Optional: give the field a name and a default value
                PartialName = "SampleTextBox",
                Value = "Enter text here"
            };

            // Add the field to the document's AcroForm
            doc.Form.Add(textField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}