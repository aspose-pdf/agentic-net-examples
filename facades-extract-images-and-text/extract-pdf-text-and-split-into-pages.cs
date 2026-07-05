using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedPages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // If the source PDF does not exist, create a tiny sample PDF so the demo runs without external files
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdf(inputPdf);
        }

        // Extract the whole text (Aspose inserts a form‑feed '\f' between pages)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractText();

            // Get the complete text as a single string using the overload that writes to a stream
            string fullText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write text to the stream
                textStream.Position = 0;       // rewind for reading
                using (StreamReader reader = new StreamReader(textStream))
                {
                    fullText = reader.ReadToEnd();
                }
            }

            // Optional: store the raw text file
            File.WriteAllText(Path.Combine(outputFolder, "full_text.txt"), fullText);

            // Split the text into pages using the page delimiter returned by GetText ("\f")
            string[] pages = fullText.Split('\f');

            for (int i = 0; i < pages.Length; i++)
            {
                string pageFile = Path.Combine(outputFolder, $"Page_{i + 1}.txt");
                // Trim to remove leading/trailing new‑lines that may appear after the split
                File.WriteAllText(pageFile, pages[i].Trim());
            }
        }

        Console.WriteLine("Text extraction and split completed.");
    }

    // Helper that creates a minimal two‑page PDF when the expected file is missing
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Page 1
            Page page1 = doc.Pages.Add();
            page1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello from page 1"));

            // Page 2
            Page page2 = doc.Pages.Add();
            page2.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello from page 2"));

            doc.Save(path);
        }
    }
}
