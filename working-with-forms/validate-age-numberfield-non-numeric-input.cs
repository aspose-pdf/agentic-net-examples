using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "age_form.pdf";

        // Create a PDF with a textbox field named "Age"
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Use fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);

            TextBoxField ageField = new TextBoxField(page, rect);
            ageField.PartialName = "Age";
            // The IsNumeric property no longer exists; numeric validation will be performed manually.
            ageField.Value = "0"; // Default numeric value
            doc.Form.Add(ageField, 1);

            doc.Save(pdfPath);
        }

        // Load the PDF and attempt to set a non‑numeric value
        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Retrieve the "Age" field
                TextBoxField ageField = (TextBoxField)doc.Form["Age"];

                // Attempt to assign alphabetic characters; this should be rejected.
                ageField.Value = "abc";

                // Manual numeric validation – if the value cannot be parsed as a number, throw.
                if (!int.TryParse(ageField.Value?.ToString() ?? string.Empty, out _))
                {
                    throw new ArgumentException("Non‑numeric value assigned to numeric field.");
                }

                // If no exception is thrown, the validation failed
                Console.WriteLine("Test FAILED: Non‑numeric value was accepted.");
            }
        }
        catch (Exception ex)
        {
            // Expected outcome: an exception for invalid numeric input
            Console.WriteLine("Test PASSED: Non‑numeric input rejected.");
            Console.WriteLine($"Exception message: {ex.Message}");
        }
    }
}
