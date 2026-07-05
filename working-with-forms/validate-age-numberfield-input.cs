using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the NumberField (left, bottom, width, height)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 20);

            // Create the NumberField on the page
            NumberField ageField = new NumberField(page, fieldRect)
            {
                PartialName = "Age",               // Field name
                AllowedChars = "0123456789",       // Ensure only digits are allowed (default)
                MaxLen = 3                         // Optional: limit length
            };

            // Add the field to the document's form (page numbers are 1‑based)
            doc.Form.Add(ageField, 1);

            // -------- Test with valid numeric input --------
            try
            {
                ageField.Value = "27"; // Should succeed
                Console.WriteLine($"Numeric input accepted: {ageField.Value}");
            }
            catch (InvalidValueFormatException ex)
            {
                Console.WriteLine($"Unexpected error with numeric input: {ex.Message}");
            }

            // -------- Test with invalid alphabetic input --------
            try
            {
                ageField.Value = "ABC"; // Should throw InvalidValueFormatException
                Console.WriteLine("Error: Alphabetic input was incorrectly accepted.");
            }
            catch (InvalidValueFormatException)
            {
                Console.WriteLine("Alphabetic input correctly rejected by NumberField.");
            }

            // Save the PDF (optional, just to have a file)
            string outputPath = "AgeFieldTest.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}