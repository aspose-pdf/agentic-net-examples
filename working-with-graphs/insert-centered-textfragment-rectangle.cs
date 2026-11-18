using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // If the input file does not exist, create a simple one-page PDF.
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
        }

        // Open the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Define the rectangle where the text will be centered.
            double llx = 100; // lower‑left X
            double lly = 500; // lower‑left Y
            double urx = 400; // upper‑right X
            double ury = 700; // upper‑right Y

            // Compute the rectangle's centre point.
            double centerX = (llx + urx) / 2;
            double centerY = (lly + ury) / 2;

            // Create the TextFragment with the desired content.
            TextFragment tf = new TextFragment("Hello World");

            // Position the fragment at the centre of the rectangle.
            tf.Position = new Position(centerX, centerY);

            // Align the text horizontally to the centre.
            tf.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            // Note: TextFragmentState does not expose a VerticalAlignment property.
            // Centering vertically is achieved by positioning the fragment at the rectangle's centre.

            // Add the fragment to the page's paragraph collection.
            page.Paragraphs.Add(tf);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"TextFragment added and saved to '{outputPath}'.");
    }
}
