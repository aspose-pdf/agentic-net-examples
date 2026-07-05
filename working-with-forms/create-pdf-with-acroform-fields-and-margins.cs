using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithMargins.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Set uniform page margins (in points) before adding any content
            doc.PageInfo.Margin = new MarginInfo
            {
                Top = 50,
                Bottom = 50,
                Left = 50,
                Right = 50
            };

            // Add a blank page; the margins defined above will be applied automatically
            Page page = doc.Pages.Add();

            // ---------- Add a TextBox field ----------
            // Rectangle coordinates: lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField txtField = new TextBoxField(doc, txtRect)
            {
                PartialName = "NameField",
                Value = "John Doe",
                Color = Color.LightGray // background color
            };
            // Set the border after the field instance has been created (Border needs the parent annotation)
            txtField.Border = new Border(txtField)
            {
                Width = 1
            };
            // Border colour is controlled by the field's own Color property
            txtField.Color = Color.Black;
            // Place the field on page 1
            doc.Form.Add(txtField, 1);

            // ---------- Add a Button field ----------
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 560, 200, 580);
            ButtonField btnField = new ButtonField(doc, btnRect)
            {
                PartialName = "SubmitBtn",
                Contents = "Submit"
            };
            doc.Form.Add(btnField, 1);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
