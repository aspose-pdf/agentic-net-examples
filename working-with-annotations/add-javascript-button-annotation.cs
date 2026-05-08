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
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 4 pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a fourth page.");
                return;
            }

            // Define the button rectangle (llx, lly, urx, ury) on page 4
            // Adjust coordinates as needed; here we place a 150x30 button at (100, 500)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 250, 530);

            // Create the button field on page 4
            ButtonField button = new ButtonField(doc.Pages[4], buttonRect)
            {
                // Set the visible caption of the button
                NormalCaption = "Click Me"
            };

            // Assign a JavaScript action that shows an alert when the button is pressed
            JavascriptAction jsAction = new JavascriptAction("app.alert('Button clicked!');");
            button.Actions.OnPressMouseBtn = jsAction;

            // Add the button to the page's annotations collection
            doc.Pages[4].Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}