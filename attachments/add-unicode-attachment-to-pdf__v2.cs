using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the temporary attachment file, and the output PDF
        const string sourcePdfPath = "source.pdf";
        const string attachmentFilePath = "temp.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Ensure the source PDF exists; create a minimal one if not
        if (!File.Exists(sourcePdfPath))
        {
            using (Document emptyDoc = new Document())
            {
                emptyDoc.Pages.Add(); // add a blank page
                emptyDoc.Save(sourcePdfPath);
            }
        }

        // Create a temporary file to be attached
        File.WriteAllText(attachmentFilePath, "Sample attachment content.");

        // Unicode file name to be stored inside the PDF
        const string unicodeFileName = "测试文档.txt";

        // Load the PDF, add the attachment (as an embedded file), and save
        using (Document doc = new Document(sourcePdfPath))
        {
            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(attachmentFilePath);
            // Override the name with the Unicode version
            fileSpec.Name = unicodeFileName;
            // Ensure the file data is stored in the specification (required for EmbeddedFiles)
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));

            // Add the specification to the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        // Re‑open the PDF and extract the attachment name to verify preservation
        using (Document doc = new Document(outputPdfPath))
        {
            // EmbeddedFiles collection is 1‑based; retrieve the first embedded file
            FileSpecification extractedSpec = doc.EmbeddedFiles[1];

            // Get the stored name (Unicode name should be preserved)
            string extractedName = extractedSpec.Name;

            // Verify that the Unicode name was preserved
            if (extractedName == unicodeFileName)
            {
                Console.WriteLine("Attachment name preserved: " + extractedName);
            }
            else
            {
                Console.WriteLine("Attachment name mismatch.");
                Console.WriteLine("Expected: " + unicodeFileName);
                Console.WriteLine("Actual:   " + extractedName);
            }
        }

        // Clean up the temporary attachment file
        if (File.Exists(attachmentFilePath))
        {
            File.Delete(attachmentFilePath);
        }
    }
}
