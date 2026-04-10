using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextFragment, FontRepository, etc.

public class Program
{
    public static void Main(string[] args)
    {
        // Expected usage: <outputDirectory> <pdfPath1> [pdfPath2] ...
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <outputDirectory> <pdfPath1> [pdfPath2] ...");
            return;
        }

        string outputDirectory = args[0];
        string[] pdfPaths = args[1..]; // C# 8 range operator
        PdfTitleRotator.RotateTitleText(pdfPaths, outputDirectory);
    }
}

public static class PdfTitleRotator
{
    /// <summary>
    /// Rotates the document title text by 45 degrees and saves each PDF to the specified output directory.
    /// </summary>
    /// <param name="pdfPaths">Array of input PDF file paths.</param>
    /// <param name="outputDirectory">Directory where rotated PDFs will be saved.</param>
    public static void RotateTitleText(string[] pdfPaths, string outputDirectory)
    {
        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in pdfPaths)
        {
            if (!File.Exists(inputPath))
                continue; // Skip missing files

            // Build output file name
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileNameWithoutExt}_rotated.pdf");

            // Load the PDF document (deterministic disposal via using)
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the document title (metadata). Fallback to a default if not set.
                string titleText = doc.Info.Title ?? "Title";

                // Create a TextFragment that will display the title rotated by 45 degrees.
                TextFragment titleFragment = new TextFragment(titleText)
                {
                    // Set visual appearance
                    TextState = {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 24,
                        ForegroundColor = Color.Blue,
                        Rotation = 45 // Rotation in degrees
                    },
                    // Position the fragment near the top of the page (adjust as needed)
                    Position = new Position(100, 700)
                };

                // Add the rotated title fragment to every page in the document.
                foreach (Page page in doc.Pages)
                {
                    page.Paragraphs.Add(titleFragment);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }
    }
}
