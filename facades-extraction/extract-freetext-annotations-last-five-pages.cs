using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "LastFiveFreeText.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);
            int totalPages = pdfDocument.Pages.Count;

            // Determine the range for the last five pages (or fewer if the document is short)
            int startPage = Math.Max(1, totalPages - 4);

            StringBuilder sb = new StringBuilder();

            // Iterate through the selected page range and collect FreeText annotation contents
            for (int pageNumber = startPage; pageNumber <= totalPages; pageNumber++)
            {
                var page = pdfDocument.Pages[pageNumber];
                foreach (var annotation in page.Annotations)
                {
                    if (annotation is FreeTextAnnotation freeText && !string.IsNullOrEmpty(freeText.Contents))
                    {
                        sb.AppendLine(freeText.Contents);
                    }
                }
            }

            // Write the collected annotation texts to the output file
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);

            Console.WriteLine($"FreeText annotations from the last five pages saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
