using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace UpdatePdfAttachmentDescription
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with a file attachment annotation
            using (Document createDoc = new Document())
            {
                // Add a blank page (evaluation mode allows up to 4 pages, we use only one)
                Page page = createDoc.Pages.Add();

                // Create a simple text file to attach
                File.WriteAllText("sample.txt", "This is version 1 of the attached file.");

                // Create a file specification and set initial description
                FileSpecification fileSpec = new FileSpecification("sample.txt");
                fileSpec.Description = "Version 1";

                // Define the rectangle where the annotation icon will appear
                Rectangle rect = new Rectangle(100, 600, 200, 700);

                // Create the file attachment annotation
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
                // Use the correct enum for the icon (FileIcon is the enum type)
                attachment.Icon = FileIcon.Paperclip;
                attachment.Contents = "Sample attachment";

                // Add the annotation to the page
                page.Annotations.Add(attachment);

                // Save the PDF
                createDoc.Save("input.pdf");
            }

            // Step 2: Open the PDF and update the attachment description
            using (Document doc = new Document("input.pdf"))
            {
                // Assuming the attachment is on the first page (1‑based indexing)
                Page firstPage = doc.Pages[1];

                // Iterate through annotations to find the file attachment
                foreach (Annotation ann in firstPage.Annotations)
                {
                    if (ann is FileAttachmentAnnotation)
                    {
                        FileAttachmentAnnotation fileAnn = (FileAttachmentAnnotation)ann;
                        // Update the description of the embedded file
                        fileAnn.File.Description = "Version 2 - Updated description";
                    }
                }

                // Save the updated PDF
                doc.Save("output.pdf");
            }
        }
    }
}
