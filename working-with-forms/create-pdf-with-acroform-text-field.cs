using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add the first page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the page
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                // Set a name for the field (used in form data)
                PartialName = "SampleTextField",
                // Optional: set an initial value
                Value = "Enter text here"
            };

            // Add the field to the document's AcroForm
            doc.Form.Add(txtField);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}