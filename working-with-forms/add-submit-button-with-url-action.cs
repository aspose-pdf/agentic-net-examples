using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF (can be empty)
        const string outputPath = "output.pdf";         // PDF with configured submit button
        const string buttonName = "SubmitBtn";          // name of the button field
        const string buttonLabel = "Submit Form";       // caption displayed on the button
        const string submitUrl = "https://example.com/submit"; // URL to submit to

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the button on
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Define the button rectangle (lower‑left X,Y and upper‑right X,Y)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push‑button field
            ButtonField submitButton = new ButtonField(page, btnRect)
            {
                PartialName   = buttonName,
                NormalCaption = buttonLabel
            };

            // Create the SubmitFormAction and assign the destination URL
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl)
            };

            // Attach the action to the button (executed when the button is activated)
            submitButton.OnActivated = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with submit button at '{outputPath}'.");
    }
}