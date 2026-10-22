using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // Form field classes (ButtonField, etc.)
using Aspose.Pdf.Annotations;    // Action classes (SubmitFormAction, FileSpecification)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_submit.pdf";
        const string fieldName  = "SubmitForm";
        const string submitUrl  = "https://api.example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the button rectangle (lower‑left X/Y, upper‑right X/Y) in points.
        Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Ensure the target page exists (pages are 1‑based in Aspose.Pdf).
        Page page = pdfDocument.Pages[1];

        // Create a button field (AcroForm button).
        ButtonField submitButton = new ButtonField(page, buttonRect)
        {
            PartialName = fieldName,
            Value = "Submit" // Text displayed on the button (optional)
        };

        // Create a SubmitFormAction using the parameter‑less constructor.
        SubmitFormAction submitAction = new SubmitFormAction();
        submitAction.Url = new FileSpecification(submitUrl);

        // Assign the action to a valid event property of the button.
        // "OnPressMouseBtn" is a supported property of AnnotationActionCollection.
        submitButton.Actions.OnPressMouseBtn = submitAction;

        // Add the button to the document's form collection.
        pdfDocument.Form.Add(submitButton, 1);

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}
