using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // Remove all annotations from each page
                foreach (Page page in doc.Pages)
                {
                    // The correct method to clear annotations is Clear(), not Delete()
                    page.Annotations.Clear();
                }

                // Save the cleaned PDF (save-to-non-pdf rule: Save() writes PDF)
                doc.Save(outputPath);
            }

            Console.WriteLine($"All annotations removed. Clean PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
