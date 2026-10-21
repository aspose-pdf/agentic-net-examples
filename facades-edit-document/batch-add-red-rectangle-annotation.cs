using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "Input";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Get all PDF files in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document
                Document doc = new Document(pdfPath);

                // Define the rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a square (rectangular) annotation on the first page
                SquareAnnotation squareAnn = new SquareAnnotation(doc.Pages[1], rect)
                {
                    // Set the border color to red (annotation color is the border color)
                    Color = Aspose.Pdf.Color.Red
                };

                // Set the border width – Border requires the owning annotation in its constructor
                squareAnn.Border = new Border(squareAnn)
                {
                    Width = 1
                };

                // Add the annotation to the page's annotation collection
                doc.Pages[1].Annotations.Add(squareAnn);

                // Build the output file name (preserves the original)
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(pdfPath) + "_annotated.pdf");

                // Save the annotated PDF
                doc.Save(outputPath);

                Console.WriteLine($"Annotated PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
