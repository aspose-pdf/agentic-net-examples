using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        const string portfolioPath1 = "portfolio1.pdf";
        const string portfolioPath2 = "portfolio2.pdf";
        const string mergedPath      = "merged_portfolio.pdf";

        // ---------------------------------------------------------------------
        // 1. Prepare sample embedded files (simple PDFs) if they do not exist.
        // ---------------------------------------------------------------------
        const string sampleFileA = "sampleA.pdf";
        const string sampleFileB = "sampleB.pdf";
        EnsureSamplePdf(sampleFileA, "Sample A – first portfolio");
        EnsureSamplePdf(sampleFileB, "Sample B – second portfolio");

        // ---------------------------------------------------------------------
        // 2. Create two PDF portfolios (collections) that embed the sample files.
        // ---------------------------------------------------------------------
        CreatePortfolio(portfolioPath1, sampleFileA, "First portfolio – contains Sample A");
        CreatePortfolio(portfolioPath2, sampleFileB, "Second portfolio – contains Sample B");

        // ---------------------------------------------------------------------
        // 3. Load the two portfolios and merge them while preserving collection items.
        // ---------------------------------------------------------------------
        using (Document doc1 = new Document(portfolioPath1))
        using (Document doc2 = new Document(portfolioPath2))
        using (Document merged = new Document())
        {
            // Merge the page content (portfolios are usually empty pages).
            merged.Merge(doc1, doc2);

            // Ensure the merged document has a Collection object.
            if (merged.Collection == null)
                merged.Collection = new Collection();

            // Helper local function to clone a FileSpecification safely.
            static FileSpecification CloneFileSpec(FileSpecification src)
            {
                // Create a new spec using the original name and description.
                var clone = new FileSpecification(src.Name, src.Description);

                // Copy the original contents stream into a new MemoryStream.
                if (src.Contents != null)
                {
                    var ms = new MemoryStream();
                    src.Contents.Position = 0; // ensure we start from the beginning
                    src.Contents.CopyTo(ms);
                    ms.Position = 0;
                    clone.Contents = ms;
                }
                return clone;
            }

            // Copy collection items from the first portfolio.
            if (doc1.Collection != null)
            {
                foreach (FileSpecification fs in doc1.Collection)
                {
                    merged.Collection.Add(CloneFileSpec(fs));
                }
            }

            // Copy collection items from the second portfolio.
            if (doc2.Collection != null)
            {
                foreach (FileSpecification fs in doc2.Collection)
                {
                    merged.Collection.Add(CloneFileSpec(fs));
                }
            }

            // Optional: set some metadata for the merged portfolio.
            merged.Info.Title = "Merged PDF Portfolio";
            merged.Info.Author = Environment.UserName;

            merged.Save(mergedPath);
        }

        Console.WriteLine($"Merged portfolio saved to '{mergedPath}'.");
    }

    // ---------------------------------------------------------------------
    // Helper: creates a simple one‑page PDF if it does not already exist.
    // ---------------------------------------------------------------------
    private static void EnsureSamplePdf(string path, string text)
    {
        if (File.Exists(path))
            return;

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment(text));
            doc.Save(path);
        }
    }

    // ---------------------------------------------------------------------
    // Helper: creates a PDF portfolio (Collection) that embeds a file.
    // ---------------------------------------------------------------------
    private static void CreatePortfolio(string portfolioPath, string fileToEmbed, string description)
    {
        using (Document portfolio = new Document())
        {
            // Portfolios are usually empty pages; we add a blank page to keep the file valid.
            portfolio.Pages.Add();

            // Initialise the Collection (portfolio) if it is null.
            if (portfolio.Collection == null)
                portfolio.Collection = new Collection();

            var fileSpec = new FileSpecification(fileToEmbed, description)
            {
                // Embed the file contents via a stream (preserves metadata).
                Contents = new MemoryStream(File.ReadAllBytes(fileToEmbed))
            };

            portfolio.Collection.Add(fileSpec);

            // Add some document‑level metadata (optional).
            portfolio.Info.Title = description;
            portfolio.Info.Subject = "PDF Portfolio example";

            portfolio.Save(portfolioPath);
        }
    }
}
