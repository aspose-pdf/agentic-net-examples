using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAddRedRectangle
{
    static void Main()
    {
        const string inputFolder = "Input";
        const string outputFolder = "Output";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input directory exists; if not, create it and exit gracefully
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist. Creating it now. Place PDF files there and re‑run the program.");
            Directory.CreateDirectory(inputFolder);
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Nothing to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                Document pdfDocument = new Document(pdfPath);

                // Define the rectangle (lower‑left X,Y and upper‑right X,Y)
                // Example: rectangle from (100,100) to (300,200)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

                // Create a square (rectangle) annotation on the first page
                SquareAnnotation square = new SquareAnnotation(pdfDocument.Pages[1], rect)
                {
                    Color = Aspose.Pdf.Color.Red,   // Border color
                    Opacity = 0.5                    // Semi‑transparent
                };

                // Add the annotation to the page
                pdfDocument.Pages[1].Annotations.Add(square);

                // Save the annotated PDF to the output folder, preserving the original name
                string fileName = System.IO.Path.GetFileName(pdfPath);
                string outPath = System.IO.Path.Combine(outputFolder, fileName);
                pdfDocument.Save(outPath);

                Console.WriteLine($"Annotated '{fileName}' saved to '{outputFolder}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
