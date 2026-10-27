using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for SubmitFormAction

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the button rectangle (lower‑left x, lower‑right y, upper‑right x, upper‑right y)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a button field and give it a name
            var submitButton = new ButtonField(page, rect)
            {
                PartialName = "SubmitBtn",
                NormalCaption = "Submit" // Button label
            };

            // Assign the SubmitForm action (URL) to the button's activation event
            var submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification(submitUrl);
            submitButton.OnActivated = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Submit button configured and saved to '{outputPath}'.");
    }
}
