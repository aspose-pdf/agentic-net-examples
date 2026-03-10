using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PCL file (PCL is supported only as input)
        const string pclPath = "input.pcl";
        // Output PDF files
        const string pdfWithAnnotation = "output_with_annotation.pdf";
        const string pdfAfterDeletion   = "output_after_deletion.pdf";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"File not found: {pclPath}");
            return;
        }

        // Load the PCL file using PclLoadOptions
        using (Document doc = new Document(pclPath, new PclLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // 1. Add a TextAnnotation to the page
            // -------------------------------------------------
            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Sample Note",
                Contents = "This is a text annotation added to a PCL‑derived PDF.",
                Color    = Aspose.Pdf.Color.Yellow,
                // Assign a name so we can retrieve it later
                Name     = "MyNote"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnn);

            // Save the document (PDF output) – extension alone does not change format,
            // so we explicitly save as PDF.
            doc.Save(pdfWithAnnotation);
            Console.WriteLine($"Document with annotation saved to '{pdfWithAnnotation}'.");

            // -------------------------------------------------
            // 2. Get (retrieve) the annotation by its name
            // -------------------------------------------------
            Annotation retrieved = page.Annotations.FindByName("MyNote");
            if (retrieved is TextAnnotation retrievedText)
            {
                Console.WriteLine("Retrieved annotation:");
                Console.WriteLine($"  Title   : {retrievedText.Title}");
                Console.WriteLine($"  Contents: {retrievedText.Contents}");
                Console.WriteLine($"  Color   : {retrievedText.Color}");
            }
            else if (retrieved != null)
            {
                // Fallback for other annotation types
                Console.WriteLine("Retrieved annotation is not a TextAnnotation.");
            }
            else
            {
                Console.WriteLine("Annotation with name 'MyNote' not found.");
            }

            // -------------------------------------------------
            // 3. Delete the annotation
            // -------------------------------------------------
            // Deletion can be performed by passing the annotation instance
            // or by its 1‑based index. Here we use the instance.
            if (retrieved != null)
            {
                page.Annotations.Delete(retrieved);
                Console.WriteLine("Annotation deleted.");
            }

            // Save the document after deletion
            doc.Save(pdfAfterDeletion);
            Console.WriteLine($"Document after deletion saved to '{pdfAfterDeletion}'.");
        }
    }
}
