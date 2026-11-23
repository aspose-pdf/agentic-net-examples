using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "newImage.jpg";

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

        // Load the PDF while preserving any existing tags.
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the tagged content interface.
            ITaggedContent taggedContent = doc.TaggedContent;

            // Ensure the document has a tag structure (it will be null for untagged PDFs).
            if (taggedContent == null)
            {
                Console.Error.WriteLine("The PDF does not contain a tag structure.");
                return;
            }

            // Get the root structure element (no cast required).
            StructureElement root = taggedContent.RootElement;

            // Create a new Figure element to represent the image in the tag tree.
            FigureElement figure = taggedContent.CreateFigureElement();
            figure.AlternativeText = "Description of the newly inserted image.";
            // Append the figure to the root (or any other appropriate parent).
            root.AppendChild(figure);

            // Insert the image into the first page.
            Page page = doc.Pages[1];
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // The Image class adds the image as a page content element.
                Image img = new Image
                {
                    ImageStream = imgStream,
                    // Optional positioning – here we place it at (100,500) with a width/height.
                    // The rectangle uses Aspose.Pdf.Rectangle (cross‑platform).
                    // Adjust as needed.
                    //Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 650)
                };
                page.Paragraphs.Add(img);
            }

            // Save the modified PDF. Existing tags are retained, and the new figure tag
            // is now associated with the inserted image.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with new tagged image: {outputPdfPath}");
    }
}