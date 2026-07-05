using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a radio button field for gender and associate it with the page via the page‑only constructor
            RadioButtonField genderField = new RadioButtonField(page)
            {
                PartialName = "Gender",               // field name
                NoToggleToOff = true                  // exactly one option must be selected
            };

            // Set the overall field rectangle (the widget that contains the options)
            genderField.Rect = new Aspose.Pdf.Rectangle(100, 700, 200, 650);

            // Add "Male" option with its own rectangle
            genderField.AddOption(
                "Male",
                new Aspose.Pdf.Rectangle(110, 680, 120, 690));

            // Add "Female" option with its own rectangle
            genderField.AddOption(
                "Female",
                new Aspose.Pdf.Rectangle(110, 660, 120, 670));

            // Add the radio button field to the document's form
            doc.Form.Add(genderField);

            // Save the PDF with the AcroForm
            doc.Save("gender_form.pdf");
        }

        Console.WriteLine("PDF with gender radio button group created successfully.");
    }
}