using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing popup annotation
        const string outputPdf = "popup_extracted.pdf"; // PDF that will contain the extracted popup

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Open the source document (lifecycle rule: wrap in using)
        using (Document srcDoc = new Document(inputPdf))
        {
            // Prepare a new PDF to hold the popup content
            using (Document popupDoc = new Document())
            {
                // Add a blank page to the new document
                Page newPage = popupDoc.Pages.Add();

                // Flag to indicate whether any popup was found
                bool popupFound = false;

                // Iterate over all pages (1‑based indexing)
                for (int pageIdx = 1; pageIdx <= srcDoc.Pages.Count; pageIdx++)
                {
                    Page srcPage = srcDoc.Pages[pageIdx];

                    // Iterate over annotations on the current page (1‑based)
                    for (int annIdx = 1; annIdx <= srcPage.Annotations.Count; annIdx++)
                    {
                        Annotation ann = srcPage.Annotations[annIdx];

                        // Identify PopupAnnotation instances
                        if (ann is PopupAnnotation popup)
                        {
                            popupFound = true;

                            // Retrieve the textual content of the popup
                            string popupText = popup.Contents ?? string.Empty;

                            // Create a TextFragment with the popup text
                            TextFragment tf = new TextFragment(popupText)
                            {
                                // Optional styling
                                TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
                            };

                            // Add the text fragment to the new page
                            newPage.Paragraphs.Add(tf);
                        }
                    }
                }

                if (!popupFound)
                {
                    Console.WriteLine("No popup annotations were found in the source PDF.");
                    return;
                }

                // Save the new PDF containing the extracted popup content
                popupDoc.Save(outputPdf);
                Console.WriteLine($"Popup annotation content saved to '{outputPdf}'.");
            }
        }
    }
}