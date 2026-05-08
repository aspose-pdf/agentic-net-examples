using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithOpenJs.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based).
            Page page = doc.Pages.Add();

            // Define the rectangle for the text field.
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field. Use the constructor that takes the target Page and rectangle.
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                // Set the field name (used to reference the field programmatically).
                PartialName = "SampleTextField",
                // Optional: set a default value.
                Value = "Enter text here"
            };

            // Add the field to the document's form.
            doc.Form.Add(txtField);

            // Set a document‑level JavaScript action that runs when the PDF is opened.
            doc.OpenAction = new JavascriptAction("app.alert('Welcome to the PDF with AcroForm!');");

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created and saved to '{outputPath}'.");
    }
}
