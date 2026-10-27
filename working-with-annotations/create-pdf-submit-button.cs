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
        const string endpoint = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push‑button form field on page 1
            ButtonField submitButton = new ButtonField(doc.Pages[1], buttonRect)
            {
                PartialName = "SubmitButton",
                Value = "Submit"
            };

            // Attach JavaScript that submits the form data to the specified HTTP endpoint
            // Use a valid action property from AnnotationActionCollection (OnPressMouseBtn)
            submitButton.Actions.OnPressMouseBtn = new JavascriptAction($"this.submitForm('{endpoint}');");

            // Add the button to the document's form collection on page 1
            doc.Form.Add(submitButton, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
