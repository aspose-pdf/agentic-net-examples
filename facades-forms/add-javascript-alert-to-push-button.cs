using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // 1. Create a minimal PDF with a push‑button field named "ShowInfo"
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a single page.
            Page page = seed.Pages.Add();

            // Define the button rectangle (left, bottom, right, top).
            var buttonRect = new Rectangle(100, 600, 200, 650);

            // Create the button field. Aspose.PDF uses ButtonField (no PushButtonField).
            var button = new ButtonField(page, buttonRect)
            {
                // The name (partial name) of the field.
                PartialName = "ShowInfo",
                // The text displayed on the button.
                Value = "Show Info"
            };

            // Add the button to the form of the document (page number = 1).
            seed.Form.Add(button, 1);

            // Save the placeholder PDF that will be used later.
            seed.Save(inputPath);
        }

        // ------------------------------------------------------------
        // 2. Open the PDF with FormEditor and attach JavaScript.
        // ------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF we just created.
            editor.BindPdf(inputPath);

            // Attach JavaScript to the push‑button field.
            // The script shows an alert when the button is clicked.
            editor.AddFieldScript("ShowInfo", "app.alert('Form loaded');");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"JavaScript added to button and saved to '{outputPath}'.");
    }
}
