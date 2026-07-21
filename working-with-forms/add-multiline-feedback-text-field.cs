using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "FeedbackForm.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a blank page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            // (llx, lly) = lower‑left corner, (urx, ury) = upper‑right corner
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Create a multiline text box field named "Feedback"
            Aspose.Pdf.Forms.TextBoxField feedbackField = new Aspose.Pdf.Forms.TextBoxField(page, fieldRect);
            feedbackField.Name = "Feedback";          // field identifier
            feedbackField.Multiline = true;           // allow multiple lines
            feedbackField.MaxLen = 500;               // limit to 500 characters

            // Optional: set a visible border color (helps users see the field)
            feedbackField.Color = Aspose.Pdf.Color.Black;

            // Save the PDF with the new form field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 'Feedback' field saved to '{outputPath}'.");
    }
}