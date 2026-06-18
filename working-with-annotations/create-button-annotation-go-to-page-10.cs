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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages
            while (doc.Pages.Count < 10)
                doc.Pages.Add();

            // Destination page (page 10, 1‑based indexing)
            Page targetPage = doc.Pages[10];

            // Page where the button will be placed (first page)
            Page firstPage = doc.Pages[1];

            // Define the button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the button field
            ButtonField button = new ButtonField(firstPage, btnRect);
            button.Color = Aspose.Pdf.Color.LightGray;          // visual background color
            button.NormalCaption = "Go to Page 10";            // text shown on the button
            button.Border = new Border(button) { Width = 1 };  // thin border

            // Assign the action that navigates to page 10 when the button is activated
            button.OnActivated = new GoToAction(targetPage);

            // Add the button to the page's annotation collection
            firstPage.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation saved to '{outputPath}'.");
    }
}