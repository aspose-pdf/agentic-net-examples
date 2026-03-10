using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_texts.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            // Create a new PDF that will contain the extracted stamp texts
            using (Document outDoc = new Document())
            {
                // Add a single page to the output document
                Page outPage = outDoc.Pages.Add();

                // Iterate over all pages and their annotations
                foreach (Page srcPage in srcDoc.Pages)
                {
                    foreach (Annotation ann in srcPage.Annotations)
                    {
                        // We're interested only in rubber‑stamp annotations
                        if (ann is StampAnnotation stampAnn)
                        {
                            string stampText = stampAnn.Contents;
                            if (!string.IsNullOrEmpty(stampText))
                            {
                                // Add the stamp text as a paragraph in the output PDF
                                TextFragment tf = new TextFragment(stampText);
                                outPage.Paragraphs.Add(tf);
                            }
                        }
                    }
                }

                // Save the result as a PDF
                outDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Stamped text extracted to '{outputPath}'.");
    }
}