using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "DatePickerForm.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Add a label for the date picker field
            TextFragment label = new TextFragment("Select Date:");
            label.Position = new Position(100, 620); // X=100, Y=620
            page.Paragraphs.Add(label);

            // Define the rectangle where the date picker will appear
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 560, 300, 600); // llx, lly, urx, ury

            // Create a TextBoxField to act as a date input field (Aspose.Pdf does not expose a dedicated DateTimeField in this version)
            TextBoxField dateField = new TextBoxField(page, fieldRect);
            dateField.PartialName = "datePicker";                     // Field name
            dateField.Value = DateTime.Now.ToString("yyyy-MM-dd");    // Default value displayed to the user
            // Optional: set appearance (font, size, color) via DefaultAppearance if desired
            // dateField.DefaultAppearance = new DefaultAppearance(FontRepository.FindFont("Helvetica"), 12, System.Drawing.Color.Black);

            // Add the field to the document's form on page 1
            doc.Form.Add(dateField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker field saved to '{outputPath}'.");
    }
}
