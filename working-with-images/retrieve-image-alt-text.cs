using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "image_alt_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Open a log file (append mode) inside a using block
                using (StreamWriter logWriter = new StreamWriter(logPath, append: true))
                {
                    int imageIndex = 0;

                    // Iterate over all pages (1‑based indexing)
                    foreach (Page page in doc.Pages)
                    {
                        // Iterate over the image collection (not a dictionary)
                        foreach (XImage img in page.Resources.Images)
                        {
                            imageIndex++;
                            // Retrieve the alternative text via the API that returns a List<string>
                            List<string> altTexts = img.GetAlternativeText(page);
                            string altText = (altTexts != null && altTexts.Count > 0)
                                ? string.Join(", ", altTexts)
                                : "(no alt text)";

                            string logEntry = $"Page {page.Number}, Image {imageIndex}: AltText = \"{altText}\"";
                            Console.WriteLine(logEntry);
                            logWriter.WriteLine(logEntry);
                        }
                    }

                    if (imageIndex == 0)
                    {
                        string msg = "No images found in the document.";
                        Console.WriteLine(msg);
                        logWriter.WriteLine(msg);
                    }
                }
            }

            Console.WriteLine($"Alt‑text extraction completed. Log saved to '{logPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
