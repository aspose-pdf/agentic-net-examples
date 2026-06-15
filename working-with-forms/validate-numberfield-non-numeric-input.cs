using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the NumberField will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create the NumberField named "Age"
            NumberField ageField = new NumberField(page, rect)
            {
                PartialName = "Age",
                // Restrict allowed characters to digits only (default is already digits)
                AllowedChars = "0123456789"
            };

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based indexing)
            doc.Form.Add(ageField, 1);

            // Attempt to set a non‑numeric value and verify that it is rejected
            try
            {
                ageField.Value = "ABC"; // Should throw InvalidValueFormatException
                Console.WriteLine("Test FAILED: non‑numeric value was accepted.");
            }
            catch (InvalidValueFormatException ex)
            {
                Console.WriteLine("Test PASSED: non‑numeric input rejected.");
                Console.WriteLine($"Exception message: {ex.Message}");
            }

            // Save the document (optional, for visual verification)
            doc.Save("AgeFieldTest.pdf");
        }
    }
}