using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the generated PDF and the temporary attachment file
        const string pdfPath = "output.pdf";
        const string attachmentPath = "sample.txt";

        // Description containing a variety of special characters
        const string description = "Special chars: äöüß € © ™ ∑ ∞";

        // -----------------------------------------------------------------
        // Step 1: Create a temporary attachment file
        // -----------------------------------------------------------------
        File.WriteAllText(attachmentPath, "This is a sample attachment.");

        // -----------------------------------------------------------------
        // Step 2: Create a new PDF, add a page, and embed the file with
        //         the special‑character description using EmbeddedFiles
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a blank page (required before adding embedded files)
            doc.Pages.Add();

            // Create a FileSpecification for the attachment
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), description)
            {
                // Load the file data into the specification
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };

            // Add the file specification to the PDF's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF
            doc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // Step 3: Reload the PDF and verify that the description was saved
        // -----------------------------------------------------------------
        using (Document loadedDoc = new Document(pdfPath))
        {
            // There should be at least one embedded file
            if (loadedDoc.EmbeddedFiles == null || loadedDoc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found.");
                return;
            }

            // Retrieve the first embedded file (1‑based indexing)
            FileSpecification loadedSpec = loadedDoc.EmbeddedFiles[1];

            // Compare the stored description with the original
            bool descriptionMatches = string.Equals(loadedSpec.Description, description, StringComparison.Ordinal);

            Console.WriteLine($"Original description : {description}");
            Console.WriteLine($"Extracted description: {loadedSpec.Description}");
            Console.WriteLine($"Integrity check      : {(descriptionMatches ? "PASS" : "FAIL")}");
        }

        // -----------------------------------------------------------------
        // Cleanup temporary files (optional)
        // -----------------------------------------------------------------
        try { File.Delete(attachmentPath); } catch { /* ignore */ }
    }
}
