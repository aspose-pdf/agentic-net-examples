using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to compare
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Verify files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDFs from memory streams
        using (MemoryStream firstStream  = new MemoryStream(File.ReadAllBytes(firstPdfPath)))
        using (MemoryStream secondStream = new MemoryStream(File.ReadAllBytes(secondPdfPath)))
        using (Document firstDoc  = new Document(firstStream))
        using (Document secondDoc = new Document(secondStream))
        // Stream that will hold the side‑by‑side comparison result
        using (MemoryStream diffStream = new MemoryStream())
        {
            // Configure comparison options (default options are sufficient for a basic diff)
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform the comparison – SideBySidePdfComparer is a static class, so call the method directly
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, diffStream, options);

            // Reset the position of the output stream before reading or saving it
            diffStream.Position = 0;

            // Example: write the diff PDF to a file (optional – the stream can be used directly)
            const string diffPdfPath = "diff.pdf";
            File.WriteAllBytes(diffPdfPath, diffStream.ToArray());

            Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPdfPath}'.");
        }
    }
}