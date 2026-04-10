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
            // Add a blank page (default size)
            Page page = doc.Pages.Add();

            // Create a radio button field for gender selection.
            // Use the constructor that receives only the Page object.
            RadioButtonField genderField = new RadioButtonField(page)
            {
                Name = "Gender",
                PartialName = "Gender",
                // Allow no option to be selected initially
                NoToggleToOff = false
            };

            // Add "Male" option
            genderField.AddOption(
                "Male",
                new Rectangle(100f, 700f, 120f, 720f));

            // Add "Female" option at a specific position
            genderField.AddOption(
                "Female",
                new Rectangle(100f, 660f, 120f, 680f));

            // Add the radio button field to the document's form
            doc.Form.Add(genderField);

            // Save the PDF with the AcroForm
            doc.Save("GenderForm.pdf");
        }

        Console.WriteLine("PDF with gender radio button group created successfully.");
    }
}
