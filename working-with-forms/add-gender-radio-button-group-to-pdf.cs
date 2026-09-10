using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "gender_form.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define rectangles for the two radio button options
            // Fully qualified to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle
            Aspose.Pdf.Rectangle maleRect   = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            Aspose.Pdf.Rectangle femaleRect = new Aspose.Pdf.Rectangle(100, 650, 120, 670);

            // Create a RadioButtonField associated with the *page* (required for option annotations)
            RadioButtonField genderField = new RadioButtonField(page)
            {
                // Set a unique name for the field (used as the form field identifier)
                Name = "Gender",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Select Gender",
                // Ensure only one option can be selected at a time
                NoToggleToOff = true
            };

            // Add the two options with their visual positions
            genderField.AddOption("Male",   maleRect);
            genderField.AddOption("Female", femaleRect);

            // Add the radio button field to the document's form collection
            doc.Form.Add(genderField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gender radio button group saved to '{outputPath}'.");
    }
}
