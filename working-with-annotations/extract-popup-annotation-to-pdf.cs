using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "popup_extracted.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= srcDoc.Pages.Count; pageIdx++)
            {
                Page page = srcDoc.Pages[pageIdx];

                // Iterate through annotations on the page
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Check if the annotation is a PopupAnnotation
                    if (ann is PopupAnnotation popup)
                    {
                        // Retrieve the popup's text content
                        string popupText = popup.Contents ?? string.Empty;

                        // Create a new PDF to hold the popup content
                        using (Document newDoc = new Document())
                        {
                            // Add a blank page
                            Page newPage = newDoc.Pages.Add();

                            // Add the popup text as a TextFragment
                            TextFragment tf = new TextFragment(popupText)
                            {
                                // Position the text near the top-left of the page
                                Position = new Position(50, 800)
                            };
                            newPage.Paragraphs.Add(tf);

                            // Save the new PDF
                            newDoc.Save(outputPdfPath);
                        }

                        Console.WriteLine($"Popup annotation extracted and saved to '{outputPdfPath}'.");
                        return; // Assuming only one popup is needed
                    }
                }
            }

            Console.WriteLine("No PopupAnnotation found in the document.");
        }
    }
}