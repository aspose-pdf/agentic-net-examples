using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 7 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("Document does not contain page 7.");
                return;
            }

            // Get page 7
            Page page = doc.Pages[7];

            // Find the first LinkAnnotation on the page
            LinkAnnotation linkAnno = null;
            foreach (Annotation anno in page.Annotations)
            {
                if (anno is LinkAnnotation la)
                {
                    linkAnno = la;
                    break;
                }
            }

            if (linkAnno == null)
            {
                Console.Error.WriteLine("No LinkAnnotation found on page 7.");
                return;
            }

            // Set the annotation color to blue using Aspose.Pdf.Color (cross‑platform)
            linkAnno.Color = Aspose.Pdf.Color.Blue;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}