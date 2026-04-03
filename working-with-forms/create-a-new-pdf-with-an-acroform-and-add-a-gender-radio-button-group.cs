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
            // Add a blank page (required before placing form fields)
            Page page = doc.Pages.Add();

            // Create a radio button field (group) on the *page* – not just the document.
            // Using the constructor that receives the page ensures the internal annotation
            // has a valid Page reference, preventing the NullReferenceException.
            RadioButtonField genderField = new RadioButtonField(page)
            {
                Name = "Gender",
                PartialName = "Gender",
                NoToggleToOff = false // allow deselection if needed
            };

            // Define rectangles for each radio button option.
            // Aspose.Pdf.Rectangle expects (llx, lly, urx, ury) – lower‑left and upper‑right coordinates.
            // Here we create 20×20 boxes at the desired positions.
            Aspose.Pdf.Rectangle maleRect   = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            Aspose.Pdf.Rectangle femaleRect = new Aspose.Pdf.Rectangle(100, 660, 120, 680);
            Aspose.Pdf.Rectangle otherRect  = new Aspose.Pdf.Rectangle(100, 620, 120, 640);

            // Add options to the radio button group with their visual positions
            genderField.AddOption("Male",   maleRect);
            genderField.AddOption("Female", femaleRect);
            genderField.AddOption("Other",  otherRect);

            // Optionally set default selected option (1‑based index)
            genderField.Selected = 1; // selects "Male" by default

            // Add the radio button field to the document's form
            doc.Form.Add(genderField);

            // Save the PDF to a file
            doc.Save("GenderRadioButtonForm.pdf");
        }

        Console.WriteLine("PDF with gender radio button group created successfully.");
    }
}
