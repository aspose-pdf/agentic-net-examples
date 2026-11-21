using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "age_form.pdf";

        // Create a PDF containing a TextBox field named "Age"
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define the field rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 200, 720);

            // Create the TextBox field
            TextBoxField ageField = new TextBoxField(page, rect);
            ageField.PartialName = "Age";   // field name
            ageField.MaxLen = 3;              // character limit (correct property name)

            // Add the field to the document's form collection
            doc.Form.Add(ageField, 1);

            // Save the PDF
            doc.Save(pdfPath);
        }

        // Open the PDF and test the field with alphabetic input
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the "Age" field
            TextBoxField ageField = (TextBoxField)doc.Form["Age"];

            // Attempt to set a non‑numeric value
            ageField.Value = "ABC";

            // Validate by trying to parse the value as an integer
            bool isAlphabeticAccepted = int.TryParse(ageField.Value, out _);
            Console.WriteLine($"Alphabetic input accepted? {isAlphabeticAccepted}");

            // For comparison, set a numeric value
            ageField.Value = "27";
            bool isNumericAccepted = int.TryParse(ageField.Value, out _);
            Console.WriteLine($"Numeric input accepted? {isNumericAccepted}");
        }
    }
}
