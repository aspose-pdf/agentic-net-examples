using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\PdfFolder";
        // Folder where processed PDFs will be saved
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
                // Open the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Absorb all text fragments in the document
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    // In recent Aspose.Pdf versions the Accept method is on the Pages collection
                    doc.Pages.Accept(absorber);

                    // Define a simple heuristic for "header" fragments:
                    // treat fragments with a font size larger than 12 as headers.
                    const float headerFontSizeThreshold = 12f;

                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // Check if the fragment meets the header criteria
                        if (fragment.TextState.FontSize > headerFontSizeThreshold)
                        {
                            // Rotate the header text by 90 degrees
                            fragment.TextState.Rotation = 90f;
                        }
                    }

                    // Save the modified document with a new name
                    string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_rotated.pdf");
                    doc.Save(outputPath);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
