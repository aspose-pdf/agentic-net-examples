using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF on disk
        const string outputPath = "output.pdf";  // PDF with imported annotations
        const string xfdfString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xfdf xmlns=\"\" xmlns:xfa=\"http://www.xfa.org/schema/xfa-data/1.0/\" >\n  <annots>\n    <highlight page=\"1\" rect=\"100,500,200,520\" color=\"255,255,0\" />\n  </annots>\n</xfdf>"; // example XFDF content; replace with your actual XFDF string

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // The core API works with a file path or a stream. Write the XFDF string to a temporary file.
                string tempXfdfPath = Path.GetTempFileName();
                File.WriteAllText(tempXfdfPath, xfdfString);

                // Import the annotation(s) from the temporary XFDF file.
                pdfDoc.ImportAnnotationsFromXfdf(tempXfdfPath);

                // Clean up the temporary file.
                File.Delete(tempXfdfPath);

                // Save the modified PDF.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during annotation import: {ex.Message}");
        }
    }
}
