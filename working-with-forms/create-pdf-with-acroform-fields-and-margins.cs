using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class CreatePdfWithFormAndMargins
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "FormWithMargins.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Set default page size and margins for all pages added to the document
            doc.PageInfo = new PageInfo();
            MarginInfo margins = new MarginInfo();
            margins.Top = 72;    // 1 inch from top
            margins.Bottom = 72; // 1 inch from bottom
            margins.Left = 72;   // 1 inch from left
            margins.Right = 72;  // 1 inch from right
            doc.PageInfo.Margin = margins;

            // Add a single page (the margins defined above will be applied)
            doc.Pages.Add();

            // ----- Text Box Field -----
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);
            TextBoxField txtField = new TextBoxField(doc, txtRect);
            txtField.PartialName = "FullName";   // field name
            txtField.Value = "John Doe";         // default value
            txtField.Color = Color.LightGray;     // background color of the field
            // Note: The Border class is not required for a basic border; default border is applied.
            // If a custom border is needed, use the appropriate Border class from Aspose.Pdf.Forms in the version you target.
            doc.Form.Add(txtField, 1);

            // ----- Button Field -----
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 600, 200, 630);
            ButtonField btnField = new ButtonField(doc, btnRect);
            btnField.PartialName = "SubmitBtn";
            btnField.Contents = "Submit"; // button caption
            btnField.Color = Color.LightGray; // background color
            // Default border is sufficient; custom border code removed to avoid missing type errors.
            doc.Form.Add(btnField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with form fields and margins saved to '{outputPath}'.");
    }
}
