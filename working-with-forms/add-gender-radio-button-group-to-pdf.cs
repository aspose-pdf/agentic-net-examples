using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "gender_form.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page where the form fields will be placed.
            Page page = doc.Pages.Add();

            // Create a radio button field named "Gender" and associate it with the page.
            RadioButtonField genderField = new RadioButtonField(page);
            genderField.PartialName = "Gender";

            // Define the options for the radio button group with their widget rectangles.
            genderField.AddOption("Male", new Rectangle(100, 700, 115, 715));
            genderField.AddOption("Female", new Rectangle(100, 670, 115, 685));

            // Set the default selected value (1‑based index, 1 = first option).
            genderField.Selected = 1;

            // Add labels next to the radio button widgets.
            TextFragment maleLabel = new TextFragment("Male") { TextState = { FontSize = 12 } };
            maleLabel.Position = new Position(120, 700);
            page.Paragraphs.Add(maleLabel);

            TextFragment femaleLabel = new TextFragment("Female") { TextState = { FontSize = 12 } };
            femaleLabel.Position = new Position(120, 670);
            page.Paragraphs.Add(femaleLabel);

            // Add the radio button field to the document's AcroForm.
            doc.Form.Add(genderField);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gender radio button group saved to '{outputPath}'.");
    }
}
