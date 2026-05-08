using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string dicomPath = "image.dcm";

        // Desired size of the image on the page (points; 1 point = 1/72 inch)
        double customWidth  = 200; // e.g., 200 points
        double customHeight = 300; // e.g., 300 points

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(dicomPath))
        {
            Console.Error.WriteLine($"DICOM image not found: {dicomPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Enable automatic tagging if the document is not already tagged
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Access tagged content (creates a tagged structure if none exists)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(outputPdf));

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a figure element (used for images) and set the DICOM image with custom dimensions
            FigureElement figure = tagged.CreateFigureElement();
            figure.SetImage(dicomPath, customWidth, customHeight);
            figure.AlternativeText = "DICOM image";

            // Attach the figure to the document structure
            root.AppendChild(figure);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with DICOM image saved to '{outputPdf}'.");
    }
}
