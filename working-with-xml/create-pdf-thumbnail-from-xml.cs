using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string thumbnailPath = "thumb.png";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file and convert it to a PDF document
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Ensure the document contains at least one page
            if (pdfDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF document has no pages.");
                return;
            }

            // Create a thumbnail device (default size is 200x200 pixels)
            // ThumbnailDevice does NOT implement IDisposable, so do NOT wrap it in a using block
            ThumbnailDevice thumbDevice = new ThumbnailDevice();

            // Generate the thumbnail for the first page and save it as PNG
            using (FileStream outStream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.Write))
            {
                thumbDevice.Process(pdfDoc.Pages[1], outStream);
            }
        }

        Console.WriteLine($"Thumbnail image saved to '{thumbnailPath}'.");
    }
}
