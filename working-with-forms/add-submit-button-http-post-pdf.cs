using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 200, 540);

            // Create a push button field on the page
            ButtonField submitButton = new ButtonField(page, rect)
            {
                NormalCaption = "Submit",
                Color = Color.LightGray
            };

            // Create a SubmitFormAction
            SubmitFormAction action = new SubmitFormAction
            {
                // Export data in HTML form (URL‑encoded) format. No GetMethod flag → HTTP POST.
                Flags = SubmitFormAction.ExportFormat,
                Url = new FileSpecification(submitUrl)
            };

            // Attach the action to the button – use a valid action property.
            submitButton.Actions.OnPressMouseBtn = action;

            // Add the button to the page annotations collection
            page.Annotations.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
