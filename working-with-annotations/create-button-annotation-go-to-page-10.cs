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
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("Document must contain at least 10 pages.");
                return;
            }

            // Choose the page where the button will be placed (e.g., first page)
            Page targetPage = doc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the button field first (no object‑initializer that references the variable itself)
            ButtonField button = new ButtonField(targetPage, buttonRect);

            // Set visual properties
            button.Color = Aspose.Pdf.Color.LightGray;
            button.Border = new Border(button) { Width = 1 };
            button.AlternateName = "GoToPage10";

            // Set the action to navigate to page 10 of the same document
            button.OnActivated = new GoToAction(doc.Pages[10]);

            // Add the button to the page annotations collection
            targetPage.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with button saved to '{outputPath}'.");
    }
}
