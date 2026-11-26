using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "template.xml";          // XML source (relative to exe)
        const string imagePath = "images/picture.jpg"; // Image file (relative path)
        const string outputPdf = "output.pdf";

        // Verify required files exist
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

        // Load the XML representation of a PDF document
        using (Document doc = new Document(xmlPath))
        {
            // Ensure at least one page exists
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Open the image file as a stream and assign it to an Image object
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                Image img = new Image
                {
                    ImageStream = imgStream,
                    FixWidth = 200,
                    FixHeight = 200
                };

                // Add the image to the first page's paragraph collection
                doc.Pages[1].Paragraphs.Add(img);

                // Save the resulting PDF while the image stream is still open
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF successfully saved to '{outputPdf}'.");
    }
}
