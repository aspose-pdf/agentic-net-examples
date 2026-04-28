using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string diffPath = "diff.pdf";

        // Verify input files exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs within using blocks for deterministic disposal
        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Instantiate the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Verify that the default highlight color matches the documentation (red)
            if (comparer.Color != Aspose.Pdf.Color.Red)
            {
                Console.WriteLine("Warning: Default highlight color is not red.");
            }
            else
            {
                Console.WriteLine("Default highlight color verified as red.");
            }

            // Generate the diff PDF; the method saves the result to the specified path
            comparer.CompareDocumentsToPdf(doc1, doc2, diffPath);
        }

        Console.WriteLine($"Diff PDF generated and saved to '{diffPath}'.");
    }
}