using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // Facade classes for editing
using Aspose.Pdf.Tagged;               // ITaggedContent interface
using Aspose.Pdf.LogicalStructure;     // Structure element types
using Aspose.Pdf.Text;                 // TextFragment, DefaultAppearance, etc.

class AccessiblePdfCreator
{
    static void Main()
    {
        const string outputPath = "accessible_output.pdf";
        const string imagePath  = "sample_image.jpg";   // Path to an image to embed

        // Ensure the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a page – required for visual content
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Add visible content (text and image) to the page
            // -------------------------------------------------

            // Header text (visible)
            TextFragment header = new TextFragment("Accessible PDF Example");
            header.TextState.FontSize = 18;
            header.TextState.Font = FontRepository.FindFont("Helvetica");
            header.Position = new Position(50, 750);   // Position near top of page
            page.Paragraphs.Add(header);

            // Paragraph text (visible)
            TextFragment paragraph = new TextFragment(
                "This PDF has been created with proper tagging to improve accessibility. " +
                "The image below includes alternative text.");
            paragraph.TextState.FontSize = 12;
            paragraph.TextState.Font = FontRepository.FindFont("Helvetica");
            paragraph.Position = new Position(50, 720);
            page.Paragraphs.Add(paragraph);

            // Use PdfFileMend facade to embed the image on the same page
            // (AddImage expects coordinates: llx, lly, urx, ury)
            PdfFileMend mend = new PdfFileMend(doc);
            // Place the image at (100,400) lower‑left and (300,600) upper‑right
            mend.AddImage(imagePath, 1, 100, 400, 300, 600);

            // -------------------------------------------------
            // Create tagged (logical) structure for accessibility
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (metadata for accessibility)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Accessible PDF Example");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a section element to group content
            SectElement sect = tagged.CreateSectElement();
            root.AppendChild(sect);

            // Header element (H1) – corresponds to the visual header
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Accessible PDF Example");
            sect.AppendChild(h1);

            // Paragraph element – corresponds to the visual paragraph
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This PDF has been created with proper tagging to improve accessibility. " +
                         "The image below includes alternative text.");
            sect.AppendChild(para);

            // Figure element – represents the image with alternative text
            FigureElement figure = tagged.CreateFigureElement();
            figure.AlternativeText = "Sample illustration showing the concept of accessibility.";
            // Associate the visual image with the figure element
            // The SetImage method embeds the image file and links it to the figure
            figure.SetImage(imagePath);
            sect.AppendChild(figure);

            // -------------------------------------------------
            // Save the tagged PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}