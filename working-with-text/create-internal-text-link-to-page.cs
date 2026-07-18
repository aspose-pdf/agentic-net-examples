using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // TextFragment and LocalHyperlink are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF (can be empty or existing)
        const string outputPath = "output.pdf";         // result PDF with the text link
        const int   targetPage = 2;                     // page number to navigate to (1‑based)

        // Ensure the input file exists; if not, create a simple one‑page PDF to work with
        if (!File.Exists(inputPath))
        {
            using (Document tmp = new Document())
            {
                tmp.Pages.Add();                       // add a blank first page
                tmp.Save(inputPath);
            }
        }

        // Open the document, add a second page (the navigation target), and place a linked text fragment
        using (Document doc = new Document(inputPath))
        {
            // Add the target page if it does not already exist
            while (doc.Pages.Count < targetPage)
                doc.Pages.Add();

            // Create a text fragment that will act as the clickable link
            TextFragment linkText = new TextFragment("Go to page " + targetPage);
            linkText.Position = new Position(100, 700); // place the text on the first page

            // Assign a LocalHyperlink that points to the desired page number
            LocalHyperlink hyperlink = new LocalHyperlink
            {
                TargetPageNumber = targetPage   // internal navigation to the specified page
            };
            linkText.Hyperlink = hyperlink;    // Hyperlink property expects a Hyperlink object

            // Add the fragment to the first page (or any page you prefer)
            doc.Pages[1].Paragraphs.Add(linkText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with text link saved to '{outputPath}'.");
    }
}