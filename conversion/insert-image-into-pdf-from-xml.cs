using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath   = @"C:\Data\input.xml";      // XML source file
        const string imagePath = @"C:\Images\picture.jpg"; // Absolute image file path
        const string outputPdf = @"C:\Output\result.pdf";

        // Verify files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF structure from the XML file
        using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create an Image element using the parameter‑less constructor
            // and supply the image data via the ImageStream property.
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                Image img = new Image { ImageStream = fs };
                // Optional: fit the image to the page width
                img.FixWidth = page.PageInfo.Width;

                // Add the image to the page's paragraph collection (binds it to the page)
                page.Paragraphs.Add(img);

                // Save the modified document as PDF while the stream is still open
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF saved to: {outputPdf}");
    }
}
