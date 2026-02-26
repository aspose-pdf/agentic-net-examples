using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath      = "input.cgm";      // CGM source file
        const string pdfPath      = "intermediate.pdf"; // PDF created from CGM
        const string searchPhrase = "Sample";        // Text to search for

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load CGM (input‑only format) and convert it to a PDF document.
        // -----------------------------------------------------------------
        CgmLoadOptions cgmLoadOpts = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, cgmLoadOpts))
        {
            // Save the intermediate PDF – this file will be used for annotation work.
            doc.Save(pdfPath);
        }

        // ---------------------------------------------------------------
        // 2. Search for a text fragment inside the generated PDF document.
        // ---------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Create an absorber for the desired phrase.
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchPhrase);
            // Search the whole document.
            pdfDoc.Pages.Accept(absorber);

            // Output the results.
            if (absorber.TextFragments.Count > 0)
            {
                Console.WriteLine($"Found {absorber.TextFragments.Count} occurrence(s) of \"{searchPhrase}\":");
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    Console.WriteLine($" - Page {fragment.Page.Number}, Text: \"{fragment.Text}\"");
                }
            }
            else
            {
                Console.WriteLine($"No occurrences of \"{searchPhrase}\" were found.");
            }

            // -----------------------------------------------------------
            // 3. Work with annotations using PdfAnnotationEditor (facade API).
            // -----------------------------------------------------------
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            // Bind the same PDF document to the editor.
            editor.BindPdf(pdfDoc);

            // Extract all text annotations from page 1 (you can adjust the range).
            string[] annotTypes = new[] { "Text" };
            IList<Annotation> annotations = editor.ExtractAnnotations(1, 1, annotTypes);

            if (annotations.Count > 0)
            {
                Console.WriteLine($"Found {annotations.Count} text annotation(s) on page 1:");
                foreach (Annotation annot in annotations)
                {
                    // Only TextAnnotation has Title and Contents properties.
                    if (annot is TextAnnotation textAnnot)
                    {
                        Console.WriteLine($" Title   : {textAnnot.Title}");
                        Console.WriteLine($" Contents: {textAnnot.Contents}");
                        Console.WriteLine($" Open    : {textAnnot.Open}");
                    }
                }

                // Example: modify the first text annotation (change title and contents).
                if (annotations[0] is TextAnnotation firstTextAnnot)
                {
                    firstTextAnnot.Title    = "Updated Title";
                    firstTextAnnot.Contents = "Updated annotation contents.";
                    firstTextAnnot.Open     = true;
                    // Apply the modification to the document (pages 1‑1).
                    editor.ModifyAnnotations(1, 1, firstTextAnnot);
                }
            }
            else
            {
                Console.WriteLine("No text annotations found on page 1.");
            }

            // Save the final PDF with possible annotation changes.
            const string outputPdf = "output.pdf";
            editor.Save(outputPdf);
            Console.WriteLine($"Final PDF saved to '{outputPdf}'.");
        }
    }
}