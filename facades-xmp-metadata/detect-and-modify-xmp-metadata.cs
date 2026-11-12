using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Detect XMP metadata via the Document.Metadata dictionary (PdfFileInfo.XmpMetadata does not exist)
        bool hasXmp;
        using (Document metaDoc = new Document(inputPath))
        {
            hasXmp = metaDoc.Metadata != null && metaDoc.Metadata.Count > 0;
        }
        Console.WriteLine($"XMP metadata present: {hasXmp}");

        // Perform modifications only if XMP metadata exists
        if (hasXmp)
        {
            // Load the PDF for modification (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count > 0)
                {
                    Page page = doc.Pages[1];

                    // Create a text annotation indicating presence of XMP metadata
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                    TextAnnotation annotation = new TextAnnotation(page, rect)
                    {
                        Title = "Info",
                        Contents = "Document contains XMP metadata.",
                        Open = true,
                        Icon = TextIcon.Note
                    };
                    page.Annotations.Add(annotation);
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
        }
        else
        {
            Console.WriteLine("No modifications performed because XMP metadata is missing.");
        }
    }
}
