using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for DefaultAppearance

class AddAcroFormFields
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page newPage = doc.Pages.Add();

            // Define rectangles for the fields (coordinates are in points; origin is bottom‑left)
            Aspose.Pdf.Rectangle textBoxRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            Aspose.Pdf.Rectangle checkBoxRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a TextBox field on the new page
            TextBoxField txtField = new TextBoxField(newPage, textBoxRect)
            {
                PartialName = "CustomerName",
                Value = "Enter name here"
            };
            // Set appearance via DefaultAppearance constructor (uses System.Drawing.Color)
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create a CheckBox field on the new page
            CheckboxField chkField = new CheckboxField(newPage, checkBoxRect)
            {
                PartialName = "SubscribeNewsletter",
                Value = "Off",
                Color = Aspose.Pdf.Color.Blue
            };

            // Add the fields to the document's form (page numbers are 1‑based)
            doc.Form.Add(txtField, newPage.Number);
            doc.Form.Add(chkField, newPage.Number);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new page and form fields: {outputPath}");
    }
}
