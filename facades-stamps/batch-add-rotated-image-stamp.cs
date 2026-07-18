using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: BatchStamp <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Path to the image that will be used as a stamp.
        const string stampImagePath = "stamp.png";

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Process each PDF file in the input folder.
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Initialize the facade.
                PdfFileStamp fileStamp = new PdfFileStamp();
                fileStamp.InputFile = pdfPath;
                fileStamp.OutputFile = outputPath;

                // Create a stamp, bind the image, set its position, size and rotation.
                Stamp stamp = new Stamp();
                stamp.BindImage(stampImagePath);
                stamp.SetOrigin(100, 500);          // Position (X, Y) from the lower‑left corner.
                stamp.SetImageSize(150, 150);       // Width and height of the image.
                stamp.Rotation = 45;                // Rotate 45 degrees.
                stamp.Pages = new int[] { 1 };      // Apply only to the first page.

                // Add the stamp to the document and finalize.
                fileStamp.AddStamp(stamp);
                fileStamp.Close();

                Console.WriteLine($"Stamped: {fileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}