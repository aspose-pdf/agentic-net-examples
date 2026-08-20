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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("Document does not contain page 10.");
                return;
            }

            // Page where the button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the button rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the button field on the specified page
            ButtonField button = new ButtonField(page, btnRect)
            {
                PartialName       = "BtnGoToPage10",          // internal field name
                AlternateCaption  = "Go to Page 10",          // tooltip / caption
                // Assign a GoToAction that points to page 10 of the same document
                OnActivated       = new GoToAction(doc.Pages[10])
            };

            // Add the button to the page's annotation collection
            page.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button annotation added. Saved to '{outputPath}'.");
    }
}