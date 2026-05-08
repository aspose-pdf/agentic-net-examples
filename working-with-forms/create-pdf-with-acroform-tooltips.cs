using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "AcroFormWithTooltips.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Text box field (e.g., for entering a name)
            // -------------------------------------------------
            // Define the field rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            // Create the text box field on the page
            TextBoxField nameField = new TextBoxField(page, nameRect);
            // Set the internal field name (used in form data)
            nameField.PartialName = "FullName";
            // Set the tooltip that appears in PDF viewers
            nameField.AlternateName = "Enter your full name";
            // Add the field to the document's form collection
            doc.Form.Add(nameField);

            // -------------------------------------------------
            // Checkbox field (e.g., for agreeing to terms)
            // -------------------------------------------------
            Aspose.Pdf.Rectangle agreeRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);
            CheckboxField agreeField = new CheckboxField(page, agreeRect);
            agreeField.PartialName = "AgreeToTerms";
            agreeField.AlternateName = "Check to agree to the terms and conditions";
            // Define the export value when the box is checked
            agreeField.ExportValue = "Yes";
            // Add the checkbox field to the form collection
            doc.Form.Add(agreeField);

            // -------------------------------------------------
            // Additional fields can be added here following the same pattern
            // -------------------------------------------------

            // Save the PDF with the form fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
