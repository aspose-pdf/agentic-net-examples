using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the PDF to create and a sample attachment file
        const string pdfPath = "unicode_attachment.pdf";
        const string attachmentPath = "sample.txt";

        // Create a simple text file that will be attached to the PDF
        File.WriteAllText(attachmentPath, "Sample attachment content");

        // Unicode name to be stored for the attachment (e.g., Chinese characters)
        const string unicodeName = "文件附件.txt";

        // -------------------------------------------------
        // Create a new PDF document and add the attachment
        // -------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page (optional, ensures the PDF has at least one page)
            doc.Pages.Add();

            // Prepare the file specification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
            {
                // Unicode name – this is the value we want to verify later
                UnicodeName = unicodeName
            };
            // Set the file data (required when using EmbeddedFiles)
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

            // Add the attachment to the document via the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF
            doc.Save(pdfPath);
        }

        // -------------------------------------------------
        // Load the PDF and verify that the Unicode name is stored
        // -------------------------------------------------
        using (Document loadedDoc = new Document(pdfPath))
        {
            foreach (FileSpecification attachment in loadedDoc.EmbeddedFiles)
            {
                // The Name property returns the stored (ASCII) name.
                // When UnicodeName is set, it is stored separately and can be read back.
                Console.WriteLine("Attachment Name       : " + attachment.Name);
                Console.WriteLine("Attachment UnicodeName: " + attachment.UnicodeName);
            }
        }
    }
}
