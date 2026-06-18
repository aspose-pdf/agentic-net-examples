using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a radio button field (group) on the page
            RadioButtonField genderField = new Aspose.Pdf.Forms.RadioButtonField(page);
            genderField.PartialName = "Gender";          // logical name of the group
            genderField.Name = "GenderGroup";            // optional display name

            // Define rectangles for each radio button option (coordinates in points)
            // Adjust positions as needed for your layout
            Aspose.Pdf.Rectangle rectMale   = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            Aspose.Pdf.Rectangle rectFemale = new Aspose.Pdf.Rectangle(100, 660, 120, 680);
            Aspose.Pdf.Rectangle rectOther  = new Aspose.Pdf.Rectangle(100, 620, 120, 640);

            // Add options to the radio button group with their visual positions
            genderField.AddOption("Male",   rectMale);
            genderField.AddOption("Female", rectFemale);
            genderField.AddOption("Other",  rectOther);

            // Optionally set a default selected option (index starts at 1)
            genderField.Selected = 1; // selects "Male" by default

            // Add the radio button field to the document's AcroForm
            doc.Form.Add(genderField);

            // Save the PDF with the form
            doc.Save("GenderForm.pdf");
        }

        Console.WriteLine("PDF with gender radio button group created successfully.");
    }
}