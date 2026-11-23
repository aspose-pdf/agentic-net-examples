using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document is tagged (creates a tagged structure if missing)
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // -----------------------------------------------------------------
            // 1. Add a new page that will hold the Table of Contents (TOC)
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Add a visible title on the page
            TextFragment tocTitle = new TextFragment("Table of Contents")
            {
                // Simple formatting for the title
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold }
            };
            tocPage.Paragraphs.Add(tocTitle);

            // -----------------------------------------------------------------
            // 2. Build the logical structure for the TOC
            // -----------------------------------------------------------------
            // Root element of the existing tagged structure
            StructureElement root = taggedContent.RootElement; // no cast needed

            // Create a container (Div) for the TOC entries
            DivElement tocDiv = taggedContent.CreateDivElement();
            tocDiv.AlternativeText = "Accessible table of contents";
            root.AppendChild(tocDiv); // attach to the document's structure tree

            // Example hierarchical headings
            // H1 – Chapter 1
            HeaderElement chapter1 = taggedContent.CreateHeaderElement(1);
            chapter1.SetText("Chapter 1: Introduction");
            tocDiv.AppendChild(chapter1);

            // H2 – Section 1.1
            HeaderElement section11 = taggedContent.CreateHeaderElement(2);
            section11.SetText("1.1 Overview");
            tocDiv.AppendChild(section11);

            // H2 – Section 1.2
            HeaderElement section12 = taggedContent.CreateHeaderElement(2);
            section12.SetText("1.2 Background");
            tocDiv.AppendChild(section12);

            // H1 – Chapter 2
            HeaderElement chapter2 = taggedContent.CreateHeaderElement(1);
            chapter2.SetText("Chapter 2: Methodology");
            tocDiv.AppendChild(chapter2);

            // H2 – Section 2.1
            HeaderElement section21 = taggedContent.CreateHeaderElement(2);
            section21.SetText("2.1 Data Collection");
            tocDiv.AppendChild(section21);

            // (Additional headings can be added in the same way)

            // -----------------------------------------------------------------
            // 3. Save the modified PDF (no PreSave call required)
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with accessible TOC saved to '{outputPath}'.");
    }
}