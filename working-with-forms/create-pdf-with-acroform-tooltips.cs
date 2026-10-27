using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Define output PDF path
        const string outputPath = "AcroFormWithTooltips.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define a rectangle for the first text box field
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
            // Create a text box field, set its name, tooltip (via Contents) and initial value
            TextBoxField nameField = new TextBoxField(page, nameRect)
            {
                PartialName = "FullName",
                Value       = ""
            };
            // Set tooltip using the Contents property (the tooltip text for widget annotations)
            nameField.Contents = "Enter your full name";
            // Add the field to the document's form collection
            doc.Form.Add(nameField);

            // Define a rectangle for a second text box field (e.g., email)
            Aspose.Pdf.Rectangle emailRect = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            TextBoxField emailField = new TextBoxField(page, emailRect)
            {
                PartialName = "EmailAddress",
                Value       = ""
            };
            emailField.Contents = "Enter your email address";
            doc.Form.Add(emailField);

            // Define a rectangle for a checkbox field
            Aspose.Pdf.Rectangle agreeRect = new Aspose.Pdf.Rectangle(100, 600, 115, 615);
            CheckboxField agreeField = new CheckboxField(page, agreeRect)
            {
                PartialName = "AgreeTerms",
                Checked     = false
            };
            agreeField.Contents = "Check to agree to the terms and conditions";
            doc.Form.Add(agreeField);

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields and tooltips saved to '{outputPath}'.");
    }
}
