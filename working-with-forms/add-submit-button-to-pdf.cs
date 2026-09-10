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

        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button on the first page
            ButtonField button = new ButtonField(doc.Pages[1], rect)
            {
                Name = "SubmitBtn",
                Contents = "Submit"
            };

            // Create a SubmitFormAction and set its URL using a FileSpecification instance
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl, "Submit URL")
            };

            // Assign the action to a valid button event (e.g., mouse‑up)
            button.Actions.OnReleaseMouseBtn = submitAction;

            // Add the button to the page annotations collection
            doc.Pages[1].Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with submit button at '{outputPath}'.");
    }
}
