using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Iterate through all annotations on the page (1‑based indexing)
                    for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                    {
                        Annotation ann = page.Annotations[annIdx];

                        // Check if the annotation is a StampAnnotation
                        if (ann is StampAnnotation stamp)
                        {
                            // Extract the text content of the stamp annotation
                            string stampText = stamp.Contents ?? string.Empty;
                            Console.WriteLine($"Page {pageNum}, Stamp {annIdx}: \"{stampText}\"");
                        }
                    }
                }

                // The PDF format is preserved; no modifications are saved.
                // If a copy is needed, uncomment the line below:
                // doc.Save("preserved_output.pdf");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}