using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Approximate reading speed: 200 words per minute => 0.3 seconds per word
            const double secondsPerWord = 60.0 / 200.0;

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                TextAbsorber absorber = new TextAbsorber();
                page.Accept(absorber);
                string pageText = absorber.Text ?? String.Empty;
                int wordCount = 0;
                if (!String.IsNullOrWhiteSpace(pageText))
                {
                    string[] words = pageText.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    wordCount = words.Length;
                }

                double duration = wordCount * secondsPerWord;
                if (duration < 1.0)
                {
                    duration = 1.0; // minimum display time
                }
                page.Duration = duration;
                Console.WriteLine($"Page {i}: {wordCount} words, duration set to {duration:F1} seconds.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}