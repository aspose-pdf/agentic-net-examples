using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // List of PDF files to process (adjust the names as needed)
        string[] inputFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf" };

        // Process each file in parallel
        Parallel.ForEach(inputFiles, (string inputPath) =>
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Create a simple output file name (no directory path as required)
            string outputFileName = "processed_" + Path.GetFileName(inputPath);

            // Load, edit, and save the PDF using the recommended lifecycle pattern
            using (Document pdfDoc = new Document(inputPath))
            {
                // Example edit: add a text fragment to the first page
                Page firstPage = pdfDoc.Pages[1];
                TextFragment tf = new TextFragment("Processed");
                tf.TextState.FontSize = 24;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                firstPage.Paragraphs.Add(tf);

                // Save the modified PDF (simple filename only)
                pdfDoc.Save(outputFileName);
                Console.WriteLine($"Saved {outputFileName}");
            }
        });
    }
}