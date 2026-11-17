using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string portfolio1 = "portfolio1.pdf";
        const string portfolio2 = "portfolio2.pdf";
        const string outputPath = "merged_portfolio.pdf";

        if (!File.Exists(portfolio1) || !File.Exists(portfolio2))
        {
            Console.Error.WriteLine("One or both input portfolio files are missing.");
            return;
        }

        // Merge PDFs and preserve portfolio (embedded file) metadata
        using (Document target = new Document(portfolio1))
        using (Document source = new Document(portfolio2))
        {
            // Append all pages from source to target
            target.Pages.Add(source.Pages);

            // Ensure the target has a Collection (portfolio) initialized
            if (target.Collection == null)
                target.Collection = new Collection();

            // Copy each embedded file (portfolio item) from source to target
            if (source.EmbeddedFiles != null)
            {
                foreach (FileSpecification srcSpec in source.EmbeddedFiles)
                {
                    // Read the original attachment content into a memory stream
                    using (MemoryStream contentStream = new MemoryStream())
                    {
                        // Reset position just in case
                        if (srcSpec.Contents.CanSeek)
                            srcSpec.Contents.Position = 0;
                        srcSpec.Contents.CopyTo(contentStream);
                        contentStream.Position = 0;

                        // Create a new FileSpecification for the target portfolio using the content stream
                        var tgtSpec = new FileSpecification(contentStream, srcSpec.Name, srcSpec.Description);

                        // Preserve metadata (if available)
                        tgtSpec.Params.ModDate = srcSpec.Params.ModDate;
                        tgtSpec.Params.CreationDate = srcSpec.Params.CreationDate;
                        tgtSpec.MIMEType = srcSpec.MIMEType;

                        // Add the specification to the target portfolio collection
                        target.Collection.Add(tgtSpec);
                    }
                }
            }

            // Save the merged portfolio
            target.Save(outputPath);
        }

        Console.WriteLine($"Merged portfolio saved to '{outputPath}'.");
    }
}
