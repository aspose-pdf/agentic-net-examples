using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";
        const string imagePath = "image.jpg";
        const string outputPdf = "output.pdf";

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

        // Load PDF structure from the XML file
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Ensure there is at least one page to add the image to
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Add the image to the first page
            Page page = doc.Pages[1];
            Image img = new Image
            {
                ImageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read)
            };
            page.Paragraphs.Add(img);

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}