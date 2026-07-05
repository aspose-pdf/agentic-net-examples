using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing input PDFs
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_bates.pdf");

            try
            {
                // Load the PDF document (lifecycle rule: use using for disposal)
                using (Document doc = new Document(inputPath))
                {
                    // Create a Bates numbering artifact with the desired defaults
                    BatesNArtifact bates = new BatesNArtifact
                    {
                        StartNumber = 1,          // will be overridden per page below
                        NumberOfDigits = 6
                    };

                    // Add the artifact to the document – it will be copied to every page
                    doc.Pages.AddBatesNumbering(bates);

                    // Adjust the start number for each page so that the Bates number
                    // increments by 5 (1, 6, 11, 16, ...).
                    int pageIndex = 0; // zero‑based index
                    foreach (Page page in doc.Pages)
                    {
                        foreach (var art in page.Artifacts)
                        {
                            if (art is BatesNArtifact bn)
                            {
                                bn.StartNumber = pageIndex * 5 + 1;
                            }
                        }
                        pageIndex++;
                    }

                    // Save the modified PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
