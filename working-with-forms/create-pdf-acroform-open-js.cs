using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithOpenJS.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a text box field on the page
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "SampleTextBox",
                Value = "Enter text here"
            };
            // Add the field to the document's form
            doc.Form.Add(txtField);

            // Set a document‑level JavaScript that runs when the PDF is opened
            // Document.OpenAction expects a PdfAction; JavascriptAction derives from it
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with AcroForm field and open‑action JavaScript: {outputPath}");
    }
}