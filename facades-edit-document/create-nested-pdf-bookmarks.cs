using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for GoToAction and other PDF actions

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bookmarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document doc = new Document(inputPath);

            // ---------- Create a three‑level bookmark hierarchy ----------
            // Level 1 – Section (top‑level bookmark)
            OutlineItemCollection section = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Section 1",
                Action = new GoToAction(1) // page numbers are 1‑based
            };
            doc.Outlines.Add(section);

            // Level 2 – Subsection (child of Section)
            OutlineItemCollection subsection = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Subsection 1.1",
                Action = new GoToAction(2)
            };
            // Add as child of the section
            section.Add(subsection);

            // Level 3 – Sub‑subsection (child of Subsection)
            OutlineItemCollection subsubsection = new OutlineItemCollection(doc.Outlines)
            {
                Title = "Sub‑subsection 1.1.1",
                Action = new GoToAction(3)
            };
            // Add as child of the subsection
            subsection.Add(subsubsection);
            // ------------------------------------------------------------

            // Save the PDF with the new bookmark hierarchy
            doc.Save(outputPath);

            Console.WriteLine($"Bookmarks added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
