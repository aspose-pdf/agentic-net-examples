using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a sample PDF with an embedded ZUGFeRD XML file
        using (Document createDoc = new Document())
        {
            // Add a blank page (required for PDF)
            createDoc.Pages.Add();

            // Sample ZUGFeRD XML content
            string xmlContent = "<Invoice><ID>12345</ID></Invoice>";
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                // Create a file specification for the embedded XML using the (fileName, description) constructor
                FileSpecification fileSpec = new FileSpecification("invoice.xml", "ZUGFeRD Invoice");
                // Assign the XML data to the Contents stream of the file specification
                fileSpec.Contents = xmlStream;
                // Add the file specification to the document's embedded files collection
                createDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF (evaluation mode allows up to 4 elements, we have only one)
            createDoc.Save("input.pdf");
        }

        // Load the PDF and extract the embedded ZUGFeRD XML
        using (Document doc = new Document("input.pdf"))
        {
            // Iterate through embedded files (1‑based indexing as required by Aspose.Pdf)
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
            {
                FileSpecification embeddedFile = doc.EmbeddedFiles[i];
                if (embeddedFile.Name != null && embeddedFile.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    // Retrieve the stream that holds the embedded file content
                    Stream contentStream = embeddedFile.Contents;
                    if (contentStream != null)
                    {
                        // Ensure the stream is positioned at the beginning
                        contentStream.Position = 0;
                        // Write the stream to a local file
                        using (FileStream fileStream = new FileStream("ZUGFeRD.xml", FileMode.Create, FileAccess.Write))
                        {
                            contentStream.CopyTo(fileStream);
                        }
                        Console.WriteLine("Extracted embedded XML to ZUGFeRD.xml");
                    }
                }
            }
        }
    }
}