using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of input PDF files to process
        string[] inputFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };
        const string outputFile = "merged_resized.pdf";

        // Create the target document that will hold all resized pages
        using (Document targetDoc = new Document())
        {
            foreach (string filePath in inputFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                // Load each source PDF
                using (Document sourceDoc = new Document(filePath))
                {
                    // Resize every page to 1024x768 points
                    for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                    {
                        Page page = sourceDoc.Pages[i];
                        page.SetPageSize(1024, 768);
                    }

                    // Append the resized pages to the target document
                    targetDoc.Pages.Add(sourceDoc.Pages);
                }
            }

            // Save the concatenated PDF
            targetDoc.Save(outputFile);
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}