using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing input PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Define a font size threshold that distinguishes headers.
        // Adjust this value based on the PDFs you process.
        const float headerFontSizeThreshold = 14f;

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Absorb all text fragments from the whole document
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    doc.Pages.Accept(absorber);

                    // Rotate fragments that are likely headers
                    foreach (TextFragment fragment in absorber.TextFragments)
                    {
                        // Simple heuristic: treat larger font size as a header
                        if (fragment.TextState.FontSize >= headerFontSizeThreshold)
                        {
                            // Rotate the text fragment by 90 degrees
                            fragment.TextState.Rotation = 90;
                        }
                    }

                    // Save the modified document to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All files processed.");
    }
}