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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form object
            Form form = doc.Form;

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button on the first page
            Page firstPage = doc.Pages[1];
            ButtonField submitButton = new ButtonField(firstPage, buttonRect);

            // Set the field name and visible caption
            submitButton.PartialName      = "SubmitForm";
            submitButton.AlternateCaption = "Submit";

            // Create a submit-form action that posts to the desired URL
            SubmitFormAction submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification("https://api.example.com/submit");

            // Assign the action to the button activation event
            submitButton.OnActivated = submitAction;

            // Add the button to the document's form (page number is optional here)
            form.Add(submitButton, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}
