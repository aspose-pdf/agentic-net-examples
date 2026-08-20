using System;
using System.Drawing; // kept for potential other uses, but not for Color ambiguity
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "text_field_readonly.pdf";

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to host the field
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField on the *page* with the specified rectangle
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                // Set a name for the field (PartialName is the correct property)
                PartialName = "SampleField",

                // Set the default value that appears when the PDF is opened
                Value = "Default Text",

                // Make the field read‑only
                ReadOnly = true,

                // Set the default appearance (font, size, color)
                // Use System.Drawing.Color for DefaultAppearance as required by the API
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with read‑only text field saved to '{outputPath}'.");
    }
}
