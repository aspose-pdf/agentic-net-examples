using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string outputNamePath = "attachment_name.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment and set its description
            // The constructor (path, description) automatically sets the Name to the file name.
            FileSpecification fileSpec = new FileSpecification(attachmentFilePath, "Sample attachment description");

            // Add the file specification to the EmbeddedFiles collection.
            // The collection stores FileSpecification objects; the logical name is taken from fileSpec.Name.
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF that now contains the attachment
            doc.Save(outputPdfPath);

            // Retrieve the name of the first embedded file (if any)
            string retrievedName = null;
            if (doc.EmbeddedFiles.Count > 0)
            {
                // Each entry is a FileSpecification; its Name property holds the logical name.
                FileSpecification firstSpec = doc.EmbeddedFiles[0];
                retrievedName = firstSpec.Name;
            }

            // Write the retrieved name to a text file
            if (retrievedName != null)
            {
                File.WriteAllText(outputNamePath, retrievedName);
                Console.WriteLine($"Embedded file name saved to '{outputNamePath}'.");
            }
            else
            {
                Console.WriteLine("No embedded files found in the PDF.");
            }
        }
    }
}
