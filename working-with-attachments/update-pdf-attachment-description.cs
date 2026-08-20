using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF with attachment
        const string outputPath = "output.pdf";         // PDF after updating description
        const string newDescription = "Updated attachment description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            FileAttachmentAnnotation targetAttachment = null;

            // Search for the first FileAttachmentAnnotation in the document
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is FileAttachmentAnnotation fileAnn)
                    {
                        targetAttachment = fileAnn;
                        break;
                    }
                }
                if (targetAttachment != null) break;
            }

            if (targetAttachment == null)
            {
                Console.Error.WriteLine("No file attachment annotation found in the document.");
                return;
            }

            // Update the description of the attached file
            if (targetAttachment.File != null)
            {
                targetAttachment.File.Description = newDescription;
            }
            else
            {
                Console.Error.WriteLine("The attachment does not have an associated FileSpecification.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment description updated and saved to '{outputPath}'.");
    }
}