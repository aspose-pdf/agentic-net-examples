using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction, AnnotationFlags
using Aspose.Pdf.Forms;      // ButtonField

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitUrl  = "https://example.com/endpoint";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a button field (form button) and assign JavaScript that submits the form
            ButtonField submitButton = new ButtonField(page, rect)
            {
                PartialName   = "SubmitButton",
                NormalCaption = "Submit"
            };
            // JavaScript that submits the form data to the specified endpoint
            submitButton.OnActivated = new JavascriptAction($"this.submitForm('{submitUrl}');");

            // Add the button field to the document's form collection (page number = 1)
            doc.Form.Add(submitButton, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation added and saved to '{outputPath}'.");
    }
}
