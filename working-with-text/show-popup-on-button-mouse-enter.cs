using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string buttonName = "MyButton"; // name of the button field in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Get the first page (or any page you prefer) to host the floating box
            Page page = doc.Pages[1];

            // Create a rectangle for the floating box (Popup annotation)
            // Coordinates: lower-left X, lower-left Y, upper-right X, upper-right Y
            Aspose.Pdf.Rectangle popupRect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Create the Popup annotation
            PopupAnnotation popup = new PopupAnnotation(page, popupRect)
            {
                // Set the text that will appear inside the floating box
                Contents = "This is a floating box displayed on mouse entry."
                // Title property is not available in the current Aspose.Pdf version, so it is omitted.
            };

            // Add the popup annotation to the page
            page.Annotations.Add(popup);

            // Retrieve the button field by name
            ButtonField button = doc.Form[buttonName] as ButtonField;
            if (button == null)
            {
                Console.Error.WriteLine($"Button field '{buttonName}' not found.");
                return;
            }

            // Create a HideAction that will SHOW the popup (Hide = false) when the mouse enters the button
            // HideAction constructor requires the target annotation and a boolean indicating whether to hide (true) or show (false).
            HideAction showPopupAction = new HideAction(popup, false);

            // Assign the action to the button's OnEnter event
            button.Actions.OnEnter = showPopupAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
