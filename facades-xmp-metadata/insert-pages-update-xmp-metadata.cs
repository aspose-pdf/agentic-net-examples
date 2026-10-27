using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input files
        const string basePdfPath   = "base.pdf";      // PDF to receive new pages
        const string insertPdfPath = "insert.pdf";    // PDF whose pages will be inserted
        const string outputPdfPath = "combined.pdf"; // Final output

        // Verify files exist
        if (!File.Exists(basePdfPath) || !File.Exists(insertPdfPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        try
        {
            // Load the base document
            using (Document baseDoc = new Document(basePdfPath))
            {
                // Load the document whose pages will be inserted
                using (Document srcDoc = new Document(insertPdfPath))
                {
                    // Insert each page from srcDoc after the first page of baseDoc
                    int insertPos = 2; // 1‑based index – after page 1
                    for (int i = 1; i <= srcDoc.Pages.Count; i++)
                    {
                        // Insert a copy of the source page at the desired position
                        baseDoc.Pages.Insert(insertPos, srcDoc.Pages[i]);
                        insertPos++;
                    }
                }

                // -----------------------------------------------------------------
                // Step 2: Update XMP‑like metadata using the Document.Metadata indexer
                // -----------------------------------------------------------------
                baseDoc.Metadata["Title"]   = "Combined Document";
                baseDoc.Metadata["Author"]  = "John Doe";
                baseDoc.Metadata["Subject"] = "Demo of page insertion + XMP update";
                baseDoc.Metadata["Keywords"] = "Aspose.Pdf, XMP, Page Insertion";

                // Save the final PDF
                baseDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully created '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDFs: {ex.Message}");
        }
    }
}
