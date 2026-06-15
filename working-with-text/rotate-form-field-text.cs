using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a text box form field
        using (Document doc = new Document())
        {
            // Add a page to the document
            doc.Pages.Add();

            // Define the rectangle where the field will be placed
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box field on the first page
            TextBoxField textBox = new TextBoxField(doc.Pages[1], fieldRect);
            textBox.PartialName = "MyField";
            textBox.Value = "Rotated Text";

            // Add the field to the form
            doc.Form.Add(textBox, 1);

            // Save the initial PDF
            doc.Save("input.pdf");
        }

        // Reopen the PDF and rotate the field's appearance
        using (Document doc = new Document("input.pdf"))
        {
            // Retrieve the previously created field
            TextBoxField textBox = (TextBoxField)doc.Form["MyField"];

            // Get the field rectangle and rotate it by 90 degrees
            Aspose.Pdf.Rectangle rotatedRect = textBox.Rect;
            rotatedRect.Rotate(90);

            // Add a new appearance for the field using the rotated rectangle
            doc.Form.AddFieldAppearance(textBox, 1, rotatedRect);

            // Save the modified PDF
            doc.Save("output.pdf");
        }
    }
}