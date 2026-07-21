using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file name (original name with "_rotated" suffix)
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + "_rotated.pdf");

            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(pdfPath))
            {
                // Absorb all text fragments from the whole document
                TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                // Corrected: use the absorber to visit the document (Document.Accept is not available)
                absorber.Visit(doc);

                // Iterate over each extracted text fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Simple heuristic: treat fragments with a relatively large font size as headers
                    // Adjust the threshold (e.g., 12) as needed for your documents
                    if (fragment.TextState.FontSize > 12)
                    {
                        // Rotate the header text by 90 degrees
                        fragment.TextState.Rotation = 90;
                    }
                }

                // Save the modified document to the output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {Path.GetFileName(outputPath)}");
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}
