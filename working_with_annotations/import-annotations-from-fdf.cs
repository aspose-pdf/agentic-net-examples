using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the FDF file located in the Resources folder
        string pdfPath = Path.Combine("Resources", "sample.pdf");
        string fdfPath = Path.Combine("Resources", "annotations.fdf");
        string outputPath = "output.pdf";

        // Verify that both files exist before proceeding
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF file as a stream and import its annotations into the document
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // OPTIONAL: process the imported annotations (e.g., list them on the first page)
            if (doc.Pages.Count > 0)
            {
                Page firstPage = doc.Pages[1]; // Pages are 1‑based
                for (int i = 1; i <= firstPage.Annotations.Count; i++)
                {
                    Annotation ann = firstPage.Annotations[i];
                    Console.WriteLine($"Annotation {i}: {ann.GetType().Name}");
                }
            }

            // Save the modified PDF to the desired output location
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}