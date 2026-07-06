using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing input PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Absorb all text fragments on the current page
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    page.Accept(absorber);

                    // Examine each fragment and rotate those that look like headers
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // Simple heuristic: treat larger font sizes as headers
                        if (fragment.TextState.FontSize >= 12) // adjust threshold as needed
                        {
                            // Rotate the fragment by 90 degrees
                            fragment.TextState.Rotation = 90;
                        }
                    }
                }

                // Save the modified document to the output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }
    }
}