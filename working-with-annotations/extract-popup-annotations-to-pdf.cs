using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPath))
        {
            int popupIndex = 0;

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
            {
                Page page = srcDoc.Pages[pageNum];

                // Iterate through all annotations on the page (1‑based indexing)
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Check if the annotation is a PopupAnnotation
                    if (ann is PopupAnnotation popup)
                    {
                        popupIndex++;

                        // Create a new PDF document to hold the popup content
                        using (Document popupDoc = new Document())
                        {
                            // Add a blank page
                            Page newPage = popupDoc.Pages.Add();

                            // Retrieve the popup's text (Contents) and add it as a TextFragment
                            string popupText = popup.Contents ?? string.Empty;
                            TextFragment tf = new TextFragment(popupText);
                            newPage.Paragraphs.Add(tf);

                            // Save the new PDF (one file per popup annotation)
                            string outputPath = $"popup_{popupIndex}.pdf";
                            popupDoc.Save(outputPath);
                            Console.WriteLine($"Saved popup annotation #{popupIndex} to '{outputPath}'.");
                        }
                    }
                }
            }

            if (popupIndex == 0)
                Console.WriteLine("No popup annotations found in the document.");
        }
    }
}