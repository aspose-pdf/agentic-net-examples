using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";               // Path to the source XML file
        const string outputDir = "Chapters";               // Folder where chapter PDFs will be saved

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the XML and convert it to a PDF document (in memory)
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(xmlPath, new XmlLoadOptions()))
        {
            // Simple chapter split: treat each page as a separate chapter.
            // Adjust the logic here if chapters span multiple pages (e.g., based on bookmarks).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Create a new PDF document for the current chapter
                using (Aspose.Pdf.Document chapterDoc = new Aspose.Pdf.Document())
                {
                    // Copy the page from the source document into the chapter document
                    chapterDoc.Pages.Add(pdfDoc.Pages[i]);

                    // Build the output file name (e.g., Chapter_1.pdf, Chapter_2.pdf, ...)
                    string outPath = Path.Combine(outputDir, $"Chapter_{i}.pdf");

                    // Save the chapter PDF
                    chapterDoc.Save(outPath);
                    Console.WriteLine($"Saved chapter {i} to '{outPath}'.");
                }
            }
        }
    }
}