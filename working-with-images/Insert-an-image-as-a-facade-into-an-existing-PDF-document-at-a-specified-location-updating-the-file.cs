using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // existing PDF
        const string imagePath     = "logo.png";    // image to insert
        const string outputPdfPath = "output.pdf";  // updated PDF

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Select the target page (Aspose.Pdf uses 1‑based indexing)
            Page targetPage = doc.Pages[1];

            // Define the rectangle where the image will be placed:
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Insert the image onto the page using the AddImage overload that takes a file path.
            targetPage.AddImage(imagePath, imageRect);

            // Save the modified document (PDF format, no extra SaveOptions needed)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdfPath}'.");
    }
}