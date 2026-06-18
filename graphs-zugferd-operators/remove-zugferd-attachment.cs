using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with a dummy ZUGFeRD attachment
        using (Document doc = new Document())
        {
            // Add a single page (evaluation mode allows up to 4 pages)
            Page page = doc.Pages.Add();

            // Create a simple XML content representing ZUGFeRD data
            using (MemoryStream zugFerdStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(zugFerdStream))
                {
                    writer.Write("<ZUGFeRD></ZUGFeRD>");
                    writer.Flush();
                    zugFerdStream.Position = 0;

                    // Create a FileSpecification for the embedded file
                    FileSpecification fileSpec = new FileSpecification("ZUGFeRD.xml", "ZUGFeRD XML");
                    fileSpec.Contents = zugFerdStream;

                    // Attach the XML as an embedded file
                    doc.EmbeddedFiles.Add(fileSpec);
                }
            }

            // Save the PDF that now contains the ZUGFeRD attachment
            doc.Save("input.pdf");
        }

        // Step 2: Load the PDF and remove the ZUGFeRD attachment
        using (Document doc = new Document("input.pdf"))
        {
            // Delete the embedded file by its name. If it does not exist, the call is ignored.
            doc.EmbeddedFiles.Delete("ZUGFeRD.xml");
            // Save the resulting PDF without the ZUGFeRD attachment
            doc.Save("output.pdf");
        }
    }
}