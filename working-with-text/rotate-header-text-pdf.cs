using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateHeaders
{
    static void Main()
    {
        // Folder containing input PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file path (append "_rotated" before extension)
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_rotated.pdf");

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over each page and its paragraphs – avoids the
                // TextFragmentAbsorber which may trigger an internal helper exe.
                foreach (Page page in doc.Pages)
                {
                    foreach (var paragraph in page.Paragraphs)
                    {
                        if (paragraph is TextFragment fragment)
                        {
                            // Simple heuristic: treat fragments with a relatively large font size as headers
                            if (fragment.TextState.FontSize >= 14)
                            {
                                // Rotate the fragment by 90 degrees clockwise
                                fragment.TextState.Rotation = 90;
                            }
                        }
                    }
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {Path.GetFileName(outputPath)}");
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}
