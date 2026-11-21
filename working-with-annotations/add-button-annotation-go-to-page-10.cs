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
        const string outputPath = "button_navigate.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages (add blanks if necessary)
            while (doc.Pages.Count < 10)
                doc.Pages.Add();

            // Define the button rectangle (lower‑left X,Y and upper‑right X,Y)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push‑button form field on the first page
            ButtonField button = new ButtonField(doc.Pages[1], buttonRect)
            {
                PartialName = "GoToPage10"
            };

            // Set visual appearance (DefaultAppearance must be set before the caption)
            button.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            button.NormalCaption = "Go to Page 10";
            button.Color = Aspose.Pdf.Color.LightGray; // background color of the button

            // Border – must be created after the button instance exists
            button.Border = new Border(button)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };

            // Assign a GoTo action that points directly to page 10 (1‑based indexing)
            button.OnActivated = new GoToAction(doc.Pages[10]);

            // Add the button to the document's form collection
            doc.Form.Add(button, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with navigation button saved to '{outputPath}'.");
    }
}
