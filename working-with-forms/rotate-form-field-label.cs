using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class RotateFormFieldLabel
{
    static void Main()
    {
        const string outputPath = "rotated_label_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the form field label
            // (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Rotate the rectangle by 45 degrees
            fieldRect.Rotate(45);

            // Create a text box form field using the rotated rectangle
            TextBoxField textBox = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",
                Value = "Label rotated 45°"
            };

            // Add the field to the document's form collection
            doc.Form.Add(textBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rotated form field label saved to '{outputPath}'.");
    }
}