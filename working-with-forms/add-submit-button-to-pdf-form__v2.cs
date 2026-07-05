using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (Document is disposed automatically by using)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the button rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the push button field on the selected page
            ButtonField submitButton = new ButtonField(page, rect)
            {
                Name = "SubmitForm",                 // Field name
                AlternateCaption = "Submit",         // Text shown when button is pressed
                NormalCaption = "Submit",            // Text shown normally
                Color = Aspose.Pdf.Color.LightGray   // Optional visual styling
            };

            // Create a SubmitFormAction that posts to the desired URL
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Url property expects a FileSpecification, not a raw string
                Url = new FileSpecification("https://api.example.com/submit")
                // Additional flags (e.g., ExportFormat, SubmitPdf) can be set via the Flags property if needed
            };

            // Assign the action to the button's activation event
            submitButton.OnActivated = submitAction;

            // Add the button field to the document's form collection
            doc.Form.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}
