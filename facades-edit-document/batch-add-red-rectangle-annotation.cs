using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // retained for potential drawing needs

class Program
{
    static void Main()
    {
        const string inputFolder = "Input";
        const string outputFolder = "Output";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder '{inputFolder}' does not exist.");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (var filePath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                Document pdfDoc = new Document(filePath);

                // Define the rectangle for the annotation (lower‑left X,Y and upper‑right X,Y)
                // Adjust these coordinates as needed
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 300);

                // Create a square (rectangle) annotation on the first page
                SquareAnnotation annotation = new SquareAnnotation(pdfDoc.Pages[1], rect);
                // Set border color via the annotation's Color property (Border has no Color property)
                annotation.Color = Aspose.Pdf.Color.Red;
                // Set border width (Border requires the parent annotation in its constructor)
                annotation.Border = new Border(annotation) { Width = 2 };

                // Add the annotation to the page
                pdfDoc.Pages[1].Annotations.Add(annotation);

                // Save the modified PDF to the output folder, preserving the original file name
                string outPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(filePath));
                pdfDoc.Save(outPath);

                Console.WriteLine($"Processed: {System.IO.Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{filePath}': {ex.Message}");
            }
        }
    }
}