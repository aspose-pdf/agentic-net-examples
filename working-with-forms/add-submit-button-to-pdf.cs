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
        const string buttonName = "SubmitBtn";               // name of the button field in the PDF
        const string submitUrl = "https://example.com/submit"; // URL to which the form will be submitted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the button field from the form collection
            // The field must exist; otherwise, handle the null case.
            ButtonField button = doc.Form[buttonName] as ButtonField;
            if (button == null)
            {
                Console.Error.WriteLine($"Button field '{buttonName}' not found.");
                return;
            }

            // Create a SubmitFormAction and set its target URL via a FileSpecification
            SubmitFormAction submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification(submitUrl);

            // Assign the action to the button's activation event
            button.OnActivated = submitAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Submit button configured and saved to '{outputPath}'.");
    }
}
