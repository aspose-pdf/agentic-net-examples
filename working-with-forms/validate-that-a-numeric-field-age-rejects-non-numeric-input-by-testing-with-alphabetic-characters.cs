using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for InvalidValueFormatException

class Program
{
    static void Main()
    {
        const string outputPath = "age_test.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a page (required before placing a field)
            Page page = doc.Pages.Add();

            // Define the rectangle where the NumberField will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a NumberField named "Age" on the page
            NumberField ageField = new NumberField(page, fieldRect);
            ageField.PartialName = "Age";

            // The default AllowedChars are "0123456789", which restricts input to digits.
            // Explicitly set it for clarity (optional)
            ageField.AllowedChars = "0123456789";

            // Add the field to the document's form collection
            doc.Form.Add(ageField);

            // Attempt to assign a non‑numeric value to the field.
            // This should raise InvalidValueFormatException because the value contains alphabetic characters.
            try
            {
                ageField.Value = "abc"; // invalid input
                Console.WriteLine("Unexpected: non‑numeric value was accepted.");
            }
            catch (InvalidValueFormatException ex)
            {
                Console.WriteLine("Validation succeeded: non‑numeric input was rejected.");
                Console.WriteLine($"Exception message: {ex.Message}");
            }

            // Save the PDF (the field will appear empty because the invalid value was not set)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}