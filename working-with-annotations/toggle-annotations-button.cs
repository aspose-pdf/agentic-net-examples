using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toggle_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page on which the button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the button annotation (coordinates are in points)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(50, 750, 150, 800);

            // Create a push‑button form field
            ButtonField toggleButton = new ButtonField(page, buttonRect)
            {
                Name = "ToggleAnnotations",
                AlternateName = "Toggle Annotations"
            };

            // Add the button to the document's form collection
            doc.Form.Add(toggleButton);

            // Collect all annotations on the page except the button itself
            Annotation[] otherAnnotations = page.Annotations
                                                .Where(a => a != toggleButton)
                                                .ToArray();

            // Create HideAction instances: one to hide, one to show the collected annotations
            HideAction hideAction = new HideAction(otherAnnotations, true);   // hide
            HideAction showAction = new HideAction(otherAnnotations, false); // show

            // Assign the actions to the button.
            // OnPressMouseBtn hides the annotations, OnReleaseMouseBtn shows them again.
            toggleButton.Actions.OnPressMouseBtn   = hideAction;
            toggleButton.Actions.OnReleaseMouseBtn = showAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'");
    }
}