using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlInputPath = "input.xml";               // XML source
        const string outputFolder = "Chapters";                // Folder for split PDFs
        const string chapterKeyword = "Chapter";               // Simple marker for chapter start

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load XML and generate PDF
        using (Document sourceDoc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            // Determine page numbers where a new chapter starts
            List<int> chapterStartPages = new List<int>();

            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                sourceDoc.Pages[i].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Simple heuristic: page contains the word "Chapter"
                if (pageText.IndexOf(chapterKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    chapterStartPages.Add(i);
                }
            }

            // If no markers were found, treat the whole document as a single chapter
            if (chapterStartPages.Count == 0)
            {
                chapterStartPages.Add(1);
            }

            // Add an end sentinel (one past the last page) to simplify range handling
            chapterStartPages.Add(sourceDoc.Pages.Count + 1);

            // Split the document according to the detected ranges
            for (int idx = 0; idx < chapterStartPages.Count - 1; idx++)
            {
                int startPage = chapterStartPages[idx];
                int endPage   = chapterStartPages[idx + 1] - 1; // inclusive

                using (Document chapterDoc = new Document())
                {
                    // Copy pages for this chapter
                    for (int p = startPage; p <= endPage; p++)
                    {
                        // Add a copy of each page to the new document
                        chapterDoc.Pages.Add(sourceDoc.Pages[p]);
                    }

                    // Build a file name like "Chapter_1.pdf", "Chapter_2.pdf", etc.
                    string chapterFileName = Path.Combine(
                        outputFolder,
                        $"Chapter_{idx + 1}.pdf");

                    chapterDoc.Save(chapterFileName);
                    Console.WriteLine($"Saved chapter {idx + 1}: {chapterFileName}");
                }
            }
        }
    }
}