using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for XmlLoadOptions

class Program
{
    static void Main()
    {
        // Input XML file that will be converted to a PDF.
        const string xmlInputPath = "input.xml";

        // Directory where the per‑chapter PDFs will be written.
        const string outputDirectory = "Chapters";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlInputPath}");
            return;
        }

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputDirectory);

        // Load the XML and generate a PDF document.
        // XmlLoadOptions is the correct load option for XML → PDF conversion.
        using (Document sourceDoc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            // NOTE:
            // If the source PDF contains bookmarks (outlines) that mark chapter starts,
            // you can replace the simple per‑page loop below with logic that reads
            // sourceDoc.Outlines and extracts the start page of each chapter.
            // For brevity, this example treats each page as a separate chapter.

            int totalPages = sourceDoc.Pages.Count;   // 1‑based page count

            for (int pageIndex = 1; pageIndex <= totalPages; pageIndex++)
            {
                // Create a new empty PDF document for the current chapter.
                using (Document chapterDoc = new Document())
                {
                    // Add the current page from the source document.
                    // Pages collection is 1‑based, so we use pageIndex directly.
                    chapterDoc.Pages.Add(sourceDoc.Pages[pageIndex]);

                    // Build the output file name, e.g. "Chapter_1.pdf".
                    string chapterFileName = Path.Combine(
                        outputDirectory,
                        $"Chapter_{pageIndex}.pdf");

                    // Save the chapter PDF.
                    chapterDoc.Save(chapterFileName);
                    Console.WriteLine($"Saved chapter PDF: {chapterFileName}");
                }
            }
        }

        Console.WriteLine("All chapters have been split successfully.");
    }
}