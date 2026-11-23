using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "sound_annotations_page5.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                if (doc.Pages.Count < 5)
                {
                    Console.Error.WriteLine("The document has fewer than 5 pages.");
                    return;
                }

                Page page5 = doc.Pages[5];

                // Open the CSV file for writing
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    // Write a simple header
                    writer.WriteLine("Title");

                    // Iterate over all annotations on page 5
                    foreach (Annotation annotation in page5.Annotations)
                    {
                        // Filter only SoundAnnotation objects
                        if (annotation is SoundAnnotation soundAnno)
                        {
                            // Write the title (empty string if null) to the CSV
                            string title = soundAnno.Title ?? string.Empty;
                            writer.WriteLine(title);
                        }
                    }
                }

                Console.WriteLine($"Sound annotation titles from page 5 have been written to '{csvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}