using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string basePdfPath = "base.pdf";
        const string sourcePdfPath = "source.pdf";
        const string resultPdfPath = "result.pdf";

        // Create simple PDFs if they do not exist
        if (!File.Exists(basePdfPath))
            CreateSimplePdf(basePdfPath, "Base Document");
        if (!File.Exists(sourcePdfPath))
            CreateSimplePdf(sourcePdfPath, "Source Document");

        // Load the base document once; it will be accessed by multiple threads
        using (Document baseDoc = new Document(basePdfPath))
        {
            // Object used for synchronizing access to the Document
            object syncLock = new object();

            // Task that repeatedly imports pages from the source PDF
            Task importTask = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    // Load source PDF inside the loop to avoid sharing Document instances across threads
                    using (Document srcDoc = new Document(sourcePdfPath))
                    {
                        lock (syncLock)
                        {
                            // Insert all pages of the source at the end of the base document
                            // Correct overload: Insert(pageNumber, sourceDocument.Pages)
                            baseDoc.Pages.Insert(baseDoc.Pages.Count + 1, srcDoc.Pages);
                        }
                    }
                    Thread.Sleep(100); // Simulate work
                }
            });

            // Task that repeatedly deletes the first page of the document
            Task deleteTask = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    lock (syncLock)
                    {
                        if (baseDoc.Pages.Count > 0)
                        {
                            // Delete the first page (pages are 1‑based)
                            baseDoc.Pages.Delete(1);
                        }
                    }
                    Thread.Sleep(150); // Simulate work
                }
            });

            // Wait for both operations to finish
            Task.WaitAll(importTask, deleteTask);

            // Save the resulting PDF
            baseDoc.Save(resultPdfPath);
        }

        // Example usage of a class from Aspose.Pdf.Facades (no operation needed for the test)
        PdfFileEditor editor = new PdfFileEditor();

        Console.WriteLine("Concurrent import/delete test completed.");
    }

    // Helper method to create a one‑page PDF containing simple text
    static void CreateSimplePdf(string path, string content)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Fully qualified TextFragment to avoid ambiguity
            TextFragment tf = new TextFragment(content);
            page.Paragraphs.Add(tf);
            doc.Save(path);
        }
    }
}