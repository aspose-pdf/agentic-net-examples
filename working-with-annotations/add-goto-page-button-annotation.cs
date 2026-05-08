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

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages (add blank pages if needed)
            while (doc.Pages.Count < 10)
                doc.Pages.Add();

            // Choose the page where the button will be placed (e.g., first page)
            Page targetPage = doc.Pages[1];

            // Define the button rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the button field on the chosen page
            ButtonField button = new ButtonField(targetPage, buttonRect)
            {
                PartialName   = "GoToPage10",          // internal name
                NormalCaption = "Go to Page 10"        // visible label
            };

            // Set the action to navigate to page 10 of the same document
            button.OnActivated = new GoToAction(doc.Pages[10]);

            // Add the button to the page's annotation collection
            targetPage.Annotations.Add(button);

            // Save the modified PDF (using the required lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation created and saved to '{outputPath}'.");
    }
}