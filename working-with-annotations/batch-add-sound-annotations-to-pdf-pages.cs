using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_tagged.pdf";

        // Audio files – one per page (ensure the array length matches the number of pages)
        string[] audioFiles = new string[]
        {
            "page1.wav",
            "page2.wav",
            "page3.wav",
            // add more entries as needed
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Verify that all audio files exist before processing
        for (int i = 0; i < audioFiles.Length; i++)
        {
            if (!File.Exists(audioFiles[i]))
            {
                Console.Error.WriteLine($"Audio file not found: {audioFiles[i]} (page {i + 1})");
                return;
            }
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Ensure we have at least as many audio files as pages
                int pageCount = doc.Pages.Count;
                if (audioFiles.Length < pageCount)
                {
                    Console.Error.WriteLine($"Not enough audio files ({audioFiles.Length}) for {pageCount} pages.");
                    return;
                }

                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int i = 1; i <= pageCount; i++)
                {
                    Page page = doc.Pages[i];

                    // Define a rectangle where the annotation icon will appear
                    // (llx, lly, urx, ury) – adjust coordinates as needed
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                    // Create the sound annotation for the current page
                    // Constructor: SoundAnnotation(Page page, Rectangle rect, string filePath)
                    SoundAnnotation soundAnno = new SoundAnnotation(page, rect, audioFiles[i - 1]);

                    // Optional: set a title that appears in the annotation popup
                    soundAnno.Title = $"Audio for page {i}";

                    // Add the annotation to the page's annotation collection
                    page.Annotations.Add(soundAnno);
                }

                // Save the modified PDF (PDF format by default)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Sound annotations added. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}