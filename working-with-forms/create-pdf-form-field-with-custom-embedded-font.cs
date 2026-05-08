using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the external TrueType font file
        const string fontPath = "custom.ttf";

        // Output PDF file
        const string outputPdf = "output.pdf";

        // Ensure the font file exists
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom font from the file stream and mark it for embedding
        Font customFont;
        using (FileStream fontStream = File.OpenRead(fontPath))
        {
            customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);
            customFont.IsEmbedded = true;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the form field
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Correct TextBoxField construction – (Page, Rectangle)
            TextBoxField textField = new TextBoxField(page, fieldRect);
            // Set the field's name via PartialName
            textField.PartialName = "MyCustomField";

            // Apply the custom font to the field's default appearance
            textField.DefaultAppearance = new DefaultAppearance(customFont, 12, System.Drawing.Color.Blue);

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Optional: generate the visual appearance on the first page
            doc.Form.AddFieldAppearance(textField, 1, fieldRect);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with custom font field saved to '{outputPdf}'.");
    }
}
