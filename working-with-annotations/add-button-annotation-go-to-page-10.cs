using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "button_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages; add blank pages if necessary
            while (doc.Pages.Count < 10)
            {
                doc.Pages.Add();
            }

            // Choose the page where the button will be placed (e.g., first page)
            Page page = doc.Pages[1];

            // Define the button's rectangle (lower‑left x/y, upper‑right x/y) in points
            Rectangle rect = new Rectangle(100, 700, 200, 750);

            // Create the button field
            ButtonField button = new ButtonField(page, rect);
            button.PartialName = "GoToPage10";
            button.AlternateCaption = "Go to Page 10"; // label shown on the button
            button.Border = new Border(button) { Width = 1 };
            // Optional visual styling – only set if the property exists in the used version
            // button.BackgroundColor = Color.LightGray;

            // Create a GoTo action that navigates to page 10 (1‑based index).
            // In older Aspose.Pdf versions the constructor accepts the target page number directly.
            GoToAction goTo = new GoToAction(10);
            // Assign the action to the button's mouse‑press event (valid property name)
            button.Actions.OnPressMouseBtn = goTo;

            // Add the button to the document's AcroForm
            doc.Form.Add(button, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation added and saved to '{outputPath}'.");
    }
}
