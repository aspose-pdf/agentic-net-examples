using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF that contains the popup annotation
        const string outputPdf = "popup_extracted.pdf"; // PDF that will hold the extracted content

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source document (lifecycle: load)
        using (Document srcDoc = new Document(inputPdf))
        {
            // Find the first PopupAnnotation in the document
            PopupAnnotation popup = null;
            foreach (Page page in srcDoc.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++) // annotation collections are 1‑based
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is PopupAnnotation pa)
                    {
                        popup = pa;
                        break;
                    }
                }
                if (popup != null) break;
            }

            if (popup == null)
            {
                Console.WriteLine("No popup annotation found.");
                return;
            }

            // Retrieve the textual content of the popup annotation
            string popupText = popup.Contents ?? string.Empty;

            // Create a new PDF document to store the extracted content (lifecycle: create)
            using (Document destDoc = new Document())
            {
                // Add a blank page
                Page newPage = destDoc.Pages.Add();

                // Add the popup text as a TextFragment
                TextFragment tf = new TextFragment(popupText);
                tf.Position = new Position(50, 750); // place near top‑left
                newPage.Paragraphs.Add(tf);

                // Save the new PDF (lifecycle: save)
                destDoc.Save(outputPdf);
            }

            Console.WriteLine($"Popup annotation content saved to '{outputPdf}'.");
        }
    }
}