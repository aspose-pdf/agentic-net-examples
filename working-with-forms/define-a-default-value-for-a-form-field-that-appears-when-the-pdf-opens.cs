using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;   // required for FontRepository if needed

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_default.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the page
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField"
            };

            // Set the default value that appears when the PDF is opened
            textField.Value = "Enter your name";

            // Optional: define default appearance (font, size, color)
            // Use the constructor that accepts font name, size and System.Drawing.Color
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with default field value saved to '{outputPath}'.");
    }
}