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

            // Define the rectangle for the NumberField (Age)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create the NumberField with default numeric allowed characters
            NumberField ageField = new NumberField(page, rect)
            {
                PartialName = "Age",
                AllowedChars = "0123456789" // explicit, though this is the default
            };

            // Add the field to the form on page 1 (1‑based indexing)
            doc.Form.Add(ageField, 1);

            // Test with valid numeric input – should succeed
            try
            {
                ageField.Value = "30";
                Console.WriteLine($"Numeric input accepted: {ageField.Value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error with numeric input: {ex.Message}");
            }

            // Test with alphabetic input – should be rejected
            try
            {
                ageField.Value = "ABC";
                Console.WriteLine("Alphabetic input was incorrectly accepted.");
            }
            catch (InvalidValueFormatException)
            {
                Console.WriteLine("Alphabetic input correctly rejected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error type: {ex.GetType().Name}");
            }

            // Save the PDF (optional, completes the create‑save lifecycle)
            doc.Save("AgeFieldTest.pdf");
        }
    }
}