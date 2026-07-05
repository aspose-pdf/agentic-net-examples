using System;
using System.IO;
using Aspose.Pdf; // Document, Page, Rotation

class BatchRotateFirstPage
{
    static void Main()
    {
        // Folder containing input PDF files
        const string inputFolder = "input_pdfs";
        // Folder where rotated PDFs will be saved
        const string outputFolder = "rotated_pdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Please create it and place PDF files inside before running the program.");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string inputFile in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file path (adds "_rotated" suffix)
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFile);
            string outputFile = Path.Combine(outputFolder, fileNameWithoutExt + "_rotated.pdf");

            // Load the PDF document
            Document doc = new Document(inputFile);

            // Ensure the document has at least one page
            if (doc.Pages.Count > 0)
            {
                // Rotate the first page by 90 degrees clockwise
                // Use the Page.Rotate property with the Rotation enum
                doc.Pages[1].Rotate = Rotation.on90;
            }
            else
            {
                Console.WriteLine($"Warning: '{Path.GetFileName(inputFile)}' contains no pages and will be copied without changes.");
            }

            // Save the modified PDF
            doc.Save(outputFile);

            Console.WriteLine($"Rotated first page of '{Path.GetFileName(inputFile)}' -> '{Path.GetFileName(outputFile)}'");
        }

        Console.WriteLine("Batch rotation completed.");
    }
}
