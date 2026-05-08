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
        const string outputPdf = "popup_extracted.pdf"; // PDF that will contain the extracted popup text

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source document (lifecycle rule: wrap in using)
        using (Document srcDoc = new Document(inputPdf))
        {
            // Variable to hold the first found popup annotation (if any)
            PopupAnnotation popup = null;

            // Iterate through all pages (1‑based indexing)
            for (int pageIdx = 1; pageIdx <= srcDoc.Pages.Count; pageIdx++)
            {
                Page page = srcDoc.Pages[pageIdx];

                // Iterate through annotations on the page (1‑based indexing)
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Check whether the annotation is a PopupAnnotation
                    if (ann is PopupAnnotation pa)
                    {
                        popup = pa;
                        break; // stop searching after the first popup is found
                    }
                }

                if (popup != null) break;
            }

            if (popup == null)
            {
                Console.WriteLine("No popup annotation found in the document.");
                return;
            }

            // Create a new PDF document to hold the extracted popup content
            using (Document outDoc = new Document())
            {
                // Add a blank page
                Page outPage = outDoc.Pages.Add();

                // Prepare the text to be saved – use the popup's Contents property
                string popupText = popup.Contents ?? string.Empty;

                // Create a TextFragment with the popup text
                TextFragment tf = new TextFragment(popupText)
                {
                    // Optional formatting
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
                };

                // Add the text fragment to the page's paragraphs collection
                outPage.Paragraphs.Add(tf);

                // Save the new PDF (lifecycle rule: use Document.Save)
                outDoc.Save(outputPdf);
            }

            Console.WriteLine($"Popup annotation extracted and saved to '{outputPdf}'.");
        }
    }
}