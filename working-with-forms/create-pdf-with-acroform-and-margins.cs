using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Rectangle

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_margins.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // ----- Set page margins before adding form fields -----
            // Create a MarginInfo instance and define margins (points)
            MarginInfo margins = new MarginInfo
            {
                Left   = 50,   // left margin
                Right  = 50,   // right margin
                Top    = 50,   // top margin
                Bottom = 50    // bottom margin
            };
            // Apply the margins to the page
            page.PageInfo.Margin = margins;

            // ----- Create an AcroForm text box field -----
            // Define the rectangle where the field will be placed
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            // Instantiate the field (document‑based constructor)
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "NameField",          // field name
                Value       = "Enter name here"     // default value
            };

            // Set default appearance (font, size, color) using the proper constructor
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // ----- Save the PDF -----
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}