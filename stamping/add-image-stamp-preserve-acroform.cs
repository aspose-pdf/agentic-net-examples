using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for files used in the example
        string inputPath = "input.pdf";
        string stampImagePath = "stamp.png";
        string outputPath = "output.pdf";

        // Create a tiny PNG image (1x1 red pixel) and write it to disk
        byte[] pngBytes = Convert.FromBase64String(
            "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK9cAAAAASUVORK5CYII=");
        File.WriteAllBytes(stampImagePath, pngBytes);

        // ---------------------------------------------------------------------
        // Step 1: Create a sample PDF that contains an AcroForm text box field
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            // Define the rectangle for the form field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField textBox = new TextBoxField(doc.Pages[1], fieldRect);
            textBox.PartialName = "SampleField";
            textBox.Value = "Initial value";
            doc.Form.Add(textBox, 1);
            doc.Save(inputPath);
        }

        // ---------------------------------------------------------------
        // Step 2: Open the PDF and add an image stamp to each page
        // ---------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPath))
        {
            ImageStamp imgStamp = new ImageStamp(stampImagePath);
            // Position the stamp 50 points from the left and bottom edges
            imgStamp.XIndent = 50f;
            imgStamp.YIndent = 50f;
            imgStamp.Opacity = 0.5f; // semi‑transparent stamp

            // Apply the stamp to every page; form fields remain untouched
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            pdfDoc.Save(outputPath);
        }
    }
}
