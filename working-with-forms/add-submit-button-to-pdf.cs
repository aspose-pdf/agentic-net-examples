using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the page
            ButtonField button = new ButtonField(page, rect);
            button.PartialName = "SubmitBtn";
            button.NormalCaption = "Submit";

            // Create a SubmitFormAction and set its destination URL
            SubmitFormAction submitAction = new SubmitFormAction();
            // Url property expects a FileSpecification, not a plain string
            submitAction.Url = new FileSpecification(submitUrl);
            // Example: submit the form in HTML format
            submitAction.Flags = SubmitFormAction.ExportFormat;

            // Assign the action to the button's mouse‑press event (click)
            button.Actions.OnPressMouseBtn = submitAction;

            // Add the button annotation to the page
            page.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with submit button to '{outputPath}'.");
    }
}
