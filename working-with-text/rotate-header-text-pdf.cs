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
        // Folder to place the rotated PDFs (can be the same as inputFolder)
        const string outputFolder = @"C:\PdfFolder\Rotated";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Absorb all text fragments in the document
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    doc.Pages.Accept(absorber);

                    // Define a font size threshold that roughly distinguishes headers
                    const double headerFontSizeThreshold = 12.0;

                    // Rotate each fragment that is considered a header
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // Check the font size; larger sizes are treated as headers
                        if (fragment.TextState.FontSize > headerFontSizeThreshold)
                        {
                            // Rotate the text fragment by 90 degrees
                            fragment.TextState.Rotation = 90;
                        }
                    }

                    // Build output file name
                    string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                    string outPath = Path.Combine(outputFolder, $"{fileName}_rotated.pdf");

                    // Save the modified PDF
                    doc.Save(outPath);
                }

                Console.WriteLine($"Processed and saved: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All files processed.");
    }
}