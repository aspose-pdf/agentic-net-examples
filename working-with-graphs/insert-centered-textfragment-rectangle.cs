using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Define the rectangle where the text will be centered
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 300; // upper‑right X
        double ury = 600; // upper‑right Y

        // Optional: create a Rectangle object for clarity (not strictly required)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // Compute the centre point of the rectangle
        double centerX = (rect.LLX + rect.URX) / 2;
        double centerY = (rect.LLY + rect.URY) / 2;

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Hello World");

            // Align the text horizontally (vertical alignment is not supported directly on TextFragmentState)
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Position the fragment at the centre of the rectangle (baseline will be at centreY)
            tf.Position = new Position(centerX, centerY);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with centered text to '{outputPath}'.");
    }
}
