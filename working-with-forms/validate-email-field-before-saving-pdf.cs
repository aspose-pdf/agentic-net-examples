using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "email_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the email field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field for the email address
            TextBoxField emailField = new TextBoxField(page, rect)
            {
                Name = "email",
                Value = "user@example.com", // Replace with actual input as needed
                Color = Aspose.Pdf.Color.LightGray
            };

            // Add the field to the document's form collection
            doc.Form.Add(emailField);

            // Validate that the email contains an '@' character before saving
            if (!string.IsNullOrEmpty(emailField.Value) && emailField.Value.Contains("@"))
            {
                // Save the PDF (no SaveOptions needed for PDF output)
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("Invalid email address: missing '@'. PDF not saved.");
            }
        }
    }
}