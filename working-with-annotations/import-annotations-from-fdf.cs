using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Paths that are known only at runtime must be static readonly, not const
    private static readonly string fdfPath = Path.Combine("Resources", "annotations.fdf");

    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document (deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF file stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations from the FDF into the PDF document
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Example processing: count total annotations after import
            Console.WriteLine($"Total annotations after import: {CountAnnotations(doc)}");

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }

    // Helper to count all annotations across all pages
    static int CountAnnotations(Document doc)
    {
        int total = 0;
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            total += doc.Pages[i].Annotations.Count;
        }
        return total;
    }
}
