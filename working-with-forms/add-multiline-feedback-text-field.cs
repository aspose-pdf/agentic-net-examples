using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "feedback_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for placing the form field)
            Page page = doc.Pages.Add();

            // Define the rectangle where the multiline text field will appear
            // (llx, lly, urx, ury) – coordinates are in points (1/72 inch)
            // Note: lower‑left Y must be less than upper‑right Y
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Create the multiline text box field
            TextBoxField feedbackField = new TextBoxField(page, fieldRect)
            {
                PartialName = "Feedback",   // Field name
                MaxLen = 500,                // Character limit
                Multiline = true,            // Enable multiline input
                Value = string.Empty,        // Initial empty value
                // The field's own Color property defines the border colour
                Color = Aspose.Pdf.Color.Black
            };

            // Border belongs to the Annotation base class and must be created
            // with the field instance as its parent.
            feedbackField.Border = new Border(feedbackField) { Width = 1 };

            // Set background colour via the field's Characteristics (System.Drawing.Color)
            feedbackField.Characteristics.Background = System.Drawing.Color.LightGray;

            // Add the field to the document's form collection
            doc.Form.Add(feedbackField);

            // Save the PDF with the new form field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiline 'Feedback' field saved to '{outputPath}'.");
    }
}
