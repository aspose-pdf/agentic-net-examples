using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where rotated PDFs will be saved
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_rotated.pdf");

            try
            {
                // Load the PDF document (using statement ensures proper disposal)
                using (Document doc = new Document(pdfPath))
                {
                    // Absorb all text fragments from the whole document
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    doc.Pages.Accept(absorber);

                    // Rotate fragments that are likely headers.
                    // Simple heuristic: treat fragments with a font size larger than 12 as headers.
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        if (fragment.TextState.FontSize > 12)
                        {
                            // Rotate the text fragment by 90 degrees.
                            // TextState.Rotation is the standard way to set rotation for a fragment.
                            fragment.TextState.Rotation = 90;
                        }
                    }

                    // Save the modified document as PDF.
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}