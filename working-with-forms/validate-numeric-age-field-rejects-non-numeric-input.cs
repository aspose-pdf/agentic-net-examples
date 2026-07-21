using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "age_form.pdf";

        // ---------- Create PDF with a numeric field ----------
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the field rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create a NumberField named "Age"
            NumberField ageField = new NumberField(page, rect);
            ageField.PartialName = "Age";

            // Restrict allowed characters to digits only (default is already digits)
            ageField.AllowedChars = "0123456789";

            // Add the field to the form on page 1
            doc.Form.Add(ageField, 1);

            // Save the PDF (optional, for visual inspection)
            doc.Save(outputPath);
        }

        // ---------- Test that non‑numeric input is rejected ----------
        try
        {
            using (Document doc = new Document(outputPath))
            {
                // Retrieve the field by its partial name
                NumberField ageField = (NumberField)doc.Form["Age"];

                // Attempt to assign alphabetic characters – should fail
                ageField.Value = "ABC";
            }

            // If no exception is thrown, the test failed
            Console.WriteLine("Test failed: non‑numeric value was accepted.");
        }
        catch (InvalidValueFormatException ex)
        {
            // Expected outcome – the field rejected the input
            Console.WriteLine("Test passed: non‑numeric input rejected.");
            Console.WriteLine($"Exception message: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other exception indicates an unexpected error
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}