using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Add a label prompting the user to select gender
            TextFragment label = new TextFragment("Select Gender:");
            label.Position = new Position(100, 700); // Position near top of page
            page.Paragraphs.Add(label);

            // Create a radio button field (AcroForm) for gender selection
            // The constructor takes only a Page instance; the field name is set via PartialName
            RadioButtonField genderField = new RadioButtonField(page);
            genderField.PartialName = "Gender";

            // Define the "Male" option with its widget rectangle
            genderField.AddOption(
                "Male",
                new Rectangle(100, 650, 120, 670) // llx, lly, urx, ury
            );

            // Define the "Female" option with its widget rectangle
            genderField.AddOption(
                "Female",
                new Rectangle(200, 650, 220, 670)
            );

            // Set the default selected value (optional)
            genderField.Value = "Male";

            // Add the radio button field to the document's AcroForm on the first page
            doc.Form.Add(genderField, page.Number);

            // Save the PDF with the AcroForm to disk
            doc.Save("gender_form.pdf");
        }

        Console.WriteLine("PDF with gender radio button group created: gender_form.pdf");
    }
}