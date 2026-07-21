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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages (add blanks if necessary)
            while (doc.Pages.Count < 10)
            {
                doc.Pages.Add();
            }

            // Define the button's rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push‑button field on the first page
            ButtonField button = new ButtonField(doc.Pages[1], buttonRect)
            {
                Name             = "GoToPage10",
                NormalCaption    = "Page 10",
                AlternateCaption = "Go to Page 10"
            };

            // Set the action that will be executed when the button is activated:
            // Go to page 10 of the same document.
            GoToAction goToPage10 = new GoToAction(doc.Pages[10]);
            button.OnActivated = goToPage10;

            // Add the button annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation created and saved to '{outputPath}'.");
    }
}