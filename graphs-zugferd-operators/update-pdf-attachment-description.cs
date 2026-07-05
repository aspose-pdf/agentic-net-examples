using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newDescription = "Updated attachment description - version 2";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            bool updated = false;

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];

                // Annotations collection is also 1‑based
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Look for file attachment annotations
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        // Update the description of the attached file
                        fileAnn.File.Description = newDescription;
                        updated = true;
                    }
                }
            }

            if (!updated)
            {
                Console.WriteLine("No FileAttachmentAnnotation found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment description updated and saved to '{outputPath}'.");
    }
}