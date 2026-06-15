using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            // Parameters: lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a TextBoxField on the page using the rectangle
            TextBoxField textBox = new TextBoxField(page, rect)
            {
                // Set a unique name for the field and an initial value
                PartialName = "SampleTextBox",
                Value = "Enter text here"
            };

            // Add the field to the document's AcroForm
            doc.Form.Add(textBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}