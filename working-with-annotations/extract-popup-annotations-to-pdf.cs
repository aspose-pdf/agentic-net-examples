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
        const string outputPath = "popup_extracted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (deterministic disposal)
        using (Document srcDoc = new Document(inputPath))
        {
            bool popupFound = false;

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= srcDoc.Pages.Count && !popupFound; i++)
            {
                Page page = srcDoc.Pages[i];

                // Iterate all annotations on the page
                foreach (Annotation annot in page.Annotations)
                {
                    // Identify a PopupAnnotation
                    if (annot is PopupAnnotation popupAnnot)
                    {
                        // The popup text is stored in the parent markup annotation's Contents property
                        string popupText = popupAnnot.Parent?.Contents ?? string.Empty;

                        if (!string.IsNullOrEmpty(popupText))
                        {
                            // Create a new PDF document for the extracted popup
                            using (Document popupDoc = new Document())
                            {
                                // Add a blank page to host the popup text
                                Page newPage = popupDoc.Pages.Add();

                                // Insert the popup text as a TextFragment
                                TextFragment tf = new TextFragment(popupText)
                                {
                                    // Optional formatting – adjust as needed
                                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Helvetica") }
                                };
                                newPage.Paragraphs.Add(tf);

                                // Save the new PDF
                                popupDoc.Save(outputPath);
                            }

                            Console.WriteLine($"Popup annotation saved to '{outputPath}'.");
                            popupFound = true;
                            break; // Exit inner foreach – we already processed a popup
                        }
                    }
                }
            }

            if (!popupFound)
            {
                Console.WriteLine("No popup annotation with content found in the document.");
            }
        }
    }
}
