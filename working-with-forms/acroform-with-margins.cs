using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormRelative.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define page margins (points) – MarginInfo expects float values
            float marginLeft   = 50f;
            float marginTop    = 50f;
            float marginRight  = 50f;
            float marginBottom = 50f;
            page.PageInfo.Margin = new MarginInfo(marginLeft, marginTop, marginRight, marginBottom);

            // Retrieve page dimensions
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Compute the usable area inside the margins
            double usableWidth  = pageWidth  - marginLeft - marginRight;
            double usableHeight = pageHeight - marginTop  - marginBottom;

            // Position a text box field relative to the margins:
            //   - 5% from the left margin
            //   - 10% down from the top margin
            //   - width = 50% of usable width, height = 20 points
            double fieldWidth  = usableWidth * 0.5;
            double fieldHeight = 20;
            double fieldX = marginLeft + usableWidth * 0.05;
            double fieldY = pageHeight - marginTop - usableHeight * 0.10 - fieldHeight;

            // Define the rectangle for the text box (lower‑left x,y, upper‑right x,y)
            Aspose.Pdf.Rectangle textBoxRect = new Aspose.Pdf.Rectangle(
                fieldX,
                fieldY,
                fieldX + fieldWidth,
                fieldY + fieldHeight);

            // Create the text box AcroForm field
            TextBoxField nameField = new TextBoxField(page, textBoxRect);
            nameField.PartialName = "Name";
            nameField.Value = "Enter name";

            // Add the field to the document's form (partial name already set on the field)
            doc.Form.Add(nameField);

            // Position a checkbox field relative to the text box:
            //   - placed to the right of the text box with a 5% usable width gap
            //   - same vertical position as the text box
            double checkBoxSize = fieldHeight;
            double checkBoxX = fieldX + fieldWidth + usableWidth * 0.05;
            double checkBoxY = fieldY;

            Aspose.Pdf.Rectangle checkBoxRect = new Aspose.Pdf.Rectangle(
                checkBoxX,
                checkBoxY,
                checkBoxX + checkBoxSize,
                checkBoxY + checkBoxSize);

            CheckboxField agreeField = new CheckboxField(page, checkBoxRect);
            agreeField.PartialName = "Agree";
            agreeField.Checked = false;

            doc.Form.Add(agreeField);

            // Save the PDF (no PreSave required for AcroForm)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
