using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "GenderForm.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // page index will be 1 (1‑based)

            // Initialize FormEditor and bind it to the document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // Define the radio button options for the gender field
                formEditor.Items = new string[] { "Male", "Female", "Other" };
                formEditor.RadioGap = 20;      // space between buttons (pixels)
                formEditor.RadioHoriz = true; // arrange horizontally

                // Add the radio button group named "Gender" on page 1
                // llx, lly, urx, ury define the rectangle that will contain the group
                formEditor.AddField(FieldType.Radio, "Gender", 1, 100, 600, 250, 630);

                // Persist the PDF with the newly added AcroForm
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with gender radio button saved to '{outputPath}'.");
    }
}