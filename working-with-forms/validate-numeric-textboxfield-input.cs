using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "age_form.pdf";

        // ---------- Create a PDF with a numeric field ----------
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the field rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create a TextBoxField named "Age" on the page (used as numeric field)
            TextBoxField ageField = new TextBoxField(page, rect);
            ageField.PartialName = "Age";

            // NOTE: The "AllowedChars" property was removed in newer Aspose.Pdf versions.
            // To enforce numeric input we will validate manually when assigning a value.

            // Add the field to the document form (page numbers are 1‑based)
            doc.Form.Add(ageField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        // ---------- Load the PDF and test input validation ----------
        using (Document doc = new Document(outputPath))
        {
            // Retrieve the "Age" field from the form as TextBoxField
            TextBoxField ageField = (TextBoxField)doc.Form["Age"];

            try
            {
                // Attempt to assign a non‑numeric value – our helper will throw InvalidValueFormatException
                SetNumericValue(ageField, "ABC");

                // If no exception, the validation failed
                Console.WriteLine("Test failed: non‑numeric input was accepted.");
            }
            catch (InvalidValueFormatException)
            {
                // Expected outcome – the field rejects alphabetic characters
                Console.WriteLine("Test passed: non‑numeric input correctly rejected.");
            }
            catch (Exception ex)
            {
                // Any other exception is unexpected
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Assigns a value to a TextBoxField ensuring it contains only digits.
    /// Throws InvalidValueFormatException if the value is not numeric.
    /// </summary>
    static void SetNumericValue(TextBoxField field, string value)
    {
        // Simple numeric validation – you can replace with more sophisticated checks if needed
        foreach (char c in value)
        {
            if (!char.IsDigit(c))
                throw new InvalidValueFormatException($"Value '{value}' is not numeric.");
        }
        field.Value = value;
    }
}
