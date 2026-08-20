using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "age_form.pdf";

        // Create a new PDF with a single page and add a NumberField named "Age"
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Rectangle for the field (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 730);

            // Initialize NumberField; AllowedChars defaults to digits, set explicitly for clarity
            NumberField ageField = new NumberField(page, rect)
            {
                PartialName = "Age",
                AllowedChars = "0123456789"
            };

            // Add the field to the form on page 1
            doc.Form.Add(ageField, 1);

            // Save the PDF (optional, for visual verification)
            doc.Save(outputPath);
        }

        // Test that assigning alphabetic characters throws an exception
        try
        {
            using (Document doc = new Document(outputPath))
            {
                // Retrieve the field by its partial name
                NumberField ageField = (NumberField)doc.Form["Age"];

                // Attempt to set a non‑numeric value
                ageField.Value = "ABC";

                // If no exception occurs, the validation failed
                Console.WriteLine("Test FAILED: Non‑numeric value was accepted.");
            }
        }
        catch (InvalidValueFormatException ex)
        {
            // Expected outcome: the field rejects non‑numeric input
            Console.WriteLine("Test PASSED: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Any other exception indicates an unexpected error
            Console.WriteLine($"Test ERROR: {ex.GetType().Name} - {ex.Message}");
        }
    }
}