using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateHeaders
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\PdfFolder";
        // Folder where rotated PDFs will be saved
        const string outputFolder = @"C:\PdfFolder\Rotated";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName + "_rotated.pdf");

            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPath))
            {
                // Absorb all text fragments in the document
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                doc.Pages.Accept(absorber);

                // Rotate fragments that look like headers (e.g., larger font size)
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Simple heuristic: treat font size >= 14 as a header
                    if (fragment.TextState != null && fragment.TextState.FontSize >= 14)
                    {
                        // Rotate the text fragment by 90 degrees
                        fragment.TextState.Rotation = 90;
                    }
                }

                // Save the modified document (lifecycle rule: save inside using)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} -> {Path.GetFileName(outputPath)}");
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}