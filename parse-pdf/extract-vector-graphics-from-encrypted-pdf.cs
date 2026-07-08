using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string password   = "userPassword";
        const string outputDir  = "ExtractedGraphics";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Open the encrypted PDF by providing the password to the Document constructor.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath, password))
        {
            // Decrypt the document in memory (optional – required only if you need to save an unprotected copy later).
            doc.Decrypt();

            int graphicCount = 0;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Paragraphs collection may contain Graph objects that represent vector graphics.
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Aspose.Pdf.Drawing.Graph graph)
                    {
                        // Create a new PDF document to hold the extracted graphic.
                        using (Aspose.Pdf.Document graphicDoc = new Aspose.Pdf.Document())
                        {
                            // Add a blank page.
                            Page newPage = graphicDoc.Pages.Add();

                            // Add the graph to the new page.
                            newPage.Paragraphs.Add(graph);

                            // Save each graphic as a separate PDF file.
                            string outPath = System.IO.Path.Combine(outputDir, $"Graphic_{++graphicCount}.pdf");
                            graphicDoc.Save(outPath);
                            Console.WriteLine($"Extracted graphic saved to '{outPath}'.");
                        }
                    }
                }
            }

            if (graphicCount == 0)
                Console.WriteLine("No vector graphics found in the document.");
        }
    }
}
